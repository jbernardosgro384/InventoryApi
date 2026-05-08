using FluentValidation;
using InventoryApi.Shared.DTOs;

namespace InventoryApi.Shared.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre del producto es requerido")
            .MaximumLength(100)
            .WithMessage("El nombre debe tener como máximo 100 caracteres");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("El precio debe ser mayor que 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El stock no puede ser negativa");
    }
}