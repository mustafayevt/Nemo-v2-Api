using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nemo_v2_Api.Hubs;
using Nemo_v2_Api.Middlewares;
using Nemo_v2_Data.AutoMapper;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Repositories.EFRepository;
using Nemo_v2_Repo.UnitOfWork;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(x =>
            {
                x.UseNpgsql(Configuration.GetConnectionString("Npgsql"), e => e.MigrationsAssembly("Nemo v2 Api"));
            });
            services.AddDbContext<HubTemporaryDataContext>(x => { x.UseSqlite(@"Data Source=HubTemporaryData.db;"); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Nemo Api", Version = "2"});
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

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMappingProfile()); });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IIngredientCategoryService, IngredientCategoryService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IFoodGroupService, FoodGroupService>();
            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<IWarehouseInvoiceService, WarehouseInvoiceService>();
            services.AddTransient<IPrinterService, PrinterService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IBuyerService, BuyerService>();
            services.AddTransient<IWarehouseExportInvoiceService, WarehouseExportInvoiceService>();
            services.AddTransient<IWarehouseTransferInvoiceService, WarehouseTransferInvoiceService>();
            services.AddTransient<IProfitService, ProfitService>();
            services.AddTransient<IManualCurrencyModelService, ManualCurrencyModelService>();
            services.AddTransient<IInvoiceNumberManagerService, InvoiceNumberManagerService>();

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            loggerFactory.AddFile("Logs/Errors/{Date}.txt", LogLevel.Error);
            loggerFactory.AddFile("Logs/Info/{Date}.txt");
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HubTemporaryDataContext>();
                context.Database.EnsureCreated();
            }

            app.UseSignalR(x =>
            {
                x.MapHub<POSHub>("/POSHub", options =>
                {
                    options.Transports =
                        HttpTransportType.WebSockets |
                        HttpTransportType.LongPolling;
                });
                x.MapHub<WarehouseHub>("/WarehouseHub", options =>
                {
                    options.Transports =
                        HttpTransportType.WebSockets |
                        HttpTransportType.LongPolling;
                });
            });
        }
    }
}