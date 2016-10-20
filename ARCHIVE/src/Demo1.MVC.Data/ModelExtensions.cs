using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Demo1.MVC.Data.Models;

namespace Demo1.MVC.Data {

    public static class ModelExtensions 
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (context.AllMigrationsApplied())
            {
                if(!context.TodoItems.Any())
                {
                    context.TodoItems.Add(new TodoItem {Title ="Test 1", Description = "Desc 1" });   
                    context.TodoItems.Add(new TodoItem {Title ="Test 2", Description = "Desc 2" });   
                    context.TodoItems.Add(new TodoItem {Title ="Test 3", Description = "Desc 3" });   
                    context.TodoItems.Add(new TodoItem {Title ="Test 4", Description = "Desc 4" });   
                    
                    context.SaveChanges();
                }

                // if (!context.WebsiteAds.Any())
                // {
                //     var kitchenAndDining = context.Categories.Single(c => c.DisplayName == "Kitchen & Dining");
                //     var clothing = context.Categories.Single(c => c.DisplayName == "Clothing");

                //     context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/Unicorn.png", TagLine = "Welcome to Unicorn Store", Details = "A series of applications showing EF7 in action", Url = "http://unicornstore.net" });
                //     context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/GitHub.png", TagLine = "Source code available on GitHub", Details = "github.com/rowanmiller/UnicornStore", Url = "http://github.com/rowanmiller/UnicornStore" });
                //     context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/UnicornClicker.png", TagLine = "Possibly the lamest game of the year", Details = "Available in the Windows 10 app store", Url = "https://www.microsoft.com/en-us/store/apps/unicorn-clicker/9nblggh5jzrk" });
                //     context.SaveChanges();
                // }
            }
        }        
        
    }

}