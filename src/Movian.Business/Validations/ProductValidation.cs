using FluentValidation;
using Movian.Business.Models;

namespace Movian.Business.Validations
{
  public class ProductValidation : AbstractValidator<Product>
  {
    public ProductValidation()
    {
      RuleFor(p => p.Name)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(2, 200)
        .WithMessage("The field {PropertyName} need to have characters in between {MinLength} and {MaxLength}");

      RuleFor(p => p.Description)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(2, 1000)
        .WithMessage("The field {PropertyName} need to have characteres in between {MinLength} and {MaxLength}");

      RuleFor(p => p.Value)
        .GreaterThan(0)
        .WithMessage("The field {PropertyName} need to be greater than {ComparisonValue}");
    }
  }
}