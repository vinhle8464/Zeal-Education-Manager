using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3
{
    public class Startup
    {
        private IConfiguration iConfiguration;

        public Startup(IConfiguration _iConfiguration)
        {
            iConfiguration = _iConfiguration;
        }



        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddSession();
            //services.AddCookiePolicy();
            var connectionString = iConfiguration.GetConnectionString("DefaultConnection");
       

            //service advertisement
            services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));
            services.AddScoped<IAccountService, AccountServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IRolesService, Areas.Admin.Service.RoleServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IClassesService, Areas.Admin.Service.ClassesServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IAttendanceService, Areas.Admin.Service.AttendanceServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IBatchesService, Areas.Admin.Service.BatchesServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IClassAssignmentService, Areas.Admin.Service.ClassAssignmentServiceImpl>();
            services.AddScoped<Areas.Admin.Service.ICoursesService, Areas.Admin.Service.CoursesServiceImpl>(); services.AddScoped<Areas.Admin.Service.IProfessionalsService, Areas.Admin.Service.ProfessionalsServiceImpl>();
            services.AddScoped<Areas.Admin.Service.ISubjectsService, Areas.Admin.Service.SubjectsServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IExamsService, Areas.Admin.Service.ExamsServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IScholarshipService, Areas.Admin.Service.ScholarshipServiceImpl>();
            services.AddScoped<Areas.Admin.Service.ICourseSubjectService, Areas.Admin.Service.CourseSubjectServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IScholarshipStudentService, Areas.Admin.Service.ScholarshipStudentServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IFeedBackService, Areas.Admin.Service.FeedBackServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IFeedbackFacultyService, Areas.Admin.Service.FeedbackFacultyServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IBatchService, Areas.Admin.Service.BatchServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IFinanceService, Areas.Admin.Service.FinanceServiceImpl>();
            services.AddScoped<Areas.Admin.Service.IStudentService, Areas.Admin.Service.StudentServiceImpl>();


            
            services.AddScoped<Areas.Faculty.Service.IMarkingService, Areas.Faculty.Service.MarkingServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IDashboardService, Areas.Faculty.Service.DashboardServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IAttendanceService, Areas.Faculty.Service.AttendanceServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IFeedbackService, Areas.Faculty.Service.FeedbackServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IStudentService, Areas.Faculty.Service.StudentServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IScheduleService, Areas.Faculty.Service.ScheduleServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IExaminationService, Areas.Faculty.Service.ExaminationServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IBatchService, Areas.Faculty.Service.BatchServiceImpl>();
            services.AddScoped<Areas.Faculty.Service.IFinanceService, Areas.Faculty.Service.FinanceServiceImpl>();


            services.AddScoped<IProfileService, ProfileServiceImpl>();
            services.AddScoped<IContactService, ContactServiceImpl>();
            services.AddScoped<IScheduleService, ScheduleServiceImpl>();
            services.AddScoped<ITestScheduleService, TestScheduleServiceImpl>();
            services.AddScoped<IMailService, MailServiceImpl>();
            services.AddScoped<IEnquiryService, EnquiryServiceImpl>();
            services.AddScoped<IPaysService, PaysServiceImpl>();
            services.AddScoped<IAttendanceService, AttendanceServiceImpl>();
            services.AddScoped<IMarkService, MarkServiceImpl>();
            services.AddScoped<IReportService, ReportServiceImpl>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                {
                    //Re-execute the request so the user gets the error page
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/error/404";
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthorization();
      
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=signin}"
                );
            });
        }
    }
}
