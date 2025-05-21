using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Builders
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerBuilder
    {
        public static IApplicationBuilder AddSwaggerApp(this IApplicationBuilder app)
        {
            var enabled = Convert.ToBoolean(Application.Registration.ConfigurationManager.SwaggerEnabled);

            if (enabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocumentTitle = $"{Application.Registration.ConfigurationManager.WebAPIName} Swagger";
                    c.HeadContent = @"
                    <script type='text/javascript'>
                        (function() {
                            var links = document.querySelectorAll(""link[rel~='icon']"");
                            for (var link of links) {
                                link.parentNode.removeChild(link);
                            }
                            var link = document.createElement('link');
                            link.rel = 'icon';
                            link.type = 'image/png';
                            link.href = '/images/favicon.png';
                            document.getElementsByTagName('head')[0].appendChild(link);
                        })();
                    </script>";

                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Application.Registration.ConfigurationManager.WebAPIName} v1");
                    c.InjectStylesheet("/css/swagger-custom.css");
                    c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    c.EnableFilter();
                    c.ShowExtensions();
                    c.ShowCommonExtensions();
                    c.EnablePersistAuthorization();
                    c.DisplayRequestDuration();
                });
            }

            return app;
        }
    }
}
