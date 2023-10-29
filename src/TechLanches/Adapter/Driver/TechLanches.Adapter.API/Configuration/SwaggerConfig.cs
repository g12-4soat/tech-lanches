using Microsoft.OpenApi.Models;
using System.Reflection;

namespace TechLanches.Adapter.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Tech Lanches",
                    Description = "Tech Lanches API Swagger",
                    Contact = new OpenApiContact 
                    { 
                        Name = "Tech Lanches", 
                        Email = "g12.4soat.fiap@outlook.com",
                        Url = new Uri("https://github.com/g12-4soat")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                xmlPath = Path.Combine(AppContext.BaseDirectory, "TechLanches.Application.xml");

                s.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                s.EnableAnnotations();
                s.UseAllOfToExtendReferenceSchemas();
                s.IgnoreObsoleteActions();
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Tech Lanches";
                c.RoutePrefix = "swagger";
                c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.InjectJavascript("/SwaggerUI/js/swagger-ui.js");
                c.InjectStylesheet("/SwaggerUI/css/swagger-ui.css");
            });

            app.UseReDoc(c =>
            {
                c.DocumentTitle = "Tech Lanches";
                c.SpecUrl = "/swagger/v1/swagger.json";
            });
        }
    }
}
