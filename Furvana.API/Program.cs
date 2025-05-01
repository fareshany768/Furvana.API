using Furvana.API.Data;
using Furvana.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Furvana.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = ""
            });

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (!context.Pets.Any())
                {
                    var pets = new List<Pet>
                    {
                        // === Cats ===
                        new Pet { Name = "Maine Coon 1", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Maine Coon/Maine Coon1.jpg" },
                        new Pet { Name = "Maine Coon 2", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Maine Coon/Maine Coon2.jpg" },
                        new Pet { Name = "Siamese 1", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Siamese/Siamese1.jpg" },
                        new Pet { Name = "Siamese 2", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Siamese/Siamese2.jpg" },
                        new Pet { Name = "Baladi 1", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Baladi Cat (Egyptian Street Cat)/Baladi1.jpg" },
                        new Pet { Name = "Baladi 2", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Baladi Cat (Egyptian Street Cat)/Baladi2.jpg" },
                        new Pet { Name = "Egyptian Mau 1", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Egyptian Mau/Egyptian Mau1.jpg" },
                        new Pet { Name = "Egyptian Mau 2", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Cats/Egyptian Mau/Egyptian Mau2.jpg" },

                        // === Dogs ===
                        new Pet { Name = "Golden 1", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Golden Retriever/Golden1.jpg" },
                        new Pet { Name = "Golden 2", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Golden Retriever/Golden2.jpg" },
                        new Pet { Name = "Rottweiler 1", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Rottweiler/Rottweiler1.jpg" },
                        new Pet { Name = "Rottweiler 2", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Rottweiler/Rottweiler2.jpg" },
                        new Pet { Name = "German 1", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/German Shepherd/German1.jpg" },
                        new Pet { Name = "German 2", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/German Shepherd/German2.jpg" },
                        new Pet { Name = "Baladi 1", Type = "Dog", Breed = "Baladi", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Baladi/Baladi1.jpg" },
                        new Pet { Name = "Baladi 2", Type = "Dog", Breed = "Baladi", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Baladi/Baladi2.jpg" },
                        new Pet { Name = "Pit Bull 1", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Pit Bull/Pit Bull1.jpg" },
                        new Pet { Name = "Pit Bull 2", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Large", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Dogs/Pit Bull/Pit Bull2.jpg" },

                        // === Birds ===
                        new Pet { Name = "White Canary", Type = "Bird", Breed = "Canary", Age = 1, Size = "Small", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Birds/Canary/white kanary.jpg" },
                        new Pet { Name = "Yellow Canary", Type = "Bird", Breed = "Canary", Age = 1, Size = "Small", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Birds/Canary/yellow kanary.jpg" },
                        new Pet { Name = "Cockatiel 1", Type = "Bird", Breed = "Cockatiel", Age = 1, Size = "Small", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Birds/Cockatiel/Cockatiel1.jpg" },
                        new Pet { Name = "Cockatiel 2", Type = "Bird", Breed = "Cockatiel", Age = 1, Size = "Small", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Birds/Cockatiel/Cockatiel2.jpg" },
                        new Pet { Name = "Budgie 1", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 1, Size = "Small", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Birds/Budgie (Parakeet)/Budgie1.jpg" },
                        new Pet { Name = "Budgie 2", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 1, Size = "Small", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/Photos/Birds/Budgie (Parakeet)/Budgie2.jpg" }
                    };

                    context.Pets.AddRange(pets);
                    context.SaveChanges();
                }
            }

            app.Run();
        }
    }
}
