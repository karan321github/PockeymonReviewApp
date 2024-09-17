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
        private readonly IPockeymonRepository _pockeymonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IPockeymonRepository pockeymonRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _pockeymonRepository = pockeymonRepository;
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]

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

            if (!ModelState.IsValid)
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
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
            {
                return BadRequest(ModelState);
            }

            var existingReviwe = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(existingReviwe != null)
            {
               ModelState.AddModelError("", "Review already exist please review it in your own words");
                return StatusCode(422 , ModelState);
            }

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);


            var reviewMap = _mapper.Map<Review>(reviewCreate);
            reviewMap.Pockeymon = _pockeymonRepository.GetPockymon(pokeId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
