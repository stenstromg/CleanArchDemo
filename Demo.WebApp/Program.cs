using Demo.App.Interfaces;
using Demo.App.Interfaces.WebAPI;
using Demo.App.Services;
using Demo.App.Services.WebAPI;
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Repositories;
using Demo.Infrastructure.Repositories.WebAPI;
using Demo.Infrastructure.Services;
using Demo.WebApp.Classes.Services;
using Demo.WebApp.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace Demo.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddRadzenComponents();

            #region Configure JSON handling

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            #endregion JSON handling

            #region register database dependency

            ConfigurationManager configuration = builder.Configuration;
            string? connectionString = configuration.GetConnectionString("DefaultDbConnection");
            builder.Services.AddDbContext<DemoDbContext>(options => options.UseSqlServer(connectionString));

            #endregion register database dependency

            #region register custom services

            builder.Services.AddScoped<ApplicationConfigurationService>( arg => new ApplicationConfigurationService(configuration));
            builder.Services.AddScoped<IApplicationSessionService, ApplicationSessionService>();

            builder.Services.AddTransient<IContactApiRepository, ContactApiRepository>();
            builder.Services.AddTransient<IContactApiService, ContactApiService>();
            builder.Services.AddTransient<IUserApiRepository, UserApiRepository>();
            builder.Services.AddTransient<IUserApiService, UserApiService>();

            builder.Services.AddTransient<IWebAPIUtilities>(provider => new WebAPIUtilities(configuration));

            #endregion register custom services

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<AppMain>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
