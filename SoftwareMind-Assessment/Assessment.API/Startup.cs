using Assessment.API.Models;
using Assessment.Application.Interfaces;
using Assessment.Application.Services;
using Assessment.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
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
            string sqlConnection = _configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(opt => opt.UseMySQL(sqlConnection));

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

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

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //    name: "default",
            //        pattern: "{controller=" + _controller + "}/{action=" + _home + "}/{id?}");
            //});

            //var configuration = new MapperConfiguration(mapper =>
            //{
            //    //mapper.CreateMap<Pedido, PedidoViewModel>().ReverseMap();

            //    mapper.AddProfile<MappingProfileService>();
            //});
        }
    }
}
