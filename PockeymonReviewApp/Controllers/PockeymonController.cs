using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;
using PockeymonReviewApp.Repository;

namespace PockeymonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PockeymonController : Controller
    {
        private readonly IPockeymonRepository _PockeymonRepository;
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICategoryRepository _categoryRepository;
        public PockeymonController(IPockeymonRepository pockeymonRepository,IOwnerRepository ownerRepository , ICategoryRepository categoryRepository , IMapper mapper)
        {
            _PockeymonRepository = pockeymonRepository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pockymon>))]

        public IActionResult GetPockeymons()
        {
            var pockeymon = _mapper.Map<List<PockeymonDto>>(_PockeymonRepository.GetPockeymons());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pockeymon);

        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pockymon))]
        [ProducesResponseType(400)]

        public IActionResult GetPockeymon(int pokeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_PockeymonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }

            var pokeymon = _mapper.Map<PockeymonDto>(_PockeymonRepository.GetPockymon(pokeId));


            return Ok(pokeymon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public IActionResult GetPockemonRating(int pokeId)
        {
            if (!_PockeymonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }

            var rating = _PockeymonRepository.GetPockymonRateing(pokeId);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreatePockeymon([FromQuery] int ownerId, [FromQuery] int catId, PockeymonDto pockeymonCreate)
        {
            if (pockeymonCreate == null)
            {
                return BadRequest(ModelState);
            }

            var existingPockeymon = _PockeymonRepository.GetPockeymons()
                .Where(p => p.Name.Trim().ToUpper() == pockeymonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (existingPockeymon != null)
            {
                ModelState.AddModelError("", "Pockeymon with this name already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pockeymonMap = _mapper.Map<Pockymon>(pockeymonCreate);

            
            //var owner = _ownerRepository.GetOwner(ownerId);
            //if (owner == null)
            //{
            //    ModelState.AddModelError("", "Owner not found");
            //    return BadRequest(ModelState);
            //}

            //var category = _categoryRepository.GetCategory(catId);
            //if (category == null)
            //{
            //    ModelState.AddModelError("", "Category not found");
            //    return BadRequest(ModelState);
            //}

            
            if (!_PockeymonRepository.CreatePockeymon(ownerId, catId, pockeymonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }



    }



}
