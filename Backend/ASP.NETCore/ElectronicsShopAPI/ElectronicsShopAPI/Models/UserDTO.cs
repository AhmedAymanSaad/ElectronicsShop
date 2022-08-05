using System.ComponentModel.DataAnnotations;

namespace ElectronicsShopAPI.Models
{
    public class UserDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Your Password is limited to {2} to {1} characters",MinimumLength =8)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BrithDate { get; set; }
        public ICollection<string> Roles { get; set; }
    }
    public class RegisterUserDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 8)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BrithDate { get; set; }
    }
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
    public class AuthUserDTO
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public ICollection<string> Roles { get; set; }
    }
    public class IdUserDTO
    {
        public string? UserId { get; set; }
    }
}
