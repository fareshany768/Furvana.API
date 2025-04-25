using Furvana.API.Data;
using Furvana.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Furvana.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add DbContext (SQL Server)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // 3. Add Swagger & Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // 4. Configure Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.MapControllers();

            // 5. Seed database with pets if empty
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Pets.Any())
                {
                    context.Pets.AddRange(
                        new Pet
                        {
                            Name = "Max",
                            Type = "Dog",
                            Breed = "Labrador",
                            Age = 3,
                            Size = "Medium",
                            HealthStatus = "Healthy",
                            GoodWith = "Kids",
                            ImageUrl = "/images/max.jpg"
                        },
                        new Pet
                        {
                            Name = "Bella",
                            Type = "Cat",
                            Breed = "Persian",
                            Age = 2,
                            Size = "Small",
                            HealthStatus = "Vaccinated",
                            GoodWith = "Other cats",
                            ImageUrl = "/images/bella.jpg"
                        }
                    );

                    context.SaveChanges();
                }
            }

            // 6. Run the app
            app.Run();
        }
    }
}
