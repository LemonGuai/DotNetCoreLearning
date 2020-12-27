using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Services;

namespace TestProject
{
	/// <summary>
	/// 动态配置asp.net core应用
	/// 先调用ConfigureServices
	/// 再调用Configure
	/// ************************************************
	/// 可以针对不同环境,调用不同的Startup类,Startup+环境名 StartupDevelopment
	/// 同时修改Program类
	/// ************************************************
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// 负责配置依赖注入的注入参数,注册服务 
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddMvc();//过于强大,都支持
			//services.AddControllers();//只用api,不需要views
			services.AddControllersWithViews();//支持Controller,API,views 不包括razor page


			/*
			1. 注册自定义的服务ChinaClock,其中ChinaClock实现了IClock接口
			2. 泛型<IClock.ChinaClock>表示:每当其他类向容器请求一个IClock实例时,IOC容器会给这个类返回一个ChinaClock实例
			 */
			services.AddSingleton<IClock, ChinaClock>();

			services.AddSingleton<IDepartmentService, DepartmentService>();
			services.AddSingleton<IEmployeeService, EmployeeService>();
		}

		/// <summary>
		/// 可以针对不同环境,调用不同的配置方法,方法名+环境名
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		//public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env) { }
		//public void ConfigureServicesDevelopment(IServiceCollection services) { }



		/// <summary>
		/// 作用:配置asp.net core处理Http请求的管道(pipeline)
		/// </summary>
		/// <param name="app">这是一个服务,通过DI注入进来,在Program类的CreateDefaultBuilder中注册的(可能)</param>
		/// <param name="env">也是注入进来的</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			/*
			 1. 如果环境变量为自定义的,例如: OK
			 2. 则使用:env.IsEnviroment("OK")来判断


			 */
			if (env.IsDevelopment())//判断是否为开发模式
			{
				app.UseDeveloperExceptionPage();//中间件:显示一个异常信息的详细页面
			}

			app.UseStaticFiles();//这样web应用可以serve静态文件了,如果没有该中间件,客户端就无法访问js,css,html文件

			app.UseHttpsRedirection();//可以把http请求转换为https,这样可以迫使用户使用TLS或者SSL协议

			app.UseAuthentication();//身份认证中间件,放在这里的原因是希望在是用MVC razor page等功能之前就验证完身份信息

			app.UseRouting();//路由中间件

			app.UseEndpoints(endpoints =>
			{
				/*
				 1.endpoints实现了IEndpointRouteBuilder接口,其中有许多扩展方法,包括MapGet()
				 2.MapGet()配置方式:把"/"结尾的url,映射到lambda表达式(如下)里面
					async context =>
					{
						//如果url符合 "/",则会执行以下操作
						await context.Response.WriteAsync("Hello World!");
					});
				*/
				#region 模版
				//endpoints.MapGet("/", async context =>
				//{
				//	await context.Response.WriteAsync("Hello World!");
				//});
				#endregion


				//MVC的样式
				endpoints.MapControllerRoute( //路由表的形式
					"default", //模版
					/*
					 1.id?表示id为可选项;
					 2.controller=Home表示如果不输入controller这块就默认是Home;
					 3.action=Index表示如果不输入action这段就默认Index;
					*/
					"{controller=Department}/{action=Index}/{id?}"
					);
				//endpoints.MapControllers();不带参数的方法用于controller中标明[Attribute]
			});
		}
	}
}
