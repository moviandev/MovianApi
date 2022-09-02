using System.Collections.Generic;
using Movian.Business.Notifications;

namespace Movian.Business.Interfaces
{
  public interface INotifier
  {
    bool HasNotifications();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
  }
}