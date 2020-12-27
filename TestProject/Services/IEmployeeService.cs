using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Services
{
    public interface IEmployeeService
    {
        Task Add(Employee employee);//添加员工
        Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId);//按照部门Id获取指定部门的所有员工
        Task<Employee> Fire(int id);//按照员工ID开除员工
    }
}
