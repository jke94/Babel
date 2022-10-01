namespace BethaWebApi
{
    using Betha.WebApi.HostedServices;
    #region

    using BethaWebApi.Services;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddSingleton<IAlphaService, AlphaService>();
            builder.Services.AddSingleton<ISomeWorkHostedService, SomeWorkHostedService>();

            builder.Services.AddHttpClient("alphaService", c =>
            {
                c.BaseAddress = new Uri(builder.Configuration["Services:AlphaService"]);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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