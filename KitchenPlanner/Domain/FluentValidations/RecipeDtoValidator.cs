using FluentValidation;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Api.Dtos.Recipe;

namespace KitchenPlanner.Domain.FluentValidations;

public class RecipeDtoValidator : AbstractValidator<RecipeDto>
{
    public RecipeDtoValidator()
    {
        RuleFor(x => x.Description).NotNull().MaximumLength(255);
        RuleFor(x => x.CookingTime).NotNull().MaximumLength(255);
        RuleFor(x => x.PictureId).Null();
    }
}