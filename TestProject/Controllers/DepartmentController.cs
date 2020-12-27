using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.Services;

namespace TestProject.Controllers
{
	public class DepartmentController: Controller//继承Controller这个类: using Microsoft.AspNetCore.Mvc;
	{
		private readonly IDepartmentService _departmentService;//本地只读变量
		/// <summary>
		/// 构造函数注入IClock
		/// </summary>
		/// <param name="clock">对应注册的类ChinaClock</param>
		public DepartmentController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

		/// <summary>
		/// action,一般是异步的
		/// </summary>
		/// <returns>Task<IActionResult> 通常是这个形式的比较好</returns>
		public async Task<IActionResult> Index()
		{
			ViewBag.Title = "Department Index";
			var department = await _departmentService.GetAll();//获取所有部门的集合
			return View(department);
		}
		
		/// <summary>
		/// action,添加部门,这个添加的操作就是跳转到添加页面,无需异步
		/// </summary>
		/// <returns></returns>
		///[HttpGet] 可以不标明,没标明则默认HttpGet
		public IActionResult Add()
		{
			ViewBag.Title = "Add department";
			return View(new Department());//view的作用是添加部门,把一个department实例传入进去之后,可以在view页面里添加属性并提交
		}

		/// <summary>
		/// action,提交工作,异步
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		/// 
		/*
		 1.如果要使用post方法,必须标明HttpPost,否则默认HttpGet
		 2.表单提交应使用post方法
		 */
		[HttpPost]
		public async Task<IActionResult> Add(Department model)
		{
			if (ModelState.IsValid)//验证model是否合法
			{
				await _departmentService.Add(model);//如果model合法,则通过Service添加Model
			}
			return RedirectToAction(nameof(Index));//返回列表页面Index
		}
	}
}
