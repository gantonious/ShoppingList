using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingList.Data;
using ShoppingList.Data.Services;
using ShoppingList.WebServices.GoogleSignOn;
using ShoppingList.WebServices.UserRegistration;

namespace ShoppingList.WebServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShoppingListContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Username=postgres;Password=password");
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<GroupService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGoogleSignOn(new GoogleSignOnOptions
            {
                ClientIds = new List<string>
                {
                    "953367067816-nfhh5d7hn4ul77shb2tadec8mjl4489q.apps.googleusercontent.com"
                }
            });

            app.UseMiddleware<UserRegistrationMiddlware>();
            app.UseMvc();
        }
    }
}