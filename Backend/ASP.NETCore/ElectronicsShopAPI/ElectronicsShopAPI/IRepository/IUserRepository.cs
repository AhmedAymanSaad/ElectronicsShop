using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace ElectronicsShopAPI.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<IdentityError>> Register(RegisterUserDTO registerUserDto);
        Task<AuthUserDTO> Login(LoginUserDTO loginUserDto);
        Task<string> CreateRefreshToken();
        Task<AuthUserDTO> VerifyRefreshToken(AuthUserDTO authUserDto);
        Task<bool> PromoteUserToAdmin(string id);
    }
}
