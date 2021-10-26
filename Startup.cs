using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Repositories;
using TrackingVoucher_v02.Services;

namespace TrackingVoucher_v02
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            //Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("firebase-admin-sdk.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<MyDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TrackingVoucherConnection")));
            //Brand
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            //Store
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();
            //Promotion
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IPromotionService, PromotionService>();
            //AppliedPromotion
            services.AddScoped<IAppliedPromotionRepository, AppliedPromotionRepository>();
            services.AddScoped<IAppliedPromotionService, AppliedPromotionService>();
            //User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            //Voucher
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IVoucherService, VoucherService>();
            //ClaimedVoucher
            services.AddScoped<IClaimedVoucherRepository, ClaimedVoucherRepository>();
            services.AddScoped<IClaimedVoucherService, ClaimedVoucherService>();
            //Authentication
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //services.AddMvc().AddControllersAsServices();
            //services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddControllers();

            //services.ConfigureSwagger();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackingVoucher", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            //add Cors
            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            ));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackingVoucher v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.ConfigureSwagger(provider);
        }
    }
}
