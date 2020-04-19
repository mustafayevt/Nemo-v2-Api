using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nemo_v2_Api.Hubs;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(x =>
            {
                x.UseNpgsql(Configuration.GetConnectionString("Npgsql"), e => e.MigrationsAssembly("Nemo v2 Api"));
            });
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

            // services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>)); 
            // services.AddScoped<IRepository<User>, EFUserRepository>();
            // services.AddScoped<IRepository<Role>, EFRoleRepository>();
            // services.AddScoped<IRepository<Restaurant>, EFRestaurantRepository>();
            // services.AddScoped<IRepository<Warehouse>,EFWarehouseRepository>();
            // services.AddScoped<IRepository<IngredientCategory>,EFIngredientCategoryRepository>();
            // services.AddScoped<IRepository<Ingredient>,EFIngredientRepository>();
            // services.AddScoped<IRepository<Section>,EFSectionRepository>();
            // services.AddScoped<IRepository<Table>,EFTableRepository>();
            // services.AddScoped<IRepository<Supplier>,EFSupplierRepository>();
            // services.AddScoped<IRepository<FoodGroup>,EFFoodGroupRepository>();
            // services.AddScoped<IRepository<Food>,EFFoodRepository>();
            // services.AddScoped<IRepository<IngredientsInsert>,EFIngredientsInsertRepository>();
            // services.AddScoped<IRepository<WarehouseInvoice>,EFWarehouseInvoiceRepository>();
            // services.AddScoped<IRepository<Printer>,EFPrinterRepository>();
            // services.AddScoped<IRepository<Invoice>,EFInvoiceRepository>();
            // services.AddScoped<IRepository<Buyer>,EFBuyerRepository>();
            // services.AddScoped<IRepository<IngredientsExport>,EFIngredientsExportRepository>();
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
            services.AddTransient<IProfitService, ProfitService>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            loggerFactory.AddFile("Logs/Errors/{Date}.txt", LogLevel.Error);
            loggerFactory.AddFile("Logs/Info/{Date}.txt");
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseSignalR(x => x.MapHub<TransferHub>("/Transfer"));
        }
    }
}