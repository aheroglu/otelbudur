using DataAccess.Concrete;
using Entity.Concrete;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Areas.Admin.Models.MailService;
using Presentation.Areas.Admin.Models.MailService.Admin;
using Presentation.Areas.Admin.Models.MailService.HotelOwnerRequest;
using Presentation.Areas.Admin.Models.MailService.Staff;
using Presentation.Areas.HotelOwner.Models.MailService.Booking;
using Presentation.Models.Booking;
using Presentation.Models.MailService;
using System;

namespace Presentation
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
            services.AddScoped<ReservationConfirmedMailService>();
            services.AddScoped<AnswerQuestionMailService>();
            services.AddScoped<AnswerContactMailService>();
            services.AddScoped<SendForAdminsMailService>();
            services.AddScoped<SendForHotelOwnersMailService>();
            services.AddScoped<SendForMembersMailService>();
            services.AddScoped<SendForStaffsMailService>();
            services.AddScoped<SendNewsletterMailService>();
            services.AddScoped<RegisterAsHotelOwnerMailService>();
            services.AddScoped<ApproveRequestMailService>();
            services.AddScoped<ApproveRequestMailService>();
            services.AddScoped<RejectedRequestMailService>();
            services.AddScoped<CreateAdminMailService>();
            services.AddScoped<CreateStaffMailService>();
            services.AddScoped<HotelOwnerAcceptBookingMailService>();
            services.AddScoped<HotelOwnerRejectBookingMailService>();
            services.AddScoped<EvaluateExperienceMailService>();
            services.AddScoped<ConfirmEmailMailService>();

            services.AddDbContext<Context>();

            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage("Server = HP\\SQLEXPRESS; Database = OtelBudur; Integrated Security = True");
            });

            services.AddHangfireServer();

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            services.AddControllersWithViews();

            services.AddAuthorization(config =>
            {
                config.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            services.AddAuthorization();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "UserCookie";
                option.Cookie.HttpOnly = true;
                option.ExpireTimeSpan = TimeSpan.FromDays(14);
                option.SlidingExpiration = true;
                option.LoginPath = "/Login";
                option.AccessDeniedPath = new PathString("/LogIn");
            });

            services.Configure<SecurityStampValidatorOptions>(option =>
            {
                option.ValidationInterval = TimeSpan.FromMinutes(1);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<CheckBookingTime>("update-bookings",
                service => service.CheckAndUpdate(),
                Cron.Minutely);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
