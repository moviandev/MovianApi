namespace Movian.Business.Notifications
{
  public class Notification
  {
    public Notification(string message)
    {
      this.Message = message;

    }
    public string Message { get; set; }
  }
}