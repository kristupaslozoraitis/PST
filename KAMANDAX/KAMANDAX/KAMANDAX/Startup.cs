using Blazored.Modal;
using KAMANDAX.Controllers;
using KAMANDAX.DB;
using KAMANDAX.JWT;
using KAMANDAX.Models;
using KAMANDAX.Services;
using KAMANDAX.Services.Authenticators;
using KAMANDAX.Services.RefreshTokenRepositories;
using KAMANDAX.Services.TokenValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatBlazor;
using KAMANDAX.Models.Responses;
using Syncfusion.Blazor;

namespace KAMANDAX
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
            services.AddMatBlazor();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.TopRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });
            services.AddControllers();
            AuthenticationConfiguration configuration = new AuthenticationConfiguration();
            Configuration.Bind("Jwt", configuration);
            services.AddSingleton(configuration);
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredModal();
            services.AddScoped<AuthController>();
            services.AddScoped<ProductController>();
            services.AddScoped<RefreshTokenService>();
            services.AddSingleton<AuthenticatedUserResponse>();
            services.AddSingleton<JwtGenerator>();
            services.AddSingleton<RefreshRequest>();
            services.AddSingleton<RefreshGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddSingleton<RefreshToken>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            services.AddScoped<RefreshTokenService>();
            services.AddScoped<ProductService>();
            services.AddScoped<UserService>();
            services.AddScoped<CartItemController>();
            services.AddScoped<CartItemService>();
            services.AddScoped<CommentService>();
            services.AddSingleton<Product>();
            services.AddSingleton<List<Product>>();
            services.AddDbContext<WebDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("default")), ServiceLifetime.Transient);
            services.AddSingleton<User>();
            services.AddScoped<OrderInformationController>();
            services.AddScoped<OrdersService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
