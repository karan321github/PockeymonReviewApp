using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PockeymonReviewApp.Data;
using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;
using PockeymonReviewApp.Repository;

namespace PockeymonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        public IOwnerRepository _OwnerRepository;
        public ICountryRepository _countryRepository;
        public IMapper _mapper;
        public OwnerController(IOwnerRepository OwnerRepository, 
            ICountryRepository countryRepository , IMapper mapper)
        {
            _OwnerRepository = OwnerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]

        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_OwnerRepository.GetOwners());
            if (!ModelState.IsValid)
            {
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
            if (_OwnerRepository.OwnerExist(ownerId)) { return NotFound(); }
            var owner = _mapper.Map<List<PockeymonDto>>(_OwnerRepository
                .GetPockeymonByOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(owner);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult createOwner([FromQuery] int countryId,
            [FromBody] OwnerDto ownerCreate)
        {
            if (createOwner == null)
            {
                BadRequest(ModelState);
            }

            var owner = _OwnerRepository.GetOwners()
                .Where(o => o.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(owner != null)
            {
                ModelState.AddModelError("", "Owner with this name already exist");
                StatusCode(422, ModelState);
            }

            if(ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Owner>(ownerCreate);
            ownerMap.Country = _countryRepository.GetCountry(countryId);
            if (!_OwnerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                StatusCode(500 , ModelState);
            }

            return Ok(ownerMap);
        }

    }
}
