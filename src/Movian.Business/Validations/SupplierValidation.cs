using FluentValidation;
using Movian.Business.EnumTypes;
using Movian.Business.Models;
using Movian.Business.Validations.DocumentValidations;

namespace Movian.Business.Validations
{
  public class SupplierValidation : AbstractValidator<Supplier>
  {
    public SupplierValidation()
    {
      RuleFor(p => p.Name)
        .NotEmpty()
        .WithMessage("The field {PropertyName} cannot be empty")
        .Length(1, 200)
        .WithMessage("The field {PropertyName} need to have in between {MinLength} and {MaxLength} characters");

      When(f => f.SuplierType == SupplierType.CPF, () =>
      {
        RuleFor(f => f.Document.Length).Equal(CpfValidation.TamanhoCpf)
          .WithMessage("The field needs to have {ComparisonValue} characteres and was inputed {PropertyValue}.");
        RuleFor(f => CpfValidation.Validate(f.Document)).Equal(true)
          .WithMessage("Invalid document.");
      });

      When(f => f.SuplierType == SupplierType.CNPJ, () =>
        {
          RuleFor(f => f.Document.Length).Equal(CnpjValidation.TamanhoCnpj)
            .WithMessage("The field needs to have {ComparisonValue} characteres and was inputed {PropertyValue}.");
          RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true)
            .WithMessage("Invalid document.");
        });
    }
  }
}