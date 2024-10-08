﻿using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Models;
using System.Collections;

namespace PockeymonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        public ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromCountry(int countryId);
        bool CountryExist(int countryId);
        public bool CreateCountry(Country country);
        public bool UpdateCategory(Country country);
        public bool DeleteCountry(Country country);
        bool save();
        
    }
}
