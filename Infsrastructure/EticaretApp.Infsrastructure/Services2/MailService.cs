using EticaretApp.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Infsrastructure.Services2
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate,string userName,string userSurname)
        {
            string mail = $"Sayın {userName} {userSurname} Merhaba<br>" +
                $"{orderDate} tarihinde vermiş olduğunuz {orderCode} kodlu sipariişini kargoya verilmiştir.";

            await SendMailAsync(to, "Siparişiniz Tamamlandı", mail);
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMailAsync(new[] {to},subject,body,isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            mail.Subject = subject;
            foreach (var item in tos)
            {
                mail.To.Add(item);
            }
            mail.Body = body;
            mail.From = new MailAddress(_configuration["Mail:UserName"], "NG E-Ticaret",System.Text.Encoding.UTF8);


            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:Password"]);
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Host = _configuration["Mail:Host"];
           await smtpClient.SendMailAsync(mail);


        }

        public async Task SendPasswordResetMailAsync(string to,string userId,string resetToken)
        {

            StringBuilder mail = new StringBuilder();
            mail.Append("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki link üzerinden şifrenizi yenileyebilirsiniz.<br><strong>" +
                "<a target=\"_blank\"href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password");
            mail.AppendLine(userId); //Link sonrası araya userId Sokmamıza yarıyor.
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız..</a></strong><br><br><span=\"font-size:12px;\">Not: Eğer Bu Talep Tarafınızca" +
                "Gerçekleştirilmediyse Dikkate Almayınız.<span/><br>Saygılarımızla..<br><br><br>MT-BILISIM"); //Burada da linki kapattık.

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
            
        }
    }
}
