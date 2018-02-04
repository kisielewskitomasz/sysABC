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
using sysABC.Infrastructure.SQL;
using Microsoft.EntityFrameworkCore;

namespace sysABC.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IUserRepository, InMemoryUserRepository>(); // without Sqlite DB
            services.AddScoped<IUserRepository, DbUserRepository>(); // with Sqlite DB
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddMvc();

            services.AddEntityFrameworkSqlite()
            //services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<SysABCContext>(options => options.UseSqlite("Data Source=sysabcsql.db"));//

            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")),

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
        }

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