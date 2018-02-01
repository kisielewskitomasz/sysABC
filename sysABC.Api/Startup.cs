using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sysABC.Core.Repositories;
using sysABC.Infrastructure.Repositories;
using sysABC.Infrastructure.Services;

namespace sysABC.Api
{
    public class Startup
    {
<<<<<<< HEAD
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
=======
        public Startup(IConfiguration configuration)
>>>>>>> parent of 1e18bdc... JwtSettings
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddMvc();
<<<<<<< HEAD
            //JwtSettings jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            //services.AddSingleton(jwtSettings);
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
=======
            JwtSettings jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddSingleton(jwtSettings);
>>>>>>> parent of 1e18bdc... JwtSettings
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:5000",

                    ValidateIssuerSigningKey = true,
<<<<<<< HEAD
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")),
=======
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnoprstuwyz")),
>>>>>>> parent of 1e18bdc... JwtSettings

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
