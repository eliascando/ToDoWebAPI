using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;

namespace Infraestructure.Security.JWT
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Jwt Authentication to the container.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                var result = new
                                {
                                    succeed = false,
                                    message = "Token Expired"
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                            }
                            else
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                var result = new 
                                { 
                                    succeed = false, 
                                    message = "Invalid Token"
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                            }
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = new
                            {
                                succeed = false,
                                message = "You are not authorized"
                            };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                        },
                        OnChallenge = context =>
                        {
                            if(!context.Request.Headers.ContainsKey("Authorization"))
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                var result = new
                                {
                                    succeed = false,
                                    message = "Token was not provided"
                                };
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
