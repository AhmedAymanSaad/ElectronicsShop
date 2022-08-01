using AutoMapper;
using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;
using ElectronicsShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }
        [HttpGet("GetRange")]
        public IActionResult GetRange(int skip=0, int take=1)
        {
            var categories = _categoryRepository.GetRange(skip, take);
            var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoriesDTO);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDTO);
            _categoryRepository.Add(category);
            _unitOfWork.Save();
            return Ok(categoryDTO);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Delete(category);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDTO);
            _categoryRepository.Update(category);
            _unitOfWork.Save();
            return Ok(categoryDTO);
        }

    }
}
