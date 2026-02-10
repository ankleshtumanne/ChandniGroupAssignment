using B2BManagement.Constant;
using Microsoft.OpenApi.Models;

namespace B2BManagement.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "B2B Management API",
                    Version = "v1"
                });

                options.AddSecurityDefinition(AppConstant.Bearer, new OpenApiSecurityScheme
                {
                    Name = AppConstant.Authorization,
                    Type = SecuritySchemeType.Http,
                    Scheme = AppConstant.Bearer,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = AppConstant.EnterJWt
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = AppConstant.Bearer
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}
