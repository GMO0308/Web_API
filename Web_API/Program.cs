
using MongoDB.Driver;
using Web_API.Models;

namespace Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // MongoDB Configuration
            builder.Services.AddSingleton<IMongoClient>(_ =>
             new MongoClient(builder.Configuration["Mongo: ConnnectionString"]));

            builder.Services.AddSingleton<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(builder.Configuration["Mongo:Database"]);
            });

            builder.Services.AddSingleton<IMongoCollection<DiaryEntries>>(sp =>
            {
                var db = sp.GetRequiredService<IMongoDatabase>();
                return db.GetCollection<Models.DiaryEntries>(builder.Configuration["Mongo:Collection"]);
            });
            // End MongoDB Configuration
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
