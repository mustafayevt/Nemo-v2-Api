using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data.AutoMapper;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Repositories;
using Nemo_v2_Repo.Repositories.EFRepository;
using Nemo_v2_Service.Abstraction;
using Nemo_v2_Service.Services;

namespace Nemo_v2_Api
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
            services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(Configuration.GetConnectionString("Npgsql")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nemo Api", Version = "2" });
                c.AddSecurityDefinition("x-api-key", new OpenApiSecurityScheme
                {
                    Description =
                        "Api Key for authorize to the API",
                    Name = "x-api-key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "x-api-key"
                            },
                            Name = "x-api-key",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                // c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>)); 
            services.AddScoped<IRepository<User>, EFUserRepository>();
            services.AddScoped<IRepository<Role>, EFRoleRepository>();
            services.AddScoped<IRepository<Restaurant>, EFRestaurantRepository>();
            services.AddScoped<IRepository<Warehouse>,EFWarehouseRepository>();
            services.AddScoped<IRepository<IngredientCategory>,EFIngredientCategoryRepository>();
            services.AddScoped<IRepository<Ingredient>,EFIngredientRepository>();
            
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IIngredientCategoryService, IngredientCategoryService>();
            services.AddTransient<IIngredientService, IngredientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            loggerFactory.AddFile("Logs/Errors/{Date}.txt",LogLevel.Error);
            loggerFactory.AddFile("Logs/Info/{Date}.txt",LogLevel.Information);
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
