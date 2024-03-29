using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Napa.Areas.Identity;
using Microsoft.Extensions.Logging;
using Tewr.Blazor.FileReader;
using Microsoft.AspNetCore.Rewrite;

namespace Napa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityEmailSender(x => Configuration.GetSection("Options:MailSender").Bind(x));
            services.AddDbContext<Data.Settings.DbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("SettingsConnection")
                //,
                //x =>
                //{
                //    x.MigrationsHistoryTable("__EFMigrationsHistory", "settings");
                //}
                );
            });

            services.AddDbContext<Data.Identity.DbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("IdentityConnection")));
            services.AddDbContext<Data.Menu.DbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MenuConnection")));
            services.AddDefaultIdentity<IdentityUser>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Data.Identity.DbContext>();
            services.AddRazorPages();
            services
                .AddServerSideBlazor()
                .AddHubOptions(o =>
                {
                    o.MaximumReceiveMessageSize = 1 * 1024 * 1024; // 1MB
                });
            services.AddFileReaderService();

            //services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddAuthorization();
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = Configuration.GetValue<String>("Options:ExternalLogins:Facebook:AppId");
                    options.AppSecret = Configuration.GetValue<String>("Options:ExternalLogins:Facebook:AppSecret");
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration.GetValue<String>("Options:ExternalLogins:Google:ClientId");
                    googleOptions.ClientSecret = Configuration.GetValue<String>("Options:ExternalLogins:Google:ClientSecret");
                });

            services.ConfigureApplicationCookie(x =>
            {
                x.Events.OnSignedIn = async x =>
                {
                    var userManager = x.HttpContext.RequestServices.GetService<UserManager<IdentityUser>>();

                    var administrators = await userManager.GetUsersInRoleAsync(Roles.Administrator);
                    if (administrators.Count == 0)
                    {
                        var user = await userManager.GetUserAsync(x.Principal);
                        var result = await userManager.AddToRoleAsync(user, Roles.Administrator);
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseRewriter(new RewriteOptions().AddRedirectToWwwPermanent());
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();

                endpoints.MapBlazorHub();
                //endpoints.MapFallbackToPage("/_Host");
            });
            Initialize(app).Wait();
        }

        private static async Task Initialize(IApplicationBuilder app)
        {
            var cultureInfo = new System.Globalization.CultureInfo("it-IT");

            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            using (var startUpScope = app.ApplicationServices.CreateScope())
            {
                var loggerFactory = startUpScope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var _logger = loggerFactory.CreateLogger("startup");
                try
                {
                    using (var tempScope = startUpScope.ServiceProvider.CreateScope())
                    {
                        var dbc = tempScope.ServiceProvider.GetRequiredService<Data.Identity.DbContext>();
                        _logger.LogInformation("Migrating Identity database to last version");
                        dbc.Database.Migrate();
                    }
                }
                catch (Exception ex) { _logger.LogError(ex, "can't migrate identity database"); }

                try
                {
                    using (var tempScope = startUpScope.ServiceProvider.CreateScope())
                    {
                        var rolemanager = tempScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                        var administratorRole = await rolemanager.FindByNameAsync(Roles.Administrator);
                        if (administratorRole == null)
                        {
                            _logger.LogInformation("No administrator role was found, adding one");
                            var res = await rolemanager.CreateAsync(new IdentityRole(Roles.Administrator));
                            if (!res.Succeeded) throw new Exception($"can't create {Roles.Administrator} role.");
                        }
                    }
                }
                catch (Exception ex) { _logger.LogError(ex, "can't create databases"); }

                try
                {
                    using (var tempScope = startUpScope.ServiceProvider.CreateScope())
                    {
                        var dbc = tempScope.ServiceProvider.GetRequiredService<Data.Menu.DbContext>();
                        _logger.LogInformation("Migrating Menu database to last version");
                        dbc.Database.Migrate();
                    }
                }
                catch (Exception ex) { _logger.LogError(ex, "can't migrate menu database"); }

                try
                {
                    using (var tempScope = startUpScope.ServiceProvider.CreateScope())
                    {
                        var dbc = tempScope.ServiceProvider.GetRequiredService<Data.Settings.DbContext>();
                        _logger.LogInformation("Migrating Settings database to last version");
                        dbc.Database.Migrate();
                    }
                }
                catch (Exception ex) { _logger.LogError(ex, "can't migrate menu database"); }

                try
                {
                    using (var tempScope = startUpScope.ServiceProvider.CreateScope())
                    {
                        //var mailsender = scope.ServiceProvider.GetService<SilentWave.Network.EmailService.IEmailSender>();
                        var mailsender = tempScope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender>();
                        //await mailsender.SendEmailAsync("trabacchin.luigi@gmail.com", "prova", "ciao");
                    }
                }
                catch (Exception ex) { _logger.LogError(ex, "can't resolve required mail sender"); }
            }
        }
    }
}
