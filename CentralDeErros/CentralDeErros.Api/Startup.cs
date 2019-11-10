using AutoMapper;
using CentralDeErros.Api.GraphQL;
using CentralDeErros.Application.ApplicationServices;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.Mapping;
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
            services.Configure<CentralDeErrosDatabaseSettings>(Configuration.GetSection(nameof(CentralDeErrosDatabaseSettings)));

            services.AddSingleton<ICentralDeErrosDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CentralDeErrosDatabaseSettings>>().Value);

            services.AddScoped<IErrorLogService, ErrorLogService>();
            services.AddScoped<IErrorLogAppService, ErrorLogAppService>();

            services.AddAutoMapper(typeof(AutoMappingDomainToViewModel));
            services.AddAutoMapper(typeof(AutoMappingViewModelToDomain));

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CentralDeErrosSchema>();
            
            services.AddGraphQL(o =>
            {
                o.ExposeExceptions = true; // it can't be true in production
            }).AddGraphTypes(ServiceLifetime.Scoped);

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
