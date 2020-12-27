using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.Services;

namespace TestProject.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IDepartmentService _departmentService;
		private readonly IEmployeeService _employeeService;

		public EmployeeController(IDepartmentService departmentService, IEmployeeService employeeService)
		{
			_departmentService = departmentService;
			_employeeService = employeeService;
		}

		/// <summary>
		/// index页面需要显示某个部门下面的所有员工,所以需要departmentId来指定部门
		/// </summary>
		/// <param name="departmentId"></param>
		/// <returns></returns>
		public async Task<IActionResult> Index(int departmentId)
		{
			var department = await _departmentService.GetById(departmentId);
			ViewBag.Title = $"Employees of {department.Name}";
			ViewBag.DepartmentId = departmentId;

			var employees = await _employeeService.GetByDepartmentId(departmentId);//根据departmentId获取指定部门的所有员工
			return View(employees);//传到view里面;
		}

		/// <summary>
		/// 根据部门添加员工
		/// </summary>
		/// <param name="departmentId">因此传入departmentId</param>
		/// <returns></returns>
		public ActionResult Add(int departmentId)
		{
			ViewBag.Title = "Add employee";
			return View(new Employee
			{
				DepartmentId = departmentId //设定好部门id
			});
		}

		/// <summary>
		/// 提交表单
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add(Employee model)//把新员工信息传入进来
		{
			if (ModelState.IsValid)
			{
				await _employeeService.Add(model);//添加一个员工
			}
			/*
			 1.返回到员工的Index页面
			 2.因为员工的Index方法需要一个参数,因此需要传入参数
			3.参数传入方法:创建一个匿名对象,对象名需要跟Index()方法的参数一模一样,然后赋值
			 */
			return RedirectToAction(nameof(Index), new { departmentId = model.DepartmentId });
		}
		/// <summary>
		/// 解雇员工
		/// </summary>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		public async Task<IActionResult> Fire(int employeeId)
		{
			var employee = await _employeeService.Fire(employeeId);
			return RedirectToAction(nameof(Index), new { departmentId = employee.DepartmentId });
		}
	}
}
