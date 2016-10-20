using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace Demo1.MVC.Services
{
    // This class is used by the application to send Email
    public class MailGunService : IEmailSender
    {
        public MailGunService(IOptions<MGOptions> options)
        {
            this.Options = options.Value;
        }
        public MGOptions Options {get;}
        public Task SendEmailAsync(string email, string subject, string message)
        {
           return SendEmailMessageAsync(email, subject, message);
        }
        
        public async Task<bool> SendEmailMessageAsync(string email, string subject, string message)
        {
            
            var emailMessage = new MimeMessage();
        
            emailMessage.From.Add(new MailboxAddress(this.Options.MGFromName, this.Options.MGFrom));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };
        
            using (var client = new SmtpClient())
            {
                client.LocalDomain = this.Options.MGDomain;                
                await client.ConnectAsync(this.Options.MGSMTPHostname, this.Options.MGSMTPPort, 
                    SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }            
        //     var client = new HttpClient();
        //     client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
        //         Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("api" + ":" + this.Options.MGKey)));

        //         var form = new Dictionary<string, string>();
        //         form["from"]    = this.Options.MGFrom;
        //         form["to"]      = email;
        //         form["subject"] = subject;
        //         form["text"]    = message;
        //       var mailgunUrl =   "https://api.mailgun.net/v2/" + this.Options.MGDomain + "/messages";
        //         Console.WriteLine(mailgunUrl);
        //    return await client.PostAsync(mailgunUrl, new FormUrlEncodedContent(form));
            return false;
            
        }
    }
    
    public class MGOptions
    {
        public string MGFrom {get;set;}
        public string MGFromName {get;set;}
        public string MGKey {get;set;}
        public string MGDomain {get;set;}
        public string MGSMTPHostname {get;set;}
        public int MGSMTPPort {get;set;}
        public string MGSMTPLogin {get;set;}
        public string MGSMTPPassword {get;set;}

    }
}
