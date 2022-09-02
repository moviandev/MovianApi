using FluentValidation;
using Movian.Business.Models;

namespace Movian.Business.Services
{
  public abstract class BaseService
  {
    protected bool ExcuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity)
        where TValidation : AbstractValidator<TEntity>
        where TEntity : Entity
    {
      var validator = validation.Validate(entity);

      if (validator.IsValid)
        return true;

      return false;
    }
  }
}