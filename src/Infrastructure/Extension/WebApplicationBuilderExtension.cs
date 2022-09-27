using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Extension
{
    public static class WebApplicationBuilderExtension
    {
        /// <summary>
        /// Add swagger processor to services
        /// </summary>
        /// <param name="builder">Application builder</param>
        /// <returns>Application builder</returns>
        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                foreach (var name in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.SwaggerDoc.XML", SearchOption.AllDirectories))
                {
                    c.IncludeXmlComments(name);
                }
            });

            return builder;
        }

        /// <summary>
        /// Add controlers and configure services and middleware
        /// </summary>
        /// <param name="builder">Application builder</param>
        /// <param name="filter">Application filter to add</param>
        /// <returns>Web application builder</returns>
        public static WebApplicationBuilder AddControllerServices(this WebApplicationBuilder builder, Type filter)
        {
            builder.Services.AddCors(options => 
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder => 
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    }
                );
            });

            builder.Services.AddControllers();

            builder.Services.AddMvc(options =>
            {
                options.Filters.Add(filter);
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });
            return builder;
        }

        /// <summary>
        /// Configure swagger to be used by application builder
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <returns>Application builder</returns>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search Postcode API V1");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}