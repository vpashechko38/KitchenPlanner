using AutoMapper;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Data.Models;

namespace KitchenPlanner.Domain.MapperProfiles;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<IngredientModel, IngredientDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description));

        CreateMap<IngredientDto, IngredientModel>()
            .ForMember(dest => dest.Id, 
                opt => opt.Ignore())
            .ForMember(dest => dest.Name, 
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, 
                opt => opt.MapFrom(src => src.Description));
    }
}