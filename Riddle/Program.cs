using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Riddle.Models;

namespace Riddle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try
            {

            
                var scope = host.Services.CreateScope(); //get the middleware services through Dependency Injection
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    //userManager handles all the users account
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    //roleManager handles all the roles account

                context.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if(!context.Roles.Any())
                {
                    roleManager.CreateAsync(adminRole).GetAwaiter().GetResult(); //GetAwaiter().GetResult() == await
                    // here await is not used because whole process is not asynchronous
                    //create a Roll
                }

                if (!context.Users.Any(u => u.UserName == "admin"))
                {
                    //create an Admin
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };

                    var result = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    // add Role to the user
                    userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
