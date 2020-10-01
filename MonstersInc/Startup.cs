using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MonstersInc.Auth;
using MonstersInc.IntimidatorIntimidationsCache;
using MonstersIncData;
using MonstersIncDomain;
using MonstersIncLogic;
using System;
using System.Collections.Generic;

namespace MonstersInc
{
    public class Startup
    {
        public static IConfiguration Configuration;

        [System.Obsolete]
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment i_env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(i_env.ContentRootPath)
                .AddJsonFile($"appsettings.{ i_env.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {


            var authBuilder = services.AddIdentityServer()
                               .AddInMemoryApiResources(Config.Apis)

                               //  .AddInMemoryClients(Config.Clients)
                               .AddClientStore<AuthClientStore>()
                               .AddInMemoryIdentityResources(Config.Ids);

            authBuilder.AddDeveloperSigningCredential();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                    .AddJwtBearer(options =>
                                    {
                                        options.Authority = "https://localhost:44309";
                                        options.RequireHttpsMetadata = false;
                                        options.Audience = "api1";
                                    });


            services.AddMvc();

            services.Configure<IConfiguration>(Configuration);

            services.AddDbContext<MonstersIncContext>(o =>
            {
                o.UseSqlServer(Configuration["Db:MonstersInc"]);
            });

            services.AddSingleton<IntimidatorIntimidationsCacheChannel>();


            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IintimidationRepository, IntimidationRepository>();
            services.AddScoped<IDoorRepository, DoorRepository>();
            services.AddScoped<IintimidatorWorkdayRepository, IntimidatorWorkdayRepository>();
            services.AddScoped<IintimidatorRepository, IntimidatorRepository>();

            services.AddDistributedMemoryCache();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHostedService<IntimidatorIntimidationsCacheService>();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <br>
                      Enter 'Bearer' [space] and then your token in the text input below.
                      <br>Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                      {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                        });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonstersInc API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseIdentityServer();


            app.UseMiddleware<RequestLoggingMiddleware>();


            if (env.IsDevelopment())
            {
                //   app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseRouting();


            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
