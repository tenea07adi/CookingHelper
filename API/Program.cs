using API.Controllers.ActionFilters;
using API.Factories;
using API.Interfaces;
using Core.Ports.Driven;
using Core.Ports.Driving;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.DataBase;
using Persistence.Repository;
using Persistence.Services;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilter));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MainDbContext")));

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // Persistence services
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddScoped<IDataBaseService, DataBaseService>();

            // Core services
            builder.Services.AddScoped(typeof(IGenericEntityService<>), typeof(GenericEntityService<>));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IInfrastructureUtilityService, InfrastructureUtilityService>();
            builder.Services.AddScoped<IIngredientsService, IngredientsService>();
            builder.Services.AddScoped<IPreparationStepsService, PreparationStepsService>();
            builder.Services.AddScoped<IRecipesServices, RecipesServices>();
            builder.Services.AddScoped<IGroceryListService, GroceryListService>();
            builder.Services.AddScoped<IGroceryListItemService, GroceryListItemService>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<ICryptographyService, CryptographyService>();

            builder.Services.AddScoped<ISessionInfoService, SessionInfoService>();

            // WebApi services
            builder.Services.AddScoped<IEnumValueDtoFactory, EnumValueDtoFactory>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 var jwtIssuer = builder.Configuration.GetSection("Security:Issuer").Get<string>();
                 var jwtSecretKey = builder.Configuration.GetSection("Security:SecretKey").Get<string>();

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtIssuer,
                     ValidAudience = jwtIssuer,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
                 };
             });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
