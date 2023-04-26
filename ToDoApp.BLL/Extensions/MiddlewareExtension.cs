using System;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.BLL.Implementations;
using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL.Context;
using ToDoApp.DAL.Implementations;
using ToDoApp.DAL.Interfaces;

namespace ToDoApp.BLL.Extensions
{
    public static class MiddlewareExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IToDoListService, ToDoListService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<ToDoAppDbContext>>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}

