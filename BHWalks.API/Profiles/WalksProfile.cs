using AutoMapper;

namespace BHWalks.API.Profiles
{
    public class WalksProfile :Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>()
                .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
                .ReverseMap();

            //If propertis of destination model and source model are not the same
            //CreateMap<Models.Domain.Region, Models.DTO.Region>()
            //    .ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));
        }
    }
}
