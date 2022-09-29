namespace AlphaWebApi
{
    #region

    using AlphaWebApi.Services;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton<IBethaService, BethaService>();

            builder.Services.AddHttpClient("bethaService", c =>
            {
                c.BaseAddress = new Uri(builder.Configuration["Services:BethaService"]);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors(builder => builder
                .WithOrigins("http://localhost"));


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