using FluentValidation;
using Movian.Business.Models;

namespace Movian.Business.Validations
{
  public class AddressValidation : AbstractValidator<Address>
  {
    public AddressValidation()
    {
      RuleFor(p => p.Number)
        .Length(1, 10)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters")
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty");

      RuleFor(p => p.StreetName)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(2, 100)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters");

      RuleFor(p => p.AdditionalAddressData)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(2, 200)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters");

      RuleFor(p => p.City)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(2, 100)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters");

      RuleFor(p => p.State)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(2, 50)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters");

      RuleFor(p => p.ZipCode)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(1, 8)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters");

      RuleFor(p => p.Neighborhood)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot empty")
        .Length(2, 100)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters");
    }
  }
}