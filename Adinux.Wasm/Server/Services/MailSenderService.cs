using Adinux.Wasm.Shared.ViewModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Adinux.Wasm.Server.Services
{
    public class MailSenderService
    {
        private SmtpOptions _smtpOptions;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MailSenderService(IOptions<SmtpOptions> smtpOptions, IWebHostEnvironment hostEnvironment)
        {
            _smtpOptions = smtpOptions.Value;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<APIResponseViewModel> SendCataloguesAsync(string to,string name,List<SelectedCataloguesViewModel> selectedCatalogues)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_smtpOptions.Username));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = "Adinux";
                var builder = new BodyBuilder();

                builder.HtmlBody = $"<h1>Hello, dear {name}, your requested catalogs have been sent to you</h1>";
                builder.TextBody = "Hi text";

                 var rootPath = _hostEnvironment.WebRootPath; //get the root path
                foreach (var item in selectedCatalogues)
                {
                    if (!string.IsNullOrEmpty(item.Files))
                    {

                        foreach (var file in item.Files.Split(","))
                        {
                            var fullPath = Path.Combine(rootPath, "catalogues/", file); //combine the root path with that of our json file inside mydata directory

                            builder.Attachments.Add(fullPath);
                        }
                    }
                }

                email.Body = builder.ToMessageBody();

                // send email
                using var smtp = new SmtpClient();
                smtp.CheckCertificateRevocation = false;
                await smtp.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port,SecureSocketOptions.None);
                await smtp.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return new APIResponseViewModel
                {
                    IsSuccesfull = true
                };

            }
            catch (Exception ex)
            {
                return new APIResponseViewModel { Errors = new List<string> { ex.ToString() } };
            }
        }
    }
}
