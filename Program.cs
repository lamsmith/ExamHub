using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Implementation;
using ExamHub.Repositories.Implementations;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Implementations;
using ExamHub.Services.Inteface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ExamHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>
                (options => options.UseMySQL(builder.Configuration.GetConnectionString("ExamHubString")));



            builder.Services.AddScoped<IClassRepository, ClassRepository>();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<IExamQuestionRepository, ExamQuestionRepository>();
            builder.Services.AddScoped<IExamQuestionService, ExamQuestionService>();
            builder.Services.AddScoped<IExamRepository, ExamRepository>();
            builder.Services.AddScoped<IExamService, ExamService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<ISubjectTeacherRepository, SubjectTeacherRepository>();
            builder.Services.AddScoped<IClassTeacherRepository, ClassTeacherRepository>();
            builder.Services.AddScoped<ISubjectTeacherService, SubjectTeacherService>();
            builder.Services.AddScoped<IClassTeacherService, ClassTeacherService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();








           // Add services to the container.
           builder.Services.AddControllersWithViews();


            // Configure cookie authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
               name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}");

          


            app.Run();
        }
    }
}
