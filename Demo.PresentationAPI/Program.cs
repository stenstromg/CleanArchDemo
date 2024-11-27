using Demo.App.Interfaces;
using Demo.App.Services;
using Microsoft.EntityFrameworkCore;

// In order to build the migrations and inject the Database context, we need the reference to the
// Infrastructure layer. Plus the repositories we need to fill the requests are defined here.
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Repositories;


namespace Demo.PresentationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register Configuration
            //

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options=>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region register database dependency

            ConfigurationManager configuration = builder.Configuration;
            string? connectionString = configuration.GetConnectionString("DefaultDbConnection");

            builder.Services.AddDbContext<DemoDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(connectionString, param => param.MigrationsAssembly("Demo.PresentationAPI"));
            });

            #endregion register database dependency

            #region inject our services

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            //builder.Services.AddScoped<IAuthenticatorService, AuthenticatorService>();

            builder.Services.AddTransient<IUserLoginService, UserLoginService>();
            builder.Services.AddTransient<IUserLoginRepository, UserLoginRepository>();

            builder.Services.AddTransient<IPersonRepository, PersonRepository>();
            builder.Services.AddTransient<IPersonService, PersonService>();

            builder.Services.AddTransient<IContactRepository, ContactRepository>();
            builder.Services.AddTransient<IContactService, ContactService>();

            #endregion inject our services

            var app = builder.Build();

            #region make sure database is built

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DemoDbContext>();
                dbContext.Database.EnsureCreated();
            }

            #endregion make sure database is built

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
