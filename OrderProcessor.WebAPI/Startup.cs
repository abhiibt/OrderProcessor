using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderProcessor.Business.Concrete;
using OrderProcessor.Business.Concrete.RuleHandlers;
using OrderProcessor.Business.Interfaces;

namespace OrderProcessing
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
            #region
            services.AddTransient<INotificationProvider, EmailProvider>();
            services.AddTransient<IOrderProcessorBusiness, OrderProcessorBusiness>();
            services.AddTransient<IShipmentProvider, ShipmentProvider>();
            services.AddTransient<IRuleHandler, BookHandler>(provider =>  {
                return new BookHandler(provider.GetService<IShipmentProvider>());
            });
            services.AddTransient<IRuleHandler, CommissionProviderHandler>();
            services.AddTransient<IRuleHandler, MembershipHandler>();
            services.AddTransient<IRuleHandler, MembershipUpgradeHandler>();
            services.AddTransient<IRuleHandler, PhysicalProductHandler>();
            services.AddTransient<IRuleHandler, SkiVideoHandler>();
            #endregion

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Processor API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Processor API V1");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}