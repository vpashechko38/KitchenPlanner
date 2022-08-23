using AutoMapper;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Data.Models;

namespace KitchenPlanner.Domain.MapperProfiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<RecipeModel, RecipeDto>()
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.CookingTime,
                opt => opt.MapFrom(src => src.CookingTime))
            .ForMember(dest => dest.PictureId,
                opt => opt.MapFrom(src => src.PictureId))
            .ForMember(dest => dest.Ingredients,
                opt => opt.MapFrom(src =>src.Ingredients))
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
        
        CreateMap<RecipeDto, RecipeModel>()
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.CookingTime,
                opt => opt.MapFrom(src => src.CookingTime))
            .ForMember(dest => dest.PictureId,
                opt => opt.MapFrom(src => src.PictureId))
            .ForMember(dest=>dest.Ingredients,
                opt => opt.MapFrom(src=>src.Ingredients))
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
    }
}