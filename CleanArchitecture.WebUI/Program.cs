using CleanArchitecture.WebUI.LocalizationResources;
using CleanArchitecture.WebUI.Services.Implementations;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Syncfusion.Licensing;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var cultures = new[]
          {
                new CultureInfo("en-US"),
                new CultureInfo("vi"),
            };

builder.Services.AddControllersWithViews().AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
{
    // When using all the culture providers, the localization process will
    // check all available culture providers in order to detect the request culture.
    // If the request culture is found it will stop checking and do localization accordingly.
    // If the request culture is not found it will check the next provider by order.
    // If no culture is detected the default culture will be used.

    // Checking order for request culture:
    // 1) RouteSegmentCultureProvider
    //      e.g. http://localhost:1234/tr
    // 2) QueryStringCultureProvider
    //      e.g. http://localhost:1234/?culture=tr
    // 3) CookieCultureProvider
    //      Determines the culture information for a request via the value of a cookie.
    // 4) AcceptedLanguageHeaderRequestCultureProvider
    //      Determines the culture information for a request via the value of the Accept-Language header.
    //      See the browsers language settings

    // Uncomment and set to true to use only route culture provider
    ops.UseAllCultureProviders = true;
    ops.ResourcesPath = "LocalizationResources";
    ops.RequestLocalizationOptions = o =>
    {
        o.SupportedCultures = cultures;
        o.SupportedUICultures = cultures;
        o.DefaultRequestCulture = new RequestCulture("en-US");
        o.RequestCultureProviders = new List<IRequestCultureProvider>
        {
			// Order is important, its in which order they will be evaluated
			new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
    };
}).AddRazorRuntimeCompilation();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
Constants.APIUrlBase = builder.Configuration["ServiceUrls:ApiUrl"];
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IVillaService, VillaService>();
builder.Services.AddScoped<IVillaNumberService, VillaNumberService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromHours(10);
    option.LoginPath = "/Access/Login";
    option.AccessDeniedPath = "/Access/AccessDenied";
});
// Add services to the container.

var app = builder.Build();
app.UseHttpLogging();
SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["Syncfusion:Key"]);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();

app.UseRouting();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
