using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PockeymonReviewApp.Models;
using PockeymonReviewApp.Repository;
using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Interfaces;

namespace PockeymonReviewApp.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository , IMapper mapper)
        { 
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200 , Type = typeof(IEnumerable<Review>))]

        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            //if(ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            return Ok(reviews);  
        }
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]

        public IActionResult GetReview(int id)
        {
            if (_reviewRepository.ReviewExists(id))
            {
                return Ok(ModelState);  
            }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));

            if(!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            return Ok(review);
        }


        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewForAPockeymon(int pokeId)
        {
            var review = _mapper.Map<ReviewDto>(_reviewRepository
                .GetReviewsOfAPokemon(pokeId));

            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }

            return Ok(review);
        }

            

    }
}
