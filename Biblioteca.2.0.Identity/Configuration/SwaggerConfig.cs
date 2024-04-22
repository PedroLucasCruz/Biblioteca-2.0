using Microsoft.OpenApi.Models;


namespace Biblioteca._2._0.Identity.Configuration
{
    public static class SwaggerConfig
    {
    
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "GeekShopping", //Titulo da sua api
                    Description = "Esta API faz parte do GeekShopping ", //Descrição
                    Contact = new OpenApiContact() { Name = "Pedro Cruz", Email = "pedrolucas11@live.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") } //licensa da api, pode ser colocado qualquer link

                });
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            return app;
        }
    }
}
