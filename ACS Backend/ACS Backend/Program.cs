using ACS_Backend.Interfaces;
using ACS_Backend.Services;
using Microsoft.EntityFrameworkCore;
using FluentScheduler;

namespace ACS_Backend
{
    public class Program
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SQL>(a => { a.UseSqlServer(SQL.connectionString); });
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICheckInService, CheckInService>();
            builder.Services.AddScoped<IFacultyService, FacultyService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}