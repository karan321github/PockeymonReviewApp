using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PockeymonReviewApp.Data;
using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        public IOwnerRepository _OwnerRepository;
        public IMapper _mapper;
        public OwnerController(IOwnerRepository OwnerRepository , IMapper mapper) 
        {
            _OwnerRepository = OwnerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]

        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>( _OwnerRepository.GetOwners());
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]

        public IActionResult GetOwner(int ownerId) 
        {
            if (_OwnerRepository.OwnerExist(ownerId))
            {
                return NotFound();
            }

            var owner = _mapper.Map<OwnerDto>(_OwnerRepository.GetOwner(ownerId));
            if (!ModelState.IsValid) 
            {
                BadRequest(ModelState);
            }

            return Ok(owner);   
        }

        [HttpGet("{ownerId}/pockeymon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]

        public IActionResult getPockeymonByOwner(int ownerId)
        {
            if(_OwnerRepository.OwnerExist(ownerId)) { return NotFound(); }
            var owner = _mapper.Map<List<PockeymonDto>>(_OwnerRepository
                .GetPockeymonByOwner(ownerId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner);
        }
        
    }
}
