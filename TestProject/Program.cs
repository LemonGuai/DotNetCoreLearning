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
		/// asp.net core��������һ��.net core����̨��Ŀ
		/// Main()��������������asp.net coreӦ��,��һ���������
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();//Build()������ѿ���̨Ӧ�ñ��aso.net coreӦ��
		}

		/// <summary>
		/// ������asp.net coreӦ�õ��������ø÷����������õ�
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					//webBuilder.ConfigureAppConfiguration() �޸�appsettubgs.json�ļ�������������������ļ�

					//webBuilder.UseStartup(typeof(Program));��Բ�ͬ���������Զ���Startup��,���û�ҵ��Ļ������Startup��
					webBuilder.UseStartup<Startup>();
					//����asp.net coreӦ����δ��������ļ�,webӦ�÷�����,·�����õ�

					//webBuilder.UseKestrel();
				});
	}
}
