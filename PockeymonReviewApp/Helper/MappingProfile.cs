using AutoMapper;
using PockeymonReviewApp.Dto;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                CreateMap<Pockymon , PockeymonDto>();
                CreateMap<Category , CategoryDto>();
                CreateMap<Country , CountryDto>();
                CreateMap<CountryDto, Country>();
                CreateMap<OwnerDto, Owner>();
                CreateMap<ReviewDto, Review>();
                CreateMap<ReviewerDto, Reviewer>();

        }
    }
}
