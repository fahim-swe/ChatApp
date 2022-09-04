using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Interface;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class ApplicatonIdentityServices
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration _config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                            options => {
                                options.SaveToken = true;
                                options.RequireHttpsMetadata = false;
                                options.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ClockSkew = TimeSpan.Zero,

                                    ValidAudience = _config["JWT:ValidAudience"],
                                    ValidIssuer = _config["JWT:ValidIssuer"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]))
                                };

                                options.Events = new JwtBearerEvents{
                                    OnMessageReceived = context => {
                                        var access_token = context.Request.Query["access_token"];
                                        var path = context.HttpContext.Request.Path;

                                        if(!string.IsNullOrEmpty(access_token) && path.StartsWithSegments("/hubs")){
                                            context.Token = access_token;
                                        }
                                        
                                        return Task.CompletedTask;
                                    }
                                };
                            }
                        
                        );
            return services;
        }
    }
}