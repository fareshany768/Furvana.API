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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "images")),
                RequestPath = "/images"
            });

            app.MapControllers();
            

            // 5. Seed database with pets if empty
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                if (!context.Pets.Any())
                {
                    var pets = new List<Pet>
{
    new Pet { Name = "Luna the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8ebcaa2dca.jpg" },
    new Pet { Name = "Milo the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8eb1d4275a.jpg" },
    new Pet { Name = "Simba the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8eb4f8001f.jpg" },
    new Pet { Name = "Nala the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8ec23925e5.jpg" },
    new Pet { Name = "Oliver the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8eb38241fa.jpg" },
    new Pet { Name = "Leo the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8ec3e9fba6.jpg" },
    new Pet { Name = "Chloe the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8ebed73492.jpg" },
    new Pet { Name = "Bella the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8eaff4867a.jpg" },
    new Pet { Name = "Lily the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8ebaacf579.jpg" },
    new Pet { Name = "Zoe the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8eb91c68d6.jpg" },
    new Pet { Name = "Luna the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8eacf9d22d.jpg" },
    new Pet { Name = "Milo the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8ec6f0147c.jpg" },
    new Pet { Name = "Simba the Maine Coon", Type = "Cat", Breed = "Maine Coon", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/maine-coon/[GetPaidStock.com]-67c8eae73b9b6.jpg" },
    new Pet { Name = "Nala the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f2ec94b70.jpg" },
    new Pet { Name = "Oliver the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f1e87877c.jpg" },
    new Pet { Name = "Leo the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f3889b849.jpg" },
    new Pet { Name = "Chloe the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f2c18cc4d.jpg" },
    new Pet { Name = "Bella the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f3d2bda9a.jpg" },
    new Pet { Name = "Lily the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f22d1c2df.jpg" },
    new Pet { Name = "Zoe the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f1cec6b8b.jpg" },
    new Pet { Name = "Luna the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f204316d5.jpg" },
    new Pet { Name = "Milo the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f3075dc21.jpg" },
    new Pet { Name = "Simba the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f26c11da5.jpg" },
    new Pet { Name = "Nala the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f24c02126.jpg" },
    new Pet { Name = "Oliver the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f3a2dd133.jpg" },
    new Pet { Name = "Leo the Siamese", Type = "Cat", Breed = "Siamese", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/siamese/[GetPaidStock.com]-67c8f2aaa531a.jpg" },
    new Pet { Name = "Chloe the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e768227b9.jpg" },
    new Pet { Name = "Bella the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e714ce62b.jpg" },
    new Pet { Name = "Lily the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e7d228c6f.jpg" },
    new Pet { Name = "Zoe the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e39224d1b.jpg" },
    new Pet { Name = "Luna the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e56e71b93.jpg" },
    new Pet { Name = "Milo the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/download.jpg" },
    new Pet { Name = "Simba the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e689718f2.jpg" },
    new Pet { Name = "Nala the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/images.jpg" },
    new Pet { Name = "Oliver the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e35ce44ab.jpg" },
    new Pet { Name = "Leo the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e0efbe0c2.jpg" },
    new Pet { Name = "Chloe the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e4846b988.jpg" },
    new Pet { Name = "Bella the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e52f6290c.jpg" },
    new Pet { Name = "Lily the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/download (1).jpg" },
    new Pet { Name = "Zoe the Baladi Cat (Egyptian Street Cat)", Type = "Cat", Breed = "Baladi Cat (Egyptian Street Cat)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/baladi-cat-(egyptian-street-cat)/[GetPaidStock.com]-67c8e6f4e4c5c.jpg" },
    new Pet { Name = "Luna the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f165a217e.jpg" },
    new Pet { Name = "Milo the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f14257bc7.jpg" },
    new Pet { Name = "Simba the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f03540c51.jpg" },
    new Pet { Name = "Nala the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f113daf82.jpg" },
    new Pet { Name = "Oliver the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f07247912.jpg" },
    new Pet { Name = "Leo the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8efe9a0bba.jpg" },
    new Pet { Name = "Chloe the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8efffa7018.jpg" },
    new Pet { Name = "Bella the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8efcfae1c5.jpg" },
    new Pet { Name = "Lily the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8ee907912d.jpg" },
    new Pet { Name = "Zoe the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8eeba5f1af.jpg" },
    new Pet { Name = "Luna the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f0a692ab6.jpg" },
    new Pet { Name = "Milo the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f12ab06ff.jpg" },
    new Pet { Name = "Simba the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8efae6e96c.jpg" },
    new Pet { Name = "Nala the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8f01bbde0b.jpg" },
    new Pet { Name = "Oliver the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8ee6193ba0.jpg" },
    new Pet { Name = "Leo the Persian", Type = "Cat", Breed = "Persian", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/persian/[GetPaidStock.com]-67c8ef723ff45.jpg" },
    new Pet { Name = "Chloe the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8ea54e3175.jpg" },
    new Pet { Name = "Bella the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8ea2e68049.jpg" },
    new Pet { Name = "Lily the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e9ed8d010.jpg" },
    new Pet { Name = "Zoe the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e96db5ecd.jpg" },
    new Pet { Name = "Luna the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e8fe42ffe.jpg" },
    new Pet { Name = "Milo the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e91d28c5d.jpg" },
    new Pet { Name = "Simba the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e89936929.jpg" },
    new Pet { Name = "Nala the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e9374d65f.jpg" },
    new Pet { Name = "Oliver the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e8763961c.jpg" },
    new Pet { Name = "Leo the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e8dac3aa4.jpg" },
    new Pet { Name = "Chloe the Egyptian Mau", Type = "Cat", Breed = "Egyptian Mau", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/egyptian-mau/[GetPaidStock.com]-67c8e8bfefc52.jpg" },
    new Pet { Name = "Bella the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/91b29edf-9790-47f2-aa5c-5e2b5db2a668.jpg" },
    new Pet { Name = "Lily the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/e1915227-6f26-49c9-a10e-d2ff06029dd7.jpg" },
    new Pet { Name = "Zoe the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/8c48df74-8f1c-41ba-883c-9442b49847b5.jpg" },
    new Pet { Name = "Luna the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/bb410215-9ca0-4389-93a4-378af907d55e.jpg" },
    new Pet { Name = "Milo the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/49ae9491-5d86-4bbe-8284-5ac9250148a7.jpg" },
    new Pet { Name = "Simba the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/6c77abac-c665-4a66-bd9f-7b494d5df8c0.jpg" },
    new Pet { Name = "Nala the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/39810d9e-1bea-4a29-a1dc-9a48e843ed01.jpg" },
    new Pet { Name = "Oliver the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/43e7171e-0f71-405d-9fd5-16a6ec6112b1.jpg" },
    new Pet { Name = "Leo the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/8a110149-616e-4601-97f5-8558fa58473b.jpg" },
    new Pet { Name = "Chloe the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/9be06272-9cde-4e84-a6ec-366915c4547f.jpg" },
    new Pet { Name = "Bella the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/e43f2e32-467e-47f3-890c-1731c210d0b4.jpg" },
    new Pet { Name = "Lily the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/fdc5df19-c49f-43d3-b0b0-d278794fd17d.jpg" },
    new Pet { Name = "Zoe the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/7ae02f3e-9331-4f12-a228-448244fe0401.jpg" },
    new Pet { Name = "Luna the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/9d7cd66b-98f6-413c-9ab5-1edd452eb171.jpg" },
    new Pet { Name = "Milo the Random cats", Type = "Cat", Breed = "Random cats", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/cats/random-cats/5ad8fba1-a061-4c3e-9301-7faf01b0585e.jpg" },
    new Pet { Name = "Coco the Canary", Type = "Bird", Breed = "Canary", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/canary/2 yellow kanary.jpg" },
    new Pet { Name = "Sky the Canary", Type = "Bird", Breed = "Canary", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/canary/yellow kanary.jpg" },
    new Pet { Name = "Angel the Canary", Type = "Bird", Breed = "Canary", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/canary/white kanary.jpg" },
    new Pet { Name = "Rio the Canary", Type = "Bird", Breed = "Canary", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/canary/7 kolors kanary.jpg" },
    new Pet { Name = "Lemon the Canary", Type = "Bird", Breed = "Canary", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/canary/red kanary.jpg" },
    new Pet { Name = "Buddy the Canary", Type = "Bird", Breed = "Canary", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/canary/yellow & white kanary.jpg" },
    new Pet { Name = "Tweety the peguin", Type = "Bird", Breed = "peguin", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/peguin/2 peguins bird 3.jpg" },
    new Pet { Name = "Echo the peguin", Type = "Bird", Breed = "peguin", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/peguin/2 peguins bird 4.jpg" },
    new Pet { Name = "Sunny the peguin", Type = "Bird", Breed = "peguin", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/peguin/peguin bird 2.jpg" },
    new Pet { Name = "Kiwi the peguin", Type = "Bird", Breed = "peguin", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/peguin/2 peguins bird.jpg" },
    new Pet { Name = "Coco the peguin", Type = "Bird", Breed = "peguin", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/peguin/peguin bird.jpg" },
    new Pet { Name = "Sky the peguin", Type = "Bird", Breed = "peguin", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/peguin/2 peguins bird 2.jpg" },
    new Pet { Name = "Angel the Cockatiel ", Type = "Bird", Breed = "Cockatiel ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/cockatiel-/10.jpg" },
    new Pet { Name = "Rio the Cockatiel ", Type = "Bird", Breed = "Cockatiel ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/cockatiel-/12.jpg" },
    new Pet { Name = "Lemon the Cockatiel ", Type = "Bird", Breed = "Cockatiel ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/cockatiel-/11.jpg" },
    new Pet { Name = "Buddy the Cockatiel ", Type = "Bird", Breed = "Cockatiel ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/cockatiel-/2.jpg" },
    new Pet { Name = "Tweety the Cockatiel ", Type = "Bird", Breed = "Cockatiel ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/cockatiel-/14.jpg" },
    new Pet { Name = "Echo the Cockatiel ", Type = "Bird", Breed = "Cockatiel ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/cockatiel-/1.jpg" },
    new Pet { Name = "Sunny the Cockatiel ", Type = "Bird", Breed = "Cockatiel ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/cockatiel-/3.jpg" },
    new Pet { Name = "Kiwi the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/17.jpg" },
    new Pet { Name = "Coco the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/22.jpg" },
    new Pet { Name = "Sky the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/20.jpg" },
    new Pet { Name = "Angel the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/18.jpg" },
    new Pet { Name = "Rio the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/15.jpg" },
    new Pet { Name = "Lemon the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/16.jpg" },
    new Pet { Name = "Buddy the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/19.jpg" },
    new Pet { Name = "Tweety the African Grey Parrot", Type = "Bird", Breed = "African Grey Parrot", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/african-grey-parrot/21.jpg" },
    new Pet { Name = "Echo the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/32.jpg" },
    new Pet { Name = "Sunny the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/31.jpg" },
    new Pet { Name = "Kiwi the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/5.jpg" },
    new Pet { Name = "Coco the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/7.jpg" },
    new Pet { Name = "Sky the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/9.jpg" },
    new Pet { Name = "Angel the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/33.jpg" },
    new Pet { Name = "Rio the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/4.jpg" },
    new Pet { Name = "Lemon the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/8.jpg" },
    new Pet { Name = "Buddy the Budgie (Parakeet)", Type = "Bird", Breed = "Budgie (Parakeet)", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/budgie-(parakeet)/6.jpg" },
    new Pet { Name = "Tweety the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/28.jpg" },
    new Pet { Name = "Echo the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/27.jpg" },
    new Pet { Name = "Sunny the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/29.jpg" },
    new Pet { Name = "Kiwi the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/24.jpg" },
    new Pet { Name = "Coco the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/30.jpg" },
    new Pet { Name = "Sky the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/23.jpg" },
    new Pet { Name = "Angel the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/26.jpg" },
    new Pet { Name = "Rio the Lovebird", Type = "Bird", Breed = "Lovebird", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/birds/lovebird/25.jpg" },
    new Pet { Name = "Bailey the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f34c7fce0.jpg" },
    new Pet { Name = "Lucy the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f499a50c9.jpg" },
    new Pet { Name = "Sadie the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f4f61f058.jpg" },
    new Pet { Name = "Bella the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f43869e2d.jpg" },
    new Pet { Name = "Charlie the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f38e22d56.jpg" },
    new Pet { Name = "Max the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f5dd45aff.jpg" },
    new Pet { Name = "Buddy the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f3f4a0a2d.jpg" },
    new Pet { Name = "Cooper the Golden Retriever", Type = "Dog", Breed = "Golden Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/golden-retriever/[GetPaidStock.com]-67c8f56957033.jpg" },
    new Pet { Name = "Rocky the Pit Bull", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/pit-bull/[GetPaidStock.com]-67c90cbf98e11.jpg" },
    new Pet { Name = "Daisy the Pit Bull", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/pit-bull/[GetPaidStock.com]-67c90b9c16ff0.jpg" },
    new Pet { Name = "Bailey the Pit Bull", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/pit-bull/[GetPaidStock.com]-67c90c3519b2a.jpg" },
    new Pet { Name = "Lucy the Pit Bull", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/pit-bull/[GetPaidStock.com]-67c90b6d618c3.jpg" },
    new Pet { Name = "Sadie the Pit Bull", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/pit-bull/[GetPaidStock.com]-67c90bdf9fa7c.jpg" },
    new Pet { Name = "Bella the Pit Bull", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/pit-bull/[GetPaidStock.com]-67c90c7e6d155.jpg" },
    new Pet { Name = "Charlie the Pit Bull", Type = "Dog", Breed = "Pit Bull", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/pit-bull/[GetPaidStock.com]-67c90d11e5c75.jpg" },
    new Pet { Name = "Max the Labrador Retriever", Type = "Dog", Breed = "Labrador Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/labrador-retriever/[GetPaidStock.com]-67c8fc749db21.jpg" },
    new Pet { Name = "Buddy the Labrador Retriever", Type = "Dog", Breed = "Labrador Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/labrador-retriever/[GetPaidStock.com]-67c8f9a926f67.jpg" },
    new Pet { Name = "Cooper the Labrador Retriever", Type = "Dog", Breed = "Labrador Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/labrador-retriever/[GetPaidStock.com]-67c8f75f9d338.jpg" },
    new Pet { Name = "Rocky the Labrador Retriever", Type = "Dog", Breed = "Labrador Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/labrador-retriever/[GetPaidStock.com]-67c8fbec3d9be.jpg" },
    new Pet { Name = "Daisy the Labrador Retriever", Type = "Dog", Breed = "Labrador Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/labrador-retriever/[GetPaidStock.com]-67c8ff9471b99.jpg" },
    new Pet { Name = "Bailey the Labrador Retriever", Type = "Dog", Breed = "Labrador Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/labrador-retriever/[GetPaidStock.com]-67c8feb61f645.jpg" },
    new Pet { Name = "Lucy the Labrador Retriever", Type = "Dog", Breed = "Labrador Retriever", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/labrador-retriever/[GetPaidStock.com]-67c8fa6c98e5c.jpg" },
    new Pet { Name = "Sadie the Doberman ", Type = "Dog", Breed = "Doberman ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/doberman-/[GetPaidStock.com]-67c9098ff1958.jpg" },
    new Pet { Name = "Bella the Doberman ", Type = "Dog", Breed = "Doberman ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/doberman-/[GetPaidStock.com]-67c9091c04a2d.jpg" },
    new Pet { Name = "Charlie the Doberman ", Type = "Dog", Breed = "Doberman ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/doberman-/[GetPaidStock.com]-67c908adabdd8.jpg" },
    new Pet { Name = "Max the Doberman ", Type = "Dog", Breed = "Doberman ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/doberman-/[GetPaidStock.com]-67c908458665a.jpg" },
    new Pet { Name = "Buddy the Doberman ", Type = "Dog", Breed = "Doberman ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/doberman-/[GetPaidStock.com]-67c90870654fe.jpg" },
    new Pet { Name = "Cooper the Doberman ", Type = "Dog", Breed = "Doberman ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/doberman-/[GetPaidStock.com]-67c9073029e5d.jpg" },
    new Pet { Name = "Rocky the Doberman ", Type = "Dog", Breed = "Doberman ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/doberman-/[GetPaidStock.com]-67c9079b6eba9.jpg" },
    new Pet { Name = "Daisy the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/[GetPaidStock.com]-67c9027e39528.jpg" },
    new Pet { Name = "Bailey the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/[GetPaidStock.com]-67c902f0dd04a.jpg" },
    new Pet { Name = "Lucy the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/fc1ecfc8-a440-4e13-a820-28c31a8c8d11.jpg" },
    new Pet { Name = "Sadie the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/[GetPaidStock.com]-67c9013e388a3.jpg" },
    new Pet { Name = "Bella the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/[GetPaidStock.com]-67c900bab3921.jpg" },
    new Pet { Name = "Charlie the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/[GetPaidStock.com]-67c9019eaa87a.jpg" },
    new Pet { Name = "Max the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/[GetPaidStock.com]-67c9036c24a7b.jpg" },
    new Pet { Name = "Buddy the Rottweiler", Type = "Dog", Breed = "Rottweiler", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/rottweiler/[GetPaidStock.com]-67c9022e0ba41.jpg" },
    new Pet { Name = "Cooper the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/b9af079144df6e22ce6e924db6eef1ab.jpg" },
    new Pet { Name = "Rocky the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/321f58b5e8c6acc8d35c0c75169179c7.jpg" },
    new Pet { Name = "Daisy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/39c9763ab98474b245ecf955336ff541.jpg" },
    new Pet { Name = "Bailey the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/bc14a6be94335113a644d59a739297ef.jpg" },
    new Pet { Name = "Lucy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/b82a96d9e48b682468158d094b1d92a0.jpg" },
    new Pet { Name = "Sadie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/64dd946dbf6137639a6aac4c13973ec9.jpg" },
    new Pet { Name = "Bella the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/24190fa8f2d24278f5a738c0d3ac7260.jpg" },
    new Pet { Name = "Charlie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/0c390a6f447873687b9a31475b8cfc51.jpg" },
    new Pet { Name = "Max the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/adc8f878159018ef2789d06b5764cf10.jpg" },
    new Pet { Name = "Buddy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/aed28a8251b1f097c0e52f912b38f5f2.png" },
    new Pet { Name = "Cooper the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/b07c95ecb96d9cb83ce1b1f017541e42.jpg" },
    new Pet { Name = "Rocky the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/07c641b91d5d24b2e54b824466fd76e0.jpg" },
    new Pet { Name = "Daisy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/9f50d8a56247674687a0c282bb1ebb7c.jpg" },
    new Pet { Name = "Bailey the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/34adbb4803684050904a20a1274555f3.jpg" },
    new Pet { Name = "Lucy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/9f58398655781858369b57fcd9971a3b.png" },
    new Pet { Name = "Sadie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/335a69217a7dbed6de0e03395974c655.jpg" },
    new Pet { Name = "Bella the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/d9f078cdbf55627f58c003b0780542f2.jpg" },
    new Pet { Name = "Charlie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/b2a7f444bb0c59534213f992f88f5b87.jpg" },
    new Pet { Name = "Max the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/2f9de0f5272d6b43da465c522a1867d8.png" },
    new Pet { Name = "Buddy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/8e245803368e2d26aa9fd157423dc6f0.png" },
    new Pet { Name = "Cooper the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/7cfc226a6bdc484459eddac97dfe145a.jpg" },
    new Pet { Name = "Rocky the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/7608b8484b75bc20c213d89ad68291db.jpg" },
    new Pet { Name = "Daisy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/b5bb69b79638b5400c9c180e3bb2de87.jpg" },
    new Pet { Name = "Bailey the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/20241016115207.jpg" },
    new Pet { Name = "Lucy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/3a0d568a119c1aa7fba447e21bac071c.jpg" },
    new Pet { Name = "Sadie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/47e5262104e4968215e51a236ea405a6.jpg" },
    new Pet { Name = "Bella the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/07b35abf7d8555bc8c21680a4d53d7a0.jpg" },
    new Pet { Name = "Charlie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/9da1fc5b3d434da6db7cc952861f1cb7.png" },
    new Pet { Name = "Max the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/20240707111138.jpg" },
    new Pet { Name = "Buddy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/729f3073000367458d01d7ee489f43b8.jpg" },
    new Pet { Name = "Cooper the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/bd355d187cbd28e6d78a1d0ca310569c.jpg" },
    new Pet { Name = "Rocky the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/dc2303077f8c1c4fffd49cadcfa887be.jpg" },
    new Pet { Name = "Daisy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/48943f9e648d826e581597b730d01da0.jpg" },
    new Pet { Name = "Bailey the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/5193eb8c3c01cd05ef79ad2e9599d9c7.jpg" },
    new Pet { Name = "Lucy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/8c5353d18983fa2e73309a36e578a9ab.jpg" },
    new Pet { Name = "Sadie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/fa7551c4098fd79d50e051ab55e60d72.jpg" },
    new Pet { Name = "Bella the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/20240829093849.jpg" },
    new Pet { Name = "Charlie the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/1624f1e716db5f19a51ddf7a5eab4321.jpg" },
    new Pet { Name = "Max the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/b70fb076470d5ca647cbabf4053a4b84.jpg" },
    new Pet { Name = "Buddy the photos with the same Dimensions ", Type = "Dog", Breed = "photos with the same Dimensions ", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/photos-with-the-same-dimensions-/a3741f68f69b5030e659edf01ff28840.jpg" },
    new Pet { Name = "Cooper the German Shepherd", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/german-shepherd/[GetPaidStock.com]-67c8f1caad15c.jpg" },
    new Pet { Name = "Rocky the German Shepherd", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/german-shepherd/[GetPaidStock.com]-67c8f17db54e3.jpg" },
    new Pet { Name = "Daisy the German Shepherd", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/german-shepherd/[GetPaidStock.com]-67c8f217c55cc.jpg" },
    new Pet { Name = "Bailey the German Shepherd", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/german-shepherd/[GetPaidStock.com]-67c8f0291491b.jpg" },
    new Pet { Name = "Lucy the German Shepherd", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/german-shepherd/[GetPaidStock.com]-67c8eeefb31c9.jpg" },
    new Pet { Name = "Sadie the German Shepherd", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/german-shepherd/[GetPaidStock.com]-67c8f117c2322.jpg" },
    new Pet { Name = "Bella the German Shepherd", Type = "Dog", Breed = "German Shepherd", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/german-shepherd/[GetPaidStock.com]-67c8edf206d8a.jpg" },
    new Pet { Name = "Charlie the Baladi", Type = "Dog", Breed = "Baladi", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/baladi/Baladi.jpg" },
    new Pet { Name = "Max the Baladi", Type = "Dog", Breed = "Baladi", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/baladi/[GetPaidStock.com]-67c8e217569bb.jpg" },
    new Pet { Name = "Buddy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/3ea5eb4f-7237-406c-8765-2eac4aba6964.jpg" },
    new Pet { Name = "Cooper the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/09c2b891-81e7-489b-a7ae-dda7f84bf264.jpg" },
    new Pet { Name = "Rocky the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/3a3a3754-10ba-4fbc-a7ff-2358af79e414.jpg" },
    new Pet { Name = "Daisy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/1bb80117-d734-4954-99f8-e5e66b456183.jpg" },
    new Pet { Name = "Bailey the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/59e0c4bb-57d6-4c6d-b5cf-9646c69aa660.jpg" },
    new Pet { Name = "Lucy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/65f85688-e2ad-44ec-93bf-d38b39fb7190.jpg" },
    new Pet { Name = "Sadie the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/205c669d-70b1-4bd5-9134-a64fd8bca1be.jpg" },
    new Pet { Name = "Bella the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/82c11d9f-f6b3-408f-856c-8a261cc019b0.jpg" },
    new Pet { Name = "Charlie the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/b0f4bcb9-aea6-448d-87bb-67e6ab610962.jpg" },
    new Pet { Name = "Max the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/16f63ae1-19fb-411f-b7eb-1505c47f8c9d.jpg" },
    new Pet { Name = "Buddy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/449fba47-0317-4e1b-a1b6-3521b4e049b7.jpg" },
    new Pet { Name = "Cooper the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/7f8ad0e9-0b3b-4284-a703-f559566bf07e.jpg" },
    new Pet { Name = "Rocky the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/d860f26c-591f-4341-865d-071f63bd752e.jpg" },
    new Pet { Name = "Daisy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/74b64b93-dfcb-4c54-a7e7-aa0fb1c0cb73.jpg" },
    new Pet { Name = "Bailey the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/9083aab3-4199-4cf4-a95d-2581ee615fe9.jpg" },
    new Pet { Name = "Lucy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/a5ff3c48-727d-4b2e-bd5c-7a3706c6bb32.jpg" },
    new Pet { Name = "Sadie the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/6017de41-748d-4ea3-b468-b360c67bf41b.jpg" },
    new Pet { Name = "Bella the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/22d6fe4a-500e-4e1b-9d51-31664944077c.jpg" },
    new Pet { Name = "Charlie the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/71701d40-130b-40f5-931e-9663c48dd467.jpg" },
    new Pet { Name = "Max the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/93ef760c-7dd3-4164-a532-4854189c3d5e.jpg" },
    new Pet { Name = "Buddy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/eeb09e9e-fecc-4682-bb26-eb3a7bac63cc.jpg" },
    new Pet { Name = "Cooper the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/cbefe758-9be1-4c2e-9161-eabbfbf89edf.jpg" },
    new Pet { Name = "Rocky the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/f4c9ec1c-3dff-482a-a7fc-dcc1fe7b05e0.jpg" },
    new Pet { Name = "Daisy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/c79d55b5-e3ef-42e1-a336-40431741f141.jpg" },
    new Pet { Name = "Bailey the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/864c2423-aea7-4f53-ae98-4421fbcf2ea5.jpg" },
    new Pet { Name = "Lucy the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/8585d0f9-00ee-48ef-b507-c4ccacb4fe61.jpg" },
    new Pet { Name = "Sadie the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/d1b8c214-776b-4600-a318-52a7ca8f1451.jpg" },
    new Pet { Name = "Bella the Random Dogs", Type = "Dog", Breed = "Random Dogs", Age = 2, Size = "Medium", HealthStatus = "Healthy", GoodWith = "Kids", ImageUrl = "/images/pets/dogs/random-dogs/ff53dd94-a8bb-49fa-94ac-40c9a8637e70.jpg" },
};

                    context.Pets.AddRange(pets);
                    context.SaveChanges();

                }
            }

            // 6. Run the app
            app.Run();
        }
    }
}
