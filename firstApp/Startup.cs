using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;

namespace firstApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 默认文件路径
            //app.UseStaticFiles();

            //自定义路径
            //var staticfile = new StaticFileOptions();
            //staticfile.FileProvider = new PhysicalFileProvider(@"D:\");//指定目录 可以是任何的地址
            //app.UseStaticFiles(staticfile);

            #endregion


            #region 文件服务器
            //var dir = new DirectoryBrowserOptions();
            //dir.FileProvider = new PhysicalFileProvider(@"D:\");
            //app.UseDirectoryBrowser(dir);
            //var staticfile = new StaticFileOptions();
            //staticfile.FileProvider = new PhysicalFileProvider(@"D:\");
            //app.UseStaticFiles(staticfile);

            #endregion


            #region 文件服务器 未知文件类型的处理
            var staticfile = new StaticFileOptions();
            staticfile.FileProvider = new PhysicalFileProvider(@"D:\");
            staticfile.ServeUnknownFileTypes = true;
            staticfile.DefaultContentType = "application/x-msdownload"; //设置默认  MIME

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".my", "text/plain");//手动设置对应MIME
            staticfile.ContentTypeProvider = provider;

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(@"D:\"),
                EnableDirectoryBrowsing = true
            });
            app.UseStaticFiles(staticfile);

            #endregion
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("哈哈哈哈");
            });
        }
    }
}
