using AutoMapper;
using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;
using ElectronicsShopAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElectronicsShopAPI.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        private const string _loginProvider = "ElectronicsShopAPI";
        private const string _refreshToken = "RefreshToken";

        public UserRepository(ElectronicsShopDbContext context, IMapper mapper, UserManager<User> userManager, IConfiguration configuration) : base(context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterUserDTO registerUserDto)
        {
            _user = _mapper.Map<User>(registerUserDto);
            _user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(_user, registerUserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }
            
            return result.Errors;
        }

        public async Task<AuthUserDTO> Login(LoginUserDTO loginUserDto)
        {
            _user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginUserDto.Password);

            if (_user == null || isValidUser == false)
                return null;

            var token = await GenerateToken();

            return new AuthUserDTO
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken(),
                Roles = await _userManager.GetRolesAsync(_user)
            };
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }
        
        public async Task<AuthUserDTO> VerifyRefreshToken(AuthUserDTO authUserDto)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(authUserDto.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != authUserDto.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, authUserDto.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthUserDTO
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
        public async Task<bool> PromoteUserToAdmin(string id)
        {
            _user = await _userManager.FindByIdAsync(id);
            if (_user == null)
                return false;   
            await _userManager.AddToRoleAsync(_user, "Administrator");
            return true;
        }
        private async Task<string> GenerateToken()
        {
            var key = _configuration["JwtSettings:Key"];
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
