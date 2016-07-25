using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Graduation.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Newtonsoft.Json.Serialization;
using Graduation.Infrastructure.Repositories.Abstract;
using Graduation.Infrastructure.Repositories;
using Graduation.Models;
using Graduation.Infrastructure;
using Graduation.Infrastructure.Mapping;

namespace Graduation
{
    public class Startup
    {
        private string _contentRootPath = string.Empty;

        public Startup(IHostingEnvironment env)
        {
            _contentRootPath = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                });

            // Add session related services.
            services.AddSession();

            // Add memory cache services
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            //services.ConfigureIdentity(Configuration.GetConfigurationSection("Identity"));

            //services.ConfigureIdentity(options =>
            //{
            //    options.Password.RequireDigit = bool.Parse(Configuration["Identity:Password:RequireDigit"].ToString());
            //    options.Password.RequiredLength = int.Parse(Configuration["Identity:Password:RequiredLength"].ToString());
            //    options.Password.RequireLowercase = bool.Parse(Configuration["Identity:Password:RequireLowercase"].ToString());
            //    options.Password.RequireNonLetterOrDigit = bool.Parse(Configuration["Identity:Password:RequireNonLetterOrDigit"].ToString());
            //    options.Password.RequireUppercase = bool.Parse(Configuration["Identity:Password:RequireUppercase"].ToString());
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Manager",
                    authBuilder =>
                    {
                        authBuilder.RequireClaim("Manager", "Allowed");
                    });
            });

            // Add framework services.
            services.AddDbContext<GraduationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GraduationDbContext>()
                .AddDefaultTokenProviders();

            AutoMapperConfiguration.Configure();

            services.AddMvc();
           
            // Repositories
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICateRepository, CateRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddScoped<ICateBlogRepository, CateBlogRepository>();
            services.AddScoped<ILoggingRepository, LoggingRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();

            services.AddScoped<IViewRepository, ViewRepository>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //use session
            app.UseSession();

            app.UseFileServer();

            var provider = new PhysicalFileProvider(
                Path.Combine(_contentRootPath, "node_modules")
            );
            var _fileServerOptions = new FileServerOptions();
            _fileServerOptions.RequestPath = "/node_modules";
            _fileServerOptions.StaticFileOptions.FileProvider = provider;
            _fileServerOptions.EnableDirectoryBrowsing = true;
            app.UseFileServer(_fileServerOptions);


            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
            app.UseStatusCodePagesWithRedirects("~/Home/StatusCodePage");

            app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                      name: "areaAdmin",
                      template: "{area:exists}/{controller}/{action}/{id?}",
                      defaults: new {controller="Home", action = "Index" }
                      );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
