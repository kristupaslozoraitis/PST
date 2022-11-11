using KAMANDAX.Controllers;
using KAMANDAX.DB;
using KAMANDAX.JWT;
using KAMANDAX.Models.Responses;
using KAMANDAX.Models;
using KAMANDAX.Services.Authenticators;
using KAMANDAX.Services.RefreshTokenRepositories;
using KAMANDAX.Services.TokenValidators;
using KAMANDAX.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Configuration;
using MatBlazor;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Blazored.Modal;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMatBlazor();
builder.Services.AddMatToaster(config =>
{
    config.Position = MatToastPosition.TopRight;
    config.PreventDuplicates = true;
    config.NewestOnTop = true;
    config.ShowCloseButton = true;
    config.MaximumOpacity = 95;
    config.VisibleStateDuration = 3000;
});
builder.Services.AddControllers();
AuthenticationConfiguration configuration = new AuthenticationConfiguration();
builder.Configuration.Bind("Jwt", configuration);
builder.Services.AddSingleton(configuration);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredModal();
builder.Services.AddScoped<AuthController>();
builder.Services.AddScoped<ProductController>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddSingleton<AuthenticatedUserResponse>();
builder.Services.AddSingleton<JwtGenerator>();
builder.Services.AddSingleton<RefreshRequest>();
builder.Services.AddSingleton<RefreshGenerator>();
builder.Services.AddSingleton<RefreshTokenValidator>();
builder.Services.AddSingleton<RefreshToken>();
builder.Services.AddScoped<Authenticator>();
builder.Services.AddSingleton<TokenGenerator>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CartItemController>();
builder.Services.AddScoped<CartItemService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddSingleton<Product>();
builder.Services.AddSingleton<List<Product>>();
builder.Services.AddDbContext<WebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("default")), ServiceLifetime.Transient);
builder.Services.AddSingleton<User>();
builder.Services.AddScoped<OrderInformationController>();
builder.Services.AddScoped<OrdersService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key)),
        ValidIssuer = configuration.Issuer,
        ValidAudience = configuration.Audience,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero
    };

});

var app = builder.Build();
if (app.Environment.IsDevelopment())
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("123");
app.Run();