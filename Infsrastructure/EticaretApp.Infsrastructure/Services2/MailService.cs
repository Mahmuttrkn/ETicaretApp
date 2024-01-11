﻿using EticaretApp.Application.Abstractions.Services;
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

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMessageAsync(new[] {to},subject,body,isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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
    }
}
