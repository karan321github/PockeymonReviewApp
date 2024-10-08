﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;
using PockeymonReviewApp.Repository;

namespace PockeymonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCounties()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            //if (!ModelState.IsValid) 
            //{
            //    return BadRequest(ModelState);
            //}

            return Ok(countries);

        }
        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepository.CountryExist(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));

            return Ok(country);
        }
        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]

        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));

            return Ok(country);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult CreateCategory([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(country != null)
            {
                ModelState.AddModelError("", "Country already exist");
                StatusCode(422, ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                StatusCode(500, ModelState);
            }

            return Ok(countryMap);
        }
        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCountry(int countryId , [FromBody] CountryDto updatedCountry)
        {
            if(updatedCountry == null)
            {
                return BadRequest(ModelState);
            }
            if (countryId != updatedCountry.Id)
                return BadRequest(ModelState);

            if (!_countryRepository.CountryExist(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(updatedCountry);

            if (!_countryRepository.UpdateCategory(countryMap))
            {
                {
                    ModelState.AddModelError("", "Something went wrong updating Country");
                    return StatusCode(500, ModelState);
                }

            }
            return Ok("Country updated successfully");
            return NoContent();
        }
        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int countryId)
        {
            if (!_countryRepository.CountryExist(countryId))
            {
                return NotFound();
            }

            var countryToDelete = _countryRepository.GetCountry(countryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_countryRepository.DeleteCountry(countryToDelete))
            {

                ModelState.AddModelError("", "Something went wrong deleting country");
            }

            return Ok("Succesfully deleted country");
        }

    }


}
