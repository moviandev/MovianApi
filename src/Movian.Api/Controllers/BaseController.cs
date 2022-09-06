using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movian.Business.Interfaces;
using Movian.Business.Notifications;

namespace Movian.Api.Controllers
{
  [ApiController]
  public class BaseController : ControllerBase
  {
    private readonly INotifier _notifier;

    public BaseController(INotifier notifier)
    {
      _notifier = notifier;
    }

    protected bool HasntNotifications()
      => !_notifier.HasNotifications();

    protected ActionResult CustomResponse(object result = null)
    {
      if (HasntNotifications())
      {
        return Ok(new
        {
          success = true,
          data = result,
        });
      }

      return BadRequest(new
      {
        success = false,
        errors = _notifier.GetNotifications(),
      });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
      if (!modelState.IsValid) NotifiyErrorInvalidModel(modelState);
      return CustomResponse();
    }

    protected void NotifiyErrorInvalidModel(ModelStateDictionary modelState)
    {
      var errors = modelState.Values.SelectMany(e => e.Errors);

      foreach (var error in errors)
      {
        var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
        NotifyError(errorMessage);
      }
    }

    protected void NotifyError(string error)
    {
      _notifier.Handle(new Notification(error));
    }

  }
}