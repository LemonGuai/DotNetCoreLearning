using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAll();//获取所有的部门
        Task<Department> GetById(int id);//通过ID获取部门
        Task<CompanySummary> GetCompanySummary();//获得公司整体情况
        Task Add(Department department);//添加部门
    }
}
