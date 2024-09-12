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
        public PockeymonController(IPockeymonRepository pockeymonRepository, IMapper mapper)
        {
            _PockeymonRepository = pockeymonRepository;
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
        [ProducesResponseType(200 , Type = typeof(Pockymon))]
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

            var rating  = _PockeymonRepository.GetPockymonRateing(pokeId);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(rating);
        }


    }



}
