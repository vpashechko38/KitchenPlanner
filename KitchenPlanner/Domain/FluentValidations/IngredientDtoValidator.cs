using FluentValidation;
using KitchenPlanner.Api.Dtos;

namespace KitchenPlanner.Domain.FluentValidations;

public class IngredientDtoValidator : AbstractValidator<IngredientDto>
{
    public IngredientDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .MaximumLength(255);
        RuleFor(x => x.Name)
            .NotNull()
            .MaximumLength(255);
    }
}