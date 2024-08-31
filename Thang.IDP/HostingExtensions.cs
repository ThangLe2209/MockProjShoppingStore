using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Thang.IDP.DbContexts;
using Thang.IDP.Services;

namespace Thang.IDP;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // configures IIS out-of-proc settings 
        //builder.Services.Configure<IISOptions>(iis =>
        //{
        //    iis.AuthenticationDisplayName = "Windows";
        //    iis.AutomaticAuthentication = false;
        //});
        // ..or configures IIS in-proc settings
        //builder.Services.Configure<IISServerOptions>(iis =>
        //{
        //    iis.AuthenticationDisplayName = "Windows";
        //    iis.AutomaticAuthentication = false;
        //});

        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.AddScoped<IPasswordHasher<Entities.User>, PasswordHasher<Entities.User>>();
        builder.Services.AddScoped<ILocalUserService, LocalUserService>(); // register LocalUserService instance
        builder.Services.AddDbContext<IdentityDbContext>(options => // ef core db sqlite
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("ThangIdentityDBConnectionString"));
            //options.UseSqlServer("Data Source=DESKTOP-98CHSUQ;Database=MarvinIDPDB;User ID=sa;Password=122215;Trust Server Certificate=True");
        });

        builder.Services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
            })
             .AddProfileService<LocalUserProfileService>() // add additional claims when store user in db
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryApiResources(Config.ApiResources) // config to use APi Resource
            .AddInMemoryClients(Config.Clients);
        //.AddTestUsers(TestUsers.Users);
        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }
}
