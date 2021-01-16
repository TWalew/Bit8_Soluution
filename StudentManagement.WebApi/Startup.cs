using System.Globalization;
using System.Linq;
using StudentManagement.Common;
using StudentManagement.Entities;
using StudentManagement.Query;
using StudentManagement.Query.Disciplines;
using StudentManagement.Query.Semesters;
using StudentManagement.Query.Students;
using StudentManagement.Services;
using StudentManagement.Services.Disciplines;
using StudentManagement.Services.Semesters;
using StudentManagement.Services.Students;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StudentManagement.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc()
                .AddFluentValidation()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = c =>
                    {
                        var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                            .SelectMany(v => v.Errors)
                            .Select(v => v.ErrorMessage)
                            .ToArray();

                        return new BadRequestObjectResult(errors);
                    };
                });
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

            services.AddTransient<IBConfiguration>(x => new BConfiguration(Configuration));

            services.AddTransient<IDisciplineService, DisciplineService>();
            services.AddTransient<ISemesterService, SemesterService>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentQuery, StudentQuery>();
            services.AddScoped<ISemesterQuery, SemesterQuery>();
            services.AddScoped<IDisciplineQuery, DisciplineQuery>();

            services.AddTransient<IValidator<CreateDisciplineRequest>, CreateDisciplineRequestValidator>();
            services.AddTransient<IValidator<CreateSemesterRequest>, CreateSemesterRequestValidator>();
            services.AddTransient<IValidator<AssignToSemesterRequest>, AssignToSemesterRequestValidator>();
            services.AddTransient<IValidator<CreateStudentRequest>, CreateStudentRequestValidator>();
            services.AddTransient<IValidator<UpdateStudentRequest>, UpdateStudentRequestValidator>();
            services.AddTransient<IValidator<UpdateDisciplineRequest>, UpdateDisciplineRequestValidator>();
            services.AddTransient<IValidator<UpdateSemesterRequest>, UpdateSemesterRequestValidator>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;


            // Enable Cors
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}