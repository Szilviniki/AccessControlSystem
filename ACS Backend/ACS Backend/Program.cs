using ACS_Backend.Interfaces;
using ACS_Backend.Services;
using Microsoft.EntityFrameworkCore;
using FluentScheduler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ACS_Backend
{
    public static class Program
    {

        // public static byte[] JwtKey { get; private set; }
        // public static string JwtIssuer { get; private set; }
        // public static string JwtAudience { get; private set; }
        public static void Main(string[] args)
        {
            const string origin = "_allowed";
            var builder = WebApplication.CreateBuilder(args);
            // JwtKey = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key"));

        public static byte[] TokenEncryptionKey { get; private set; }
        public static void Main(string[] args)
        {  
            var origin = "_allowed";
            var builder = WebApplication.CreateBuilder(args);
            TokenEncryptionKey = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key"));
            SQL.connectionString = builder.Configuration.GetConnectionString("REMOTE");


            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: origin,
                                  policy =>
                                  {
                                      policy.WithOrigins("127.0.0.1", "localhost");
                                  });
            });

            // builder.Services.AddAuthentication()
            //     .AddJwtBearer(a =>
            // {
            //     a.RequireHttpsMetadata = false;
            //     a.SaveToken = true;
            //     a.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateAudience = true,
            //         ValidateIssuer = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(JwtKey),
            //         ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),
            //         ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer")
            //     };
            // });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SQL>(a => { a.UseSqlServer(SQL.connectionString); });
            builder.Services.AddTransient<IHomepageService, HomepageService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICheckInService, CheckInService>();
            builder.Services.AddScoped<IPersonnelService, PersonnelService>();
            builder.Services.AddScoped<IEncryptionService, EncryptionService>();
            builder.Services.AddScoped<IGuardianService, GuardianService>();
            builder.Services.AddScoped<IRestrictionService, RestrictionService>();
            // builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddSingleton<IMatchingService, MatchingService>();


            // builder.Services.AddSingleton<IScheduledTasksService, SchedueldTaskService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(origin);
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}