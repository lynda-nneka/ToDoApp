using System;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Context;

namespace ToDoApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDBConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoAppDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConn")
         ));
        }
    }
}

