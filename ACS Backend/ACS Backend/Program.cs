using Microsoft.EntityFrameworkCore;
using FluentScheduler;
using Microsoft.IdentityModel.Tokens;

namespace ACS_Backend
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Registry registry = new Registry();

            /*


             ScheduledTasks st = new ScheduledTasks(sql);

             st.EveryoneOut();
             registry.Schedule(st.EveryoneOut).ToRunEvery(0).Weeks().On(DayOfWeek.Saturday).At(09, 00);*/

            var builder = WebApplication.CreateBuilder(args);

            SQL.connectionString = builder.Configuration.GetConnectionString("REMOTE");

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name:AllowedOrigins, policy =>
                {
                    policy.WithOrigins("127.0.0.1");
                });
            });

            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICheckInService, CheckInService>();
            builder.Services.AddScoped<IFacultyService, FacultyService>();




            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SQL>(a =>
            {
                a.UseSqlServer(SQL.connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(AllowedOrigins);

            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}