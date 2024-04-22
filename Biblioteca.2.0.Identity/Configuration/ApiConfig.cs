using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Biblioteca._2._0.Identity.Configuration
{

    //Para utilizar o tipo de injecao de depedencia com os metodos static é preciso que a classe esteja statica
    public static class ApiConfig
    {
    
        public static IServiceCollection AddApiConfiguration(this IServiceCollection service)
        {
            service.AddControllers();
            return service;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer )
        {          

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration();


            //app.UseAuthorization();
            //app.UseAuthentication(); //adicionado para autenticar a aplicação

            dbInitializer.Initialize();

            //Utilize os endpoint, as controller, rotas das controllers 
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllers(); //vai criar um mapeamento das controllers 
            });

            return  app;
        }

    }
}
