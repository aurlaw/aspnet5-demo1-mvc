using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using Demo1.MVC.Services;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Demo1.Test
{
    public class EmailTest
    {
        public EmailTest()
        {
        }
        
        [Fact]
        public async void PassingTest()
        {

           var builder = new ConfigurationBuilder();          
           builder.AddEnvironmentVariables();
            var config = builder.Build();
            
            var services = new ServiceCollection().AddOptions();
            services.Configure<MGOptions>(config);
            
            var service = services.BuildServiceProvider().GetService<IOptions<MGOptions>>();
            var mService = new MailGunService(service);
            var response = await mService.SendEmailMessageAsync("test@test.com", "subject", "message");
            
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

    }
}
