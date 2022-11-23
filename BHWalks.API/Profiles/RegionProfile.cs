using AutoMapper;

namespace BHWalks.API.Profiles
{
    public class RegionProfile :Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();

            //If propertis of destination model and source model are not the same
            //CreateMap<Models.Domain.Region, Models.DTO.Region>()
            //    .ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));
        }
    }
}
