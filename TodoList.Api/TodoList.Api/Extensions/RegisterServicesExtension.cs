using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Services;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain.Repositories;
using TodoList.Infra.Data.Repository;

namespace TodoList.Api.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        }
    }
}
