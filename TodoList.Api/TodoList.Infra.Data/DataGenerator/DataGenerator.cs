using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TodoList.Domain;
using TodoList.Domain.Entities;
using TodoList.Shared;

namespace TodoList.Infra.Data.DataGenerator
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>()))
            {
                if (context.User.Any())
                {
                    return;
                }

                context.User.Add(
                    new User
                    {
                        Id = 1,
                        Email = "teste@ubistart.com",
                        Password = Md5HashExtensions.CreateMD5("123456"),
                        Profile = EProfile.Administrator
                    });

                context.SaveChanges();
            }
        }
    }
}
