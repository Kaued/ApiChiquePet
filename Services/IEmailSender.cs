using System.Net.Mail;

namespace ApiChikPet.Services
{

  public interface IEmailSender
  {
    Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message, string title);
  }
}