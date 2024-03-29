﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniMart.API.Extensions;
using MiniMart.API.Jobs;
using MiniMart.Domain.Models;
using Quartz;
using System.Security.Claims;
using System.Text;

namespace MiniMart.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MiniMart_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.SecretKey)),

                };
            });
            services.AddHttpContextAccessor();
            services.AddScoped<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext(Configuration);
            services.AddGenericRepositories();
            services.AddServices(Configuration);
            services.AddUnitOfWork();
            services.RegisterMediator();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                q.AddJob<PriceDecreaseStrategyJob>(opts => opts.WithIdentity("PriceDecreaseStrategy"));
                q.AddTrigger(opts => opts
                 .ForJob("PriceDecreaseStrategy")
                 .WithIdentity("PriceDecreaseStrategy-trigger")
                 .WithCronSchedule("*/6 * * * * ?"));

                q.AddJob<PriceDecreaseStrategyExpiredJob>(opts => opts.WithIdentity("PriceDecreaseStrategyExpired"));
                q.AddTrigger(opts => opts
                 .ForJob("PriceDecreaseStrategyExpired")
                 .WithIdentity("PriceDecreaseStrategyExpired-trigger")
                 .WithCronSchedule("*/6 * * * * ?"));
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
