using ACS_Backend.Interfaces;
using ACS_Backend.Services;
using Microsoft.EntityFrameworkCore;
using FluentScheduler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ACS_Backend
{
    public static class Program
    {
        public static byte[] TokenEncryptionKey { get; private set; }
        public static void Main(string[] args)
        {
            Registry registry = new Registry();
       /*     var sts = new SchedueldTaskService(new SQL());
            registry.Schedule<>(a => a.EveryoneOut()).ToRunNow().AndEvery(1).Minutes();*/
            var origin = "_allowed";
            var builder = WebApplication.CreateBuilder(args);
            TokenEncryptionKey = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("TokenEncryptionKey"));
            SQL.connectionString = builder.Configuration.GetConnectionString("REMOTE");


            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: origin,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://example.com",
                                                          "http://www.contoso.com");
                                  });
            });

            builder.Services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(a =>
            {
                a.SaveToken = true;
                a.RequireHttpsMetadata = true;
                a.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenEncryptionKey),
                    ValidateIssuerSigningKey = true
                };
            });

            using (SQL sql = new SQL())
            {
                foreach (Role r in sql.PersonRoles)
                {
                    builder.Services.AddAuthorization(a =>
                    {
                        a.AddPolicy(r.Name, o => { o.RequireRole(r.Id.ToString()); });
                    });
                }
            }

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SQL>(a => { a.UseSqlServer(SQL.connectionString); });
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICheckInService, CheckInService>();
            builder.Services.AddScoped<IFacultyService, FacultyService>();
            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddScoped<IEncryptionService, EncryptionService>();
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