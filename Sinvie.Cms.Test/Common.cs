using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sinvie.Cms.Core.CodeGenerator;
using Sinvie.Cms.Core.Models;
using Sinvie.Cms.Core.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinvie.Cms.Test {


    public class Common {
        /// <summary>
        /// 构造依赖注入容器，然后传入参数
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider BuildServiceForSqlServer() {

            var services = new ServiceCollection();
            services.Configure<CodeGenerateOption>(options => {
                options.ConnectionString = "Data Source=.;Initial Catalog=SinvieCms;User ID=db.user;Password=abc;Persist Security Info=True;Max Pool Size=50;Min Pool Size=0;Connection Lifetime=300;";//这个必须
                options.DbType = DatabaseType.SqlServer.ToString();//数据库类型是SqlServer,其他数据类型参照枚举DatabaseType//这个也必须
                options.Author = "sinvie";//作者名称，随你，不写为空
                options.OutputPath = @"F:\VS_Project\Sinvie2019\Sinvie.NetCore.Web\Sinvie.Cms.Models";//实体模型输出路径，为空则默认为当前程序运行的路径
                options.ModelsNamespace = "Sinvie.Cms.Models";//实体命名空间
            });
            services.AddSingleton<CodeGenerator>();//注入Model代码生成器
            return services.BuildServiceProvider(); //构建服务提供程序


            //var services = new ServiceCollection();
            //services.Configure<CodeGenerateOption>(options => {
            //    options.ConnectionString = "Data Source=.;Initial Catalog=CzarCms;User ID=sa;Password=1;Persist Security Info=True;Max Pool Size=50;Min Pool Size=0;Connection Lifetime=300;";
            //    options.DbType = DatabaseType.SqlServer.ToString();//数据库类型是SqlServer,其他数据类型参照枚举DatabaseType
            //    options.Author = "sinvie";//作者名称
            //    options.OutputPath = "C:\\CzarCmsCodeGenerator";//模板代码生成的路径
            //    options.ModelsNamespace = "Sinvie.Cms.Models";//实体命名空间
            //    options.IRepositoryNamespace = "Sinvie.Cms.IRepository";//仓储接口命名空间
            //    options.RepositoryNamespace = "Sinvie.Cms.Repository.SqlServer";//仓储命名空间
            //    options.IServicesNamespace = "Sinvie.Cms.IServices";//服务接口命名空间
            //    options.ServicesNamespace = "Sinvie.Cms.Services";//服务命名空间


            //});
            //services.Configure<DbOption>("CzarCms", GetConfiguration().GetSection("DbOpion"));
            ////services.AddScoped<IArticleRepository, ArticleRepository>();
            ////services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<CodeGenerator>();
            //return services.BuildServiceProvider(); //构建服务提供程序
        }

        public static IConfiguration GetConfiguration() {
            //var builder = new ConfigurationBuilder()
            //   .SetBasePath(AppContext.BaseDirectory)
            //   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //   .AddEnvironmentVariables();


            var builder = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
