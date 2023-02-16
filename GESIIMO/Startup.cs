using GESIIMO.Areas.Identity;
using GESIIMO.Data;
using GESIIMO.Services;
using GESIIMO.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Blazored.SessionStorage;
using System;
using System.Net.Http;

namespace GESIIMO
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<GESIIMO.Data.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            });

            services.AddRazorPages();
            services.AddServerSideBlazor(options =>
            {
                options.DetailedErrors = false;
                options.DisconnectedCircuitMaxRetained = 100;
                options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(5);
                options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(5);
                options.MaxBufferedUnacknowledgedRenderBatches = 10;
            });
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<GESIIMO.Data.ApplicationUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();


            var sqlConnectionConfiguration =
                new SqlConfiguration(Configuration.GetConnectionString("DefaultConnection"));

            services.AddSingleton(sqlConnectionConfiguration);
            services.AddSingleton<HttpClient>();

            services.AddMudServices(config =>
            {
                //config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                //config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            services.AddScoped<IApartamentoService, ApartamentoService>();
            services.AddScoped<IArchivoService, ArchivoService>();
            services.AddScoped<IConjuntoService, ConjuntoService>();
            services.AddScoped<ITorreService, TorreService>();
            services.AddScoped<IListaService, ListaService>();
            services.AddScoped<IPropietarioService, PropietarioService>();
            services.AddScoped<IAsignacionApartamentoService, AsignacionApartamentoService>();
            services.AddScoped<ICargueJsonService, CargueJsonService>();
            services.AddScoped<IAsambleaService, AsambleaService>();
            services.AddScoped<ICuestionarioService, CuestionarioService>();
            services.AddScoped<IPreguntaService, PreguntaService>();
            services.AddScoped<IOpcionPreguntaService, OpcionPreguntaService>();
            services.AddScoped<IPoderService, PoderService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAccesoAsambleaService, AccesoAsambleaService>();
            services.AddScoped<IRespuestaService, RespuestaService>();
            services.AddScoped<IConfiguracionService, ConfiguracionService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAsignarPoderesService, AsignarPoderesService>();
            services.AddScoped<IConexionService, ConexionService>();

            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"],
                    sqlConnectionConfiguration
                )
            );

            services.AddSignalR();
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDkzMzY5QDMxMzkyZTMyMmUzMEJMbXQ1NDF1R0F4dDMreEpTN2ZSNEtKSldSUXYwWXBnVGFUS1VMVVh5NWM9");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<ChatHub>(ChatClient.HUBURL);
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

    }
}
