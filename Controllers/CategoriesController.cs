using API_Movies.Models;
using API_Movies.Models.Dtos;
using API_Movies.Repositories;
using API_Movies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_Movies.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategory ctRepo, IMapper mapper)
        {
            _categoryRepository = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            
            var categoriesDto = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoriesDto.Add(_mapper.Map<CategoryDto>(category));
            }
            
            return Ok(categoriesDto);
        }

        [HttpGet("{categoryId:int}", Name ="GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoryById(int categoryId)
        {
            var itemCategory = _categoryRepository.GetCategoryById(categoryId);

            if(itemCategory == null)
            {
                return NotFound();
            }

            var itemCategoryDto = _mapper.Map<CategoryDto>(itemCategory);
            
            return Ok(itemCategoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto category)
        {
            if (!ModelState.IsValid || category == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.ExistsCategoryByName(category.CategoryName))
            {
                ModelState.AddModelError("", $"Category already exists");
                return StatusCode(404, ModelState);
            }


            var categoryFromBody = _mapper.Map<Category>(category);

            if (!_categoryRepository.CreateCategory(categoryFromBody))
            {
                ModelState.AddModelError("", $"Creation failed: {category.CategoryName}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetCategoryById", new {categoryId = categoryFromBody.Id}, categoryFromBody);
        }

        [HttpPatch("{categoryId:int}", Name = "UpdateCategoryById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategoryById(int categoryId, [FromBody] CategoryDto category)
        {
            if (!ModelState.IsValid || category == null || categoryId != category.Id)
            {
                return BadRequest(ModelState);
            }

            var categoryFromBody = _mapper.Map<Category>(category);

            if (!_categoryRepository.UpdateCategory(categoryFromBody))
            {
                ModelState.AddModelError("", $"Update failed: {category.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{categoryId:int}", Name = "ChangeCategoryById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ChangeCategoryById(int categoryId, [FromBody] CategoryDto category)
        {
            if (!ModelState.IsValid || category == null || categoryId != category.Id)
            {
                return BadRequest(ModelState);
            }

            var existsCategory = _categoryRepository.GetCategoryById(categoryId);

            if(existsCategory == null)
            {
                return NotFound($"Category by id not exists");
            }

            var categoryFromBody = _mapper.Map<Category>(category);

            if (!_categoryRepository.UpdateCategory(categoryFromBody))
            {
                ModelState.AddModelError("", $"Update failed: {category.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int categoryId)
        {            

            if (!_categoryRepository.ExistsCategoryById(categoryId))
            {
                return NotFound();
            }

            var category = _categoryRepository.GetCategoryById(categoryId);

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", $"Delete failed: {category.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
