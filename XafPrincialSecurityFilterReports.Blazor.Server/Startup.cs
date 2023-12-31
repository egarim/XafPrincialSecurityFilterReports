﻿using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using DevExpress.ExpressApp.Xpo;
using XafPrincialSecurityFilterReports.Blazor.Server.Services;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace XafPrincialSecurityFilterReports.Blazor.Server;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
        services.AddXaf(Configuration, builder => {
            builder.UseApplication<XafPrincialSecurityFilterReportsBlazorApplication>();
            builder.Modules
                .AddCloningXpo()
                .AddConditionalAppearance()
                .AddDashboards(options => {
                    options.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData);
                })
                .AddFileAttachments()
                .AddReports(options => {
                    options.EnableInplaceReports = true;
                    options.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
                    options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
                })
                .AddValidation(options => {
                    options.AllowValidationDetailsAccess = false;
                })
                .Add<XafPrincialSecurityFilterReports.Module.XafPrincialSecurityFilterReportsModule>()
            	.Add<XafPrincialSecurityFilterReportsBlazorModule>();
            builder.ObjectSpaceProviders
                .AddSecuredXpo((serviceProvider, options) => {
                    string connectionString = null;
                    if(Configuration.GetConnectionString("ConnectionString") != null) {
                        connectionString = Configuration.GetConnectionString("ConnectionString");
                    }
#if EASYTEST
                    if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                        connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                    }
#endif
                    ArgumentNullException.ThrowIfNull(connectionString);
                    options.ConnectionString = connectionString;
                    options.ThreadSafe = true;
                    options.UseSharedDataStoreProvider = true;
                })
                .AddNonPersistent();
            builder.Security
                .UseIntegratedMode(options => {
                    options.RoleType = typeof(PermissionPolicyRole);
                    // ApplicationUser descends from PermissionPolicyUser and supports the OAuth authentication. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
                    // If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
                    options.UserType = typeof(XafPrincialSecurityFilterReports.Module.BusinessObjects.ApplicationUser);
                    // ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
                    // If you use PermissionPolicyUser or a custom user type, comment out the following line:
                    options.UserLoginInfoType = typeof(XafPrincialSecurityFilterReports.Module.BusinessObjects.ApplicationUserLoginInfo);
                    options.UseXpoPermissionsCaching();
                    options.Events.OnSecurityStrategyCreated += securityStrategy => {
                        // Use the 'PermissionsReloadMode.NoCache' option to load the most recent permissions from the database once
                        // for every Session instance when secured data is accessed through this instance for the first time.
                        // Use the 'PermissionsReloadMode.CacheOnFirstAccess' option to reduce the number of database queries.
                        // In this case, permission requests are loaded and cached when secured data is accessed for the first time
                        // and used until the current user logs out. 
                        // See the following article for more details: https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Security.SecurityStrategy.PermissionsReloadMode.
                        ((SecurityStrategy)securityStrategy).PermissionsReloadMode = PermissionsReloadMode.NoCache;
                    };
                })
                .AddPasswordAuthentication(options => {
                    options.IsSupportChangePassword = true;
                });
        });
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
            options.LoginPath = "/LoginPage";
        });
        var assembly = typeof(DevExpress.ExpressApp.Blazor.ApplicationBuilder.ReportsApplicationBuilderExtensions).Assembly;

        // Replace "AssemblyName" with the name of the assembly and
        // "InternalClassName" with the name of the internal class
        string assemblyName = assembly.GetName().Name;
        string internalClassName = "ReportsAuthorizationFilter";

        // Load the assembly
        //var assembly = Assembly.Load(assemblyName);

        // Get the Type of the internal class
        //var internalType = assembly.GetType($"DevExpress.ExpressApp.ReportsV2.Blazor.Authorization.{internalClassName}");
        //ReplaceService(services, internalType, typeof(CustomReportsAuthorizationFilter));
        // Now you can use the Type, for example, to create instances
        // Note: This will only work if the class has a public or internal constructor
        //var instance = Activator.CreateInstance(internalType);

        //CreateInternalType();
        // Replace the service
        //ReplaceService<CustomReportsAuthorizationFilter, CustomReportsAuthorizationFilter>(services);
    }
    private static void ReplaceService(IServiceCollection services, Type serviceType, Type implementationType)
    {
        // Remove the existing registrations
        var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == serviceType);
        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        // Add the new implementation
        services.AddScoped(serviceType, implementationType);
    }
    private static void ReplaceService<TService, TImplementation>(IServiceCollection services)
       where TService : class
       where TImplementation : class, TService
    {
        // Remove the existing registrations
        var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(TService));
        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        // Add the new implementation
        services.AddScoped<TService, TImplementation>();
    }
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseXaf();
        app.UseEndpoints(endpoints => {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
    }
}
