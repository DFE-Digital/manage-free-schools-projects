using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.API.Middleware;
using Dfe.ManageFreeSchoolProjects.API.StartupConfiguration;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Dfe.ManageFreeSchoolProjects.Middleware;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddMfspApiProject(Configuration);

            services.AddHsts(options => {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
	        app.UseManageFreeSchoolProjectsSwagger(provider);

            app.UseSecurityHeaders(options =>
            {
                options.AddFrameOptionsDeny()
                    .AddXssProtectionDisabled()
                    .AddContentTypeOptionsNoSniff()
                    .RemoveServerHeader()
                    .AddContentSecurityPolicy(builder =>
                    {
                        builder.AddDefaultSrc().None();
                    })
                    .AddPermissionsPolicy(builder =>
                    {
                        builder.AddAccelerometer().None();
                        builder.AddAutoplay().None();
                        builder.AddCamera().None();
                        builder.AddEncryptedMedia().None();
                        builder.AddFullscreen().None();
                        builder.AddGeolocation().None();
                        builder.AddGyroscope().None();
                        builder.AddMagnetometer().None();
                        builder.AddMicrophone().None();
                        builder.AddMidi().None();
                        builder.AddPayment().None();
                        builder.AddPictureInPicture().None();
                        builder.AddSyncXHR().None();
                        builder.AddUsb().None();
                    });
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            else 
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<ApiKeyMiddleware>();
            app.UseMiddleware<UrlDecoderMiddleware>();
            app.UseMiddleware<CorrelationIdMiddleware>();
			app.UseMiddleware<UserContextReceiverMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseManageFreeSchoolProjectsEndpoints();
            
            //TODO: is this correct?
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MfspContext>();

                // Automatically apply migrations at startup
                context.Database.Migrate();
                    
                var randomKpi = RandomDataGenerator.GenerateRandomValues<Kpi>();
                context.Kpi.Add(randomKpi);
                context.SaveChanges();
            }

        }
    }
}
