namespace Strogue.Aira.Web;

public static class DependencyInjection
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
        {
            ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
        });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddControllersWithViews();

        AddMultiLingualSupport(builder);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        var supportedCultures = new[] { "en", "es", "pt", "pl" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);


        app.UseRequestLocalization(localizationOptions);
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        //app.MapRazorPages()
        //    .WithStaticAssets();

        return app;
    }

    #region Private Methods

    private static void AddMultiLingualSupport(this WebApplicationBuilder builder)
    {
        #region Registering ResourcesPath

        builder.Services.AddLocalization(options => options.ResourcesPath = "");

        #endregion Registering ResourcesPath

        builder.Services.AddMvc()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options => {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            });
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var cultures = new List<CultureInfo> {
                new("en"),
                new("es"),
                new("pt"),
                new("pl")
            };
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = cultures;
            options.SupportedUICultures = cultures;
            options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
        });
        builder.Services.AddSingleton<SharedResourceService>();
    }

    #endregion Private Methods
}