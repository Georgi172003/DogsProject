using System;
using DogApp.Data;
using DogApp.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DogApp.Areas.Identity.IdentityHostingStartup))]
namespace DogApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                //services.AddDefaultIdentity<IdentityUser>(x => x.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}