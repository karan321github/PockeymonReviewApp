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
                CreateMap<PockeymonDto , Pockymon>();
                CreateMap<Category , CategoryDto>();
                CreateMap<CategoryDto , Category>();
                CreateMap<Country , CountryDto>();
                CreateMap<CountryDto, Country>();
                CreateMap<OwnerDto, Owner>();
                CreateMap<Owner, OwnerDto>();
                CreateMap<ReviewDto, Review>();
                CreateMap<ReviewerDto, Reviewer>();

        }
    }
}
