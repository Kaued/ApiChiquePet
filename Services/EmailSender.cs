
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace ApiChikPet.Services
{

  public class EmailSender : IEmailSender
  {
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EmailSender(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
      _config = configuration;
      _webHostEnvironment = webHostEnvironment;
    }

    public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message, string title)
    {
      string htmlBody = "";
      var pathToFile = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() +
                      "Templates" + Path.DirectorySeparatorChar.ToString() +
                      "Email" + Path.DirectorySeparatorChar.ToString() +
                      "DefaultEmail.html";

      using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
      {
        htmlBody = streamReader.ReadToEnd();
      }

      string footer = "Atenciosamente, Equipe Chikpet. Para entrar em contato, utilize o e-mail chikpetrp@gmail.com ou ligue para [Inserir número de telefone].<br/> Este é um e-mail automático; se recebido por engano, favor ignorar. Chikpet - [Inserir endereço da empresa]<br/>e <a href=''>chikpet.com.br</a>.";

      LinkedResource res = new LinkedResource(_webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "images" + Path.DirectorySeparatorChar.ToString() + "default" + Path.DirectorySeparatorChar.ToString() + "logo.jpg");
      res.ContentId = Guid.NewGuid().ToString();
      string imageBody = @"cid:" + res.ContentId;


      string messageBody = string.Format(htmlBody,
        title,
        imageBody,
        message,
        footer
      );

      var alternate = GetEmbeddedImage(messageBody, res);

      var mailMessage = new MailMessage(fromAddress, toAddress, subject, messageBody);
      mailMessage.AlternateViews.Add(alternate);

      mailMessage.IsBodyHtml = true;

      using (var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]!))
      {
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(_config["SMTP:UserName"], _config["SMTP:Password"])
      })
      {
        await client.SendMailAsync(mailMessage);
      }
    }

    private AlternateView GetEmbeddedImage(string htmlBody, LinkedResource res)
    {
      AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
      alternateView.LinkedResources.Add(res);
      return alternateView;
    }

    private static string FixBase64ForImage(string Image)
    {
      System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
      sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
      return sbText.ToString();
    }
  }

}