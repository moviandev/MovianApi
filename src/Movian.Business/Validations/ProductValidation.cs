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
    }
  }
}