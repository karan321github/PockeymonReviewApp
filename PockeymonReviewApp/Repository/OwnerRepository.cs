﻿using AutoMapper;
using PockeymonReviewApp.Data;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public OwnerRepository(ApplicationDBContext context , IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public Owner GetOwner(int ownerId)
        {
            return _context.Owner.Where(e => e.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPockeymon(int pokeId)
        {
            return _context.PockeymonOwners
                .Where(p => p.Pockymon.Id == pokeId)
                .Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owner.ToList();
        }

        public ICollection<Pockymon> GetPockeymonByOwner(int ownerId)
        {
            return _context.PockeymonOwners.
                Where(p => p.Owner.Id == ownerId)
                .Select(p => p.Pockymon).ToList();

        }

        public bool OwnerExist(int ownerId)
        {
            return _context == null || _context.Owner.Any(e => e.Id == ownerId);
        }
    }
}
