using AutoMapper;
using Domain.Dto.Api;
using Domain.Dto.Integration;
using GeoCoordinatePortable;
using Infrastructure.Helper;

namespace Domain.Mapper
{
    public class PostCodeProfile : Profile
    {
        public PostCodeProfile()
        {
            CreateMap<PostCodeResultDto, PostCodeLocationDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Result.Postcode))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => $"{src.Result.AdminDistrict}, {src.Result.Country}"))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Result.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Result.Longitude));
        }
    }
}