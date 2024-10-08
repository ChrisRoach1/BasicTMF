
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BasicTMF.Infrastructure;
using BasicTMF.Application;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BasicTMF.Infrastructure.Database;

namespace BasicTMF.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Documentation",
                    Version = "v1.0",
                    Description = ""
                });
                options.ResolveConflictingActions(x => x.First());
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    BearerFormat = "JWT",
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/oauth/token"),
                            AuthorizationUrl = new Uri($"https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/authorize?audience=https://tmf-server.com"),
                            Scopes = new Dictionary<string, string>{{ "openid", "OpenId" }, { "read:weather", "readweather" }, { "study", "studyinfo" } }
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                        {{new OpenApiSecurityScheme
                                                      {
                                                          Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                                                      },new[] { "openid" }}
                                                        });

            });

            


            builder.Services.AddInfrastructure(builder.Configuration).AddApplication();

            Initializer.InitilizeDatabase(builder.Configuration.GetConnectionString("tmfDB")!);
            

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/";
                options.Audience = "https://tmf-server.com";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

                        builder.Services
                          .AddAuthorization(options =>
                          {
                              options.AddPolicy("read:weather",policy => policy.Requirements.Add(new HasScopeRequirement("read:weather", "https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/")));

                              options.AddPolicy("study", policy => policy.Requirements.Add(new HasScopeRequirement("study", "https://dev-tjsbzsbfnaw8tlzb.us.auth0.com/")));
                          });

            builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


            var app = builder.Build();

            app.UseAuthentication();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(settings =>
                {
                    settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
                    settings.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
                    settings.OAuthClientSecret(builder.Configuration["Auth0:ClientSecret"]);
                    settings.OAuthUsePkce();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
