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
	/// ��̬����asp.net coreӦ��
	/// �ȵ���ConfigureServices
	/// �ٵ���Configure
	/// ************************************************
	/// ������Բ�ͬ����,���ò�ͬ��Startup��,Startup+������ StartupDevelopment
	/// ͬʱ�޸�Program��
	/// ************************************************
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// ������������ע���ע�����,ע����� 
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddMvc();//����ǿ��,��֧��
			//services.AddControllers();//ֻ��api,����Ҫviews
			services.AddControllersWithViews();//֧��Controller,API,views ������razor page


			/*
			1. ע���Զ���ķ���ChinaClock,����ChinaClockʵ����IClock�ӿ�
			2. ����<IClock.ChinaClock>��ʾ:ÿ������������������һ��IClockʵ��ʱ,IOC�����������෵��һ��ChinaClockʵ��
			 */
			services.AddSingleton<IClock, ChinaClock>();

			services.AddSingleton<IDepartmentService, DepartmentService>();
			services.AddSingleton<IEmployeeService, EmployeeService>();
		}

		/// <summary>
		/// ������Բ�ͬ����,���ò�ͬ�����÷���,������+������
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		//public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env) { }
		//public void ConfigureServicesDevelopment(IServiceCollection services) { }



		/// <summary>
		/// ����:����asp.net core����Http����Ĺܵ�(pipeline)
		/// </summary>
		/// <param name="app">����һ������,ͨ��DIע�����,��Program���CreateDefaultBuilder��ע���(����)</param>
		/// <param name="env">Ҳ��ע�������</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			/*
			 1. �����������Ϊ�Զ����,����: OK
			 2. ��ʹ��:env.IsEnviroment("OK")���ж�


			 */
			if (env.IsDevelopment())//�ж��Ƿ�Ϊ����ģʽ
			{
				app.UseDeveloperExceptionPage();//�м��:��ʾһ���쳣��Ϣ����ϸҳ��
			}

			app.UseStaticFiles();//����webӦ�ÿ���serve��̬�ļ���,���û�и��м��,�ͻ��˾��޷�����js,css,html�ļ�

			app.UseHttpsRedirection();//���԰�http����ת��Ϊhttps,����������ʹ�û�ʹ��TLS����SSLЭ��

			app.UseAuthentication();//�����֤�м��,���������ԭ����ϣ��������MVC razor page�ȹ���֮ǰ����֤�������Ϣ

			app.UseRouting();//·���м��

			app.UseEndpoints(endpoints =>
			{
				/*
				 1.endpointsʵ����IEndpointRouteBuilder�ӿ�,�����������չ����,����MapGet()
				 2.MapGet()���÷�ʽ:��"/"��β��url,ӳ�䵽lambda���ʽ(����)����
					async context =>
					{
						//���url���� "/",���ִ�����²���
						await context.Response.WriteAsync("Hello World!");
					});
				*/
				#region ģ��
				//endpoints.MapGet("/", async context =>
				//{
				//	await context.Response.WriteAsync("Hello World!");
				//});
				#endregion


				//MVC����ʽ
				endpoints.MapControllerRoute( //·�ɱ����ʽ
					"default", //ģ��
					/*
					 1.id?��ʾidΪ��ѡ��;
					 2.controller=Home��ʾ���������controller����Ĭ����Home;
					 3.action=Index��ʾ���������action��ξ�Ĭ��Index;
					*/
					"{controller=Department}/{action=Index}/{id?}"
					);
				//endpoints.MapControllers();���������ķ�������controller�б���[Attribute]
			});
		}
	}
}
