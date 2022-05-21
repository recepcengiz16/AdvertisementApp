using AdvertisementApp.Business.DependencyResolves.Microsoft;
using AdvertisementApp.Business.Helpers;
using AdvertisementApp.Business.ValidationRules.FluentValidation;
using AdvertisementApp.Web.Mappings.AutoMapper;
using AdvertisementApp.Web.Models;
using AdvertisementApp.Web.ValidationRules;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<ProvidedServiceCreateDtoValidator>();
builder.Services.AddTransient<IValidator<UserCreateModel>, UserCreateModelValidator>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => {
        opt.Cookie.Name = "UdemyCookie";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
        opt.ExpireTimeSpan = TimeSpan.FromDays(20);
    });



var profiles = ProfileHelper.GetProfiles();
profiles.Add(new UserCreateModelProfile());

var configuration = new MapperConfiguration(opt =>
{
    opt.AddProfiles(profiles);
});

var mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
