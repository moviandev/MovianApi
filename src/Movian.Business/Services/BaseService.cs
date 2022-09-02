using FluentValidation;
using FluentValidation.Results;
using Movian.Business.Interfaces;
using Movian.Business.Models;
using Movian.Business.Notifications;

namespace Movian.Business.Services
{
  public abstract class BaseService
  {
    private readonly INotifier _notifier;

    protected BaseService(INotifier notifier)
    {
      _notifier = notifier;
    }

    protected void Notify(ValidationResult result)
    {
      foreach (var error in result.Errors)
        Notify(error.ErrorMessage);
    }

    protected void Notify(string message)
    {
      _notifier.Handle(new Notification(message));
    }

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