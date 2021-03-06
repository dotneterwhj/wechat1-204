using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WechatSubscription.DbContexts;
using WechatSubscription.WechatAPI;

namespace WechatSubscription
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            services.AddControllers(options =>
            {
                //options.OutputFormatters.Insert(0, new XmlSerializerOutputFormatter());
            });

            services.AddDbContext<WechatDbContext>(options =>
            {
                options.UseSqlite("FileName=" + Path.Combine(AppContext.BaseDirectory, "wechat.db"));
            });

            services.AddTransient<WechatMenuApi>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            IntialSettings(serviceProvider, configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void IntialSettings(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            WechatContrants.Appid = configuration.GetSection("WechatSetting:Appid").Value;
            WechatContrants.WechatToken = configuration.GetSection("WechatSetting:WeChatToken").Value;
            WechatContrants.Secret = configuration.GetSection("WechatSetting:Secret").Value;
            WechatContrants.GrantType = configuration.GetSection("WechatSetting:GrantType").Value;

            //var menuApi = serviceProvider.GetService<WechatMenuApi>();

            //menuApi.DeleteMenuAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            //menuApi.CreateMenuAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        }
    }
}
