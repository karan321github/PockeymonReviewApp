﻿using AutoMapper;
using PockeymonReviewApp.Data;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Repository
{

    public class CountryRepository : ICountryRepository
    {
        private ApplicationDBContext _context;
        private IMapper _mapper;
        public CountryRepository(ApplicationDBContext context , IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;

        }

        public bool CountryExist(int countryId)
        {
            return _context.Countries.Any(e => e.Id == countryId);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owner.Where(e => e.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return _context.Owner.Where(e => e.Country.Id == countryId).ToList();
        }
    }
}
