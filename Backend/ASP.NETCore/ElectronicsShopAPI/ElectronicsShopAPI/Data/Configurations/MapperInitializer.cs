using AutoMapper;
using ElectronicsShopAPI.Models;

namespace ElectronicsShopAPI.Data.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();

            CreateMap<Discount, DiscountDTO>().ReverseMap();
            
            CreateMap<DiscountType, DiscountTypeDTO>().ReverseMap();
            
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<User, AuthUserDTO>().ReverseMap();
            CreateMap<User, IdUserDTO>().ReverseMap();
        }
    }
}
