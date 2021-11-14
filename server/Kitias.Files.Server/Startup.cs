using Kitias.Files.Server.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kitias.Files.Server
{
    /// <summary>
    /// Startup class for run the app
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor for additing neccesary fields
        /// </summary>
        /// <param name="config">Get the app config file</param>
        public Startup(IConfiguration config) => _config = config;

        /// <summary>
        /// Configure all neccessary services
        /// </summary>
        /// <param name="services">Initial services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenAPI();
        }

        /// <summary>
        /// Configure all neccessary middlewares
        /// </summary>
        /// <param name="app">Generate app piplines</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Kitias.Files.Server");
                o.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
