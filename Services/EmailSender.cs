
using System.Net;
using System.Net.Mail;

namespace ApiChikPet.Services
{

  public class EmailSender : IEmailSender
  {
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration configuration)
    {
      _config = configuration;
    }

    public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message, string title)
    {
      var mailMessage = new MailMessage(fromAddress, toAddress, subject, message);

      using (var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]!))
      {
        Credentials = new NetworkCredential(_config["SMTP:UserName"], _config["SMTP:Password"])
      })
      {
        await client.SendMailAsync(mailMessage);
      }
    }
  }
}