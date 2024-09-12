using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace PockeymonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository? _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository , IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]

        public IActionResult GetCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categories =_mapper.Map<List<CategoryDto>>(_categoryRepository?.GetCategories());

            return Ok(categories);
        }
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]

        public IActionResult GetCategory(int categoryId)
        {
            
            if (!_categoryRepository.isCategoryExist(categoryId))
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepository?.GetCategory(categoryId));

            return Ok(category);
        }

        [HttpGet("pockymon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pockymon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPockeymonByCategory(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var pockeymons = _mapper.Map<List<PockeymonDto>>(_categoryRepository?.GetPockeymonByCategory(categoryId));
            return Ok(pockeymons);
        }
    }
}
