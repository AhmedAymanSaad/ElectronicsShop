using AutoMapper;
using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //var categories2 = _unitOfWork.Categories.GetAll();
            var categories = _categoryRepository.GetAll();
            var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoriesDTO);
        }
    }
}
