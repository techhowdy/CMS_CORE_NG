using System;
using System.Linq;
using System.Text;
using ActivityService;
using AuthService;
using BackendService;
using CMS_CORE_NG.Extensions;
using CookieService;
using CountryService;
using DashboardService;
using DataService;
using EmailService;
using FiltersService;
using FunctionalService;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ModelService;
using RolesService;
using UserService;

namespace CMS_CORE_NG
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            /*---------------------------------------------------------------------------------------------------*/
            /*                              Cookie Helper SERVICE                                                */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddHttpContextAccessor();
            services.AddTransient<CookieOptions>();
            services.AddTransient<ICookieSvc, CookieSvc>();           
            /*---------------------------------------------------------------------------------------------------*/
            /*                              DB CONNECTION OPTIONS                                                */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CmsCoreNg_DEV"), x => x.MigrationsAssembly("CMS_CORE_NG")));

            services.AddDbContext<DataProtectionKeysContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DataProtectionKeysContext"), x => x.MigrationsAssembly("CMS_CORE_NG")));
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Functional SERVICE                                                    */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IFunctionalSvc, FunctionalSvc>();
            services.Configure<AdminUserOptions>(Configuration.GetSection("AdminUserOptions"));
            services.Configure<AppUserOptions>(Configuration.GetSection("AppUserOptions"));
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Writable SERVICE                                                      */
            /*---------------------------------------------------------------------------------------------------*/
            var sendGridOptionsSection = Configuration.GetSection("SendGridOptions");
            var smtpOptionsSection = Configuration.GetSection("SmtpOptions");
            var siteWideSettingsSection = Configuration.GetSection("SiteWideSettings");
            services.ConfigureWritable<SendGridOptions>(sendGridOptionsSection, "appsettings.json");
            services.ConfigureWritable<SmtpOptions>(smtpOptionsSection, "appsettings.json");
            services.ConfigureWritable<SiteWideSettings>(siteWideSettingsSection, "appsettings.json");
            /*---------------------------------------------------------------------------------------------------*/
            /*                              DEFAULT IDENTITY OPTIONS                                             */
            /*---------------------------------------------------------------------------------------------------*/
            var identityDefaultOptionsConfiguration = Configuration.GetSection("IdentityDefaultOptions");
            services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfiguration);
            var identityDefaultOptions = identityDefaultOptionsConfiguration.Get<IdentityDefaultOptions>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
                options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
                options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
                options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
                options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

                // User settings
                options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;

                // email confirmation require
                options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              DATA PROTECTION SERVICE                                              */
            /*---------------------------------------------------------------------------------------------------*/
            var dataProtectionSection = Configuration.GetSection("DataProtectionKeys");
            services.Configure<DataProtectionKeys>(dataProtectionSection);
            services.AddDataProtection().PersistKeysToDbContext<DataProtectionKeysContext>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              USER HELPER SERVICE                                                  */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IUserSvc, UserSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              USER Roles SERVICE                                                   */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IRoleSvc, RoleSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                                 APPSETTINGS SERVICE                                               */
            /*---------------------------------------------------------------------------------------------------*/
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            /*---------------------------------------------------------------------------------------------------*/
            /*                                 JWT AUTHENTICATION SERVICE                                        */
            /*---------------------------------------------------------------------------------------------------*/
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(o => {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = appSettings.ValidateIssuerSigningKey,
                    ValidateIssuer = appSettings.ValidateIssuer,
                    ValidateAudience = appSettings.ValidateAudience,
                    ValidIssuer = appSettings.Site,
                    ValidAudience = appSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero

                };
            });
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Email SERVICE                                                         */
            /*---------------------------------------------------------------------------------------------------*/
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGridOptions"));
            services.Configure<SmtpOptions>(Configuration.GetSection("SmtpOptions"));
            services.AddTransient<IEmailSvc, EmailSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              AUTH SERVICE                                                         */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IAuthSvc, AuthSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              Admin SERVICE                                                        */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IAdminSvc, AdminSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              ACTIVITY SERVICE                                                     */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IActivitySvc, ActivitySvc>();           
            /*---------------------------------------------------------------------------------------------------*/
            /*                              Country SERVICE                                                      */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<ICountrySvc, CountrySvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Dashboard SERVICE                                                     */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IDashboardSvc, DashboardSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                                 AuthenticationSchemes SERVICE                                     */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddAuthentication("Administrator").AddScheme<AdminAuthenticationOptions, AdminAuthenticationHandler>("Admin", null);
            services.AddAuthentication("User").AddScheme<UserAuthenticationOptions, UserAuthenticationHandler>("User", null);
            /*---------------------------------------------------------------------------------------------------*/
            /*                              ENABLE CORS                                                          */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddCors(options => {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });            

            /*---------------------------------------------------------------------------------------------------*/
            /*                              ENABLE API Versioning                                                */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddApiVersioning(
               options =>
               {
                   options.ReportApiVersions = true;
                   options.AssumeDefaultVersionWhenUnspecified = true;
                   options.DefaultApiVersion = new ApiVersion(1, 0);
               });            
            /*---------------------------------------------------------------------------------------------------*/
            /*                                 Razor Pages Runtime SERVICE                                       */
            /* Add Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation NuGet package to the project                */
            /* Surprised that refreshing a view while the app is running did not work                            */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddMvc().AddControllersAsServices().AddRazorRuntimeCompilation().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            /*--------------------------------------------------------------------------------------------------------------------*/
            /*                      Anti Forgery Token Validation Service                                                         */
            /* We use the option patterm to configure the Antiforgery feature through the AntiForgeryOptions Class                */
            /* The HeaderName property is used to specify the name of the header through which antiforgery token will be accepted */
            /*--------------------------------------------------------------------------------------------------------------------*/
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCors("EnableCORS");               
            app.UseStaticFiles();            
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();           
            app.UseAuthorization();

            /* Configure the app to provide a token in a cookie called XSRF-TOKEN */
            /* Custom Middleware Component is required to Set the cookie which is named XSRF-TOKEN 
             * The Value for this cookie is obtained from IAntiForgery service
             * We must configure this cookie with HttpOnly option set to false so that browser will allow JS to read this cookie
             */
            app.Use(nextDelegate => context =>
            {
                string path = context.Request.Path.Value.ToLower();
                string[] directUrls = { "/admin", "/store", "/cart", "checkout", "/login" };
                if (path.StartsWith("/swagger") || path.StartsWith("/api") || string.Equals("/", path) || directUrls.Any(url => path.StartsWith(url)))
                {
                    AntiforgeryTokenSet tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions()
                    {
                        HttpOnly = false,
                        Secure = false,
                        IsEssential = true,
                        SameSite = SameSiteMode.Strict
                    });

                }              

                return nextDelegate(context);
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}


