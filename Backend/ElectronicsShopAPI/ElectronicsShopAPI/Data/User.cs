using Microsoft.AspNetCore.Identity;

namespace ElectronicsShopAPI.Data
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime BrithDate { get; set; }
    }
}
