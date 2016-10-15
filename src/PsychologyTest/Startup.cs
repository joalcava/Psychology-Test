using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PsychologyTest.Models;

namespace PsychologyTest
{
    public class Startup
    {
        private IHostingEnvironment _env;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            // TODO: Agregar servicio controlador de Emails

            services.AddMvc(config =>
            {
                //if (_env.IsProduction()) {
                //    config.Filters.Add(new RequireHttpsAttribute());
                //}
            }).AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddIdentity<PsyTestUser, IdentityRole>(config =>
            {
                //User
                config.User.RequireUniqueEmail = true;

                //Password
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = true;
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireLowercase = false;

                //Cookies
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/login";
            })
            .AddEntityFrameworkStores<PsyTestContext>()
            .AddDefaultTokenProviders();

            services.AddDbContext<PsyTestContext>();
            services.AddScoped<IPsyTestRepository, PsyTestRepository>();
            services.AddTransient<PsyTestSeedData>();
            services.AddLogging();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("EmailConfirmedPolicy", policy =>
                {
                    policy.RequireClaim("emailconfirmation", "1");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            PsyTestSeedData seeder,
            ILoggerFactory loggerFactory)
        {
            //TODO: Crear mi propio mapper
            Mapper.Initialize(config =>
            {
                config.CreateMap<ViewModels.CreateUserViewModel, PsyTestUser>().ReverseMap();
                config.CreateMap<ViewModels.RootEditUserViewModel, PsyTestUser>().ReverseMap();
                config.CreateMap<ViewModels.RootCreateUserViewModel, PsyTestUser>().ReverseMap();
                config.CreateMap<ViewModels.GrupoViewModel, Grupo>().ReverseMap();
                config.CreateMap<ViewModels.InstitucionViewModel, Institucion>().ReverseMap();
                config.CreateMap<DeletedUsers, PsyTestUser>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseStaticFiles();
            app.UseIdentity();
            
            //if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                loggerFactory.AddDebug(LogLevel.Information);
            //}
            //else {
            //    app.UseExceptionHandler("/Home/Error");
            //    loggerFactory.AddDebug(LogLevel.Error);
            //}

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //seeder.SeedDataTask().Wait();
        }
    }
}
