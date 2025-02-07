
using IdentityModel.Client;
using UmbracoBridge.Services;

namespace UmbracoBridge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            //**********
            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            //**********


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }

    }

    
}
