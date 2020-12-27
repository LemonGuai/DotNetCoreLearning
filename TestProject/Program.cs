using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject
{
	public class Program
	{
		/// <summary>
		/// asp.net core本质上是一个.net core控制台项目
		/// Main()方法配置了整个asp.net core应用,是一个运行入口
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();//Build()方法会把控制台应用变成aso.net core应用
		}

		/// <summary>
		/// 针对这个asp.net core应用的配置是用该方法进行配置的
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					//webBuilder.ConfigureAppConfiguration() 修改appsettubgs.json文件或者添加其他的配置文件

					//webBuilder.UseStartup(typeof(Program));针对不同环境来找自定义Startup类,如果没找到的话会调用Startup类
					webBuilder.UseStartup<Startup>();
					//配置asp.net core应用如何处理配置文件,web应用服务器,路由配置等

					//webBuilder.UseKestrel();
				});
	}
}
