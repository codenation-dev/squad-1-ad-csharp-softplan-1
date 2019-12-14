using AutoMapper;
using CentralDeErros.Api.GraphQL;
using CentralDeErros.Application.Services;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.Mapping;
using CentralDeErros.Data.Context;
using CentralDeErros.Data.Repositories;
using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Domain.Models;
using CentralDeErros.Domain.Services;
using CentralDeErros.CrossCutting.Helpers;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using CentralDeErros.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace CentralDeErros.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IUserRepository, UserRepository>();

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
            ConfigureAuth(services);

            services.AddControllers();

            services.AddCors();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Central de Erros API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var secretKeyJWT = Encoding.ASCII.GetBytes(appSettings.SecretKeyJWT);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.ClaimsIssuer = "api.centraldeerros.com";
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyJWT),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Central de Erros V1");
            });
        }
    }
}
