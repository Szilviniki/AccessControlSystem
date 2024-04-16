using ACS_Backend.Interfaces;
using ACS_Backend.Middlewares;
using ACS_Backend.Services;
using ACS_Backend.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ACS_Backend
{
    public static class Program
    {
        
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

            builder.Services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(a =>
            {
                a.SaveToken = true;
                a.RequireHttpsMetadata = true;
                a.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenEncryptionKey),
                    ValidateIssuerSigningKey = true
                };
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SQL>(a => { a.UseSqlServer(SQL.connectionString); });
            
            //middleware
            builder.Services.AddTransient<GlobalExceptionHandling>();
            
            //utilities
            builder.Services.AddScoped<IObjectValidatorService, ObjectValidatorService>();
            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddSingleton<IValidatorService, ValidatorService>();
            builder.Services.AddScoped<IEncryptionService, EncryptionService>();

            
            //services
            builder.Services.AddScoped<IUniquenessChecker, UniquenessChecker>();
            builder.Services.AddTransient<IHomepageService, HomepageService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICheckInService, CheckInService>();
            builder.Services.AddScoped<IPersonnelService, PersonnelService>();
            builder.Services.AddScoped<IEncryptionService, EncryptionService>();
            builder.Services.AddScoped<IGuardianService, GuardianService>();
            builder.Services.AddScoped<ILockRuleService, LockRuleService>();
            
         
            

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
            
            app.UseMiddleware<GlobalExceptionHandling>();
            
            app.MapControllers();
            app.Run();
        }
    }
}