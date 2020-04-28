﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlog.Areas.Identity.Data;
using SimpleBlog.Data;

[assembly: HostingStartup(typeof(SimpleBlog.Areas.Identity.IdentityHostingStartup))]
namespace SimpleBlog.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SimpleBlogContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("SimpleBlogContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SimpleBlogContext>();
            });
        }
    }
}