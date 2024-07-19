using AutoMapper;
using WebApi2.API.Models.Domain;
using WebApi2.API.Models.DTO;

namespace WebApi2.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            CreateMap<AddWalkRequestDto,Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto,Walk>().ReverseMap();
        }
    }
}
