using Assessment.Application.Interfaces;
using Assessment.Application.Services;
using Assessment.Infrastructure.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assessment.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            string sqlConnection = _configuration.GetConnectionString("MySqlConnection");

            string connectionString = sqlConnection + password;

            services.AddDbContext<AppDbContext>(opt => opt.UseMySQL(sqlConnection));

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(MappingProfileService));

            services.AddControllers();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddMvc(x => x.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthorization();

            new MapperConfiguration(mapper =>
            {
                mapper.AddProfile<MappingProfileService>();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //    name: "default",
            //        pattern: "{controller=" + _controller + "}/{action=" + _home + "}/{id?}");
            //});
        }
    }
}
