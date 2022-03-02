using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using VacationBookerAPI.Helpers;
using VacationBookerAPI.Interfaces;
using VacationBookerAPI.Migrations;
using VacationBookerAPI.Repositories;

namespace VacationBookerAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private const string PolicyName = "CorsPolicy";

        public Startup(IConfiguration config)
        {
            _config = config;
            //Configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: PolicyName, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //Usually called the dependency injection container...

            //Set up our data connection
            services.AddDbContext<DataContext>(options =>
            {
                //options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddControllers(options => { options.InputFormatters.Insert(0, GetJsonPatchInputFormatter()); });
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"}); });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(PolicyName);
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}