using AutoMapper;
using CentralDeErros.Api.GraphQL;
using CentralDeErros.Application.ApplicationServices;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.Mapping;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Data.Repositories;
using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Models;
using CentralDeErros.Domain.Services;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using CentralDeErros.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CentralDeErros.Api
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
            services.Configure<AuditDatabaseSettings>(Configuration.GetSection(nameof(AuditDatabaseSettings)));

            services.AddSingleton<IAuditDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AuditDatabaseSettings>>().Value);

            services.AddDbContext<CentralDeErrosContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IErrorLogService, ErrorLogService>();
            services.AddScoped<IErrorLogAppService, ErrorLogAppService>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();

            services.AddAutoMapper(typeof(AutoMappingDomainToViewModel));
            services.AddAutoMapper(typeof(AutoMappingViewModelToDomain));

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CentralDeErrosSchema>();
            
            services.AddGraphQL(options =>
            {
                options.ExposeExceptions = true; // it can't be true in production
            }).AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User);

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseGraphQL<CentralDeErrosSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
