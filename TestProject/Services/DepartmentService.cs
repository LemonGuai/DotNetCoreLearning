using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Services
{
    public class DepartmentService :IDepartmentService
    {
        private readonly List<Department> _departments = new List<Department>();

        public DepartmentService()//添加3个部门
        {
            _departments.Add(new Department
            {
                Id = 1,
                Name = "HR",
                EmployeeCount = 16,
                Location = "Beijing"
            });
            _departments.Add(new Department
            {
                Id = 2,
                Name = "RD",
                EmployeeCount = 52,
                Location = "Shanghai"
            });
            _departments.Add(new Department
            {
                Id = 3,
                Name = "Sales",
                EmployeeCount = 200,
                Location = "China"
            });
        }
        public Task<IEnumerable<Department>> GetAll()
        {
            return Task.Run(() => _departments.AsEnumerable());
        }

        public Task<Department> GetById(int id)
        {
            return Task.Run(() => _departments.FirstOrDefault(x => x.Id == id));//根据id查记录
        }

        public Task<CompanySummary> GetCompanySummary()
        {
            return Task.Run(() =>
            {
                return new CompanySummary
                {
                    EmployeeCount = _departments.Sum(x => x.EmployeeCount),//所有部门的员工总数之和
                    AverageDepartmentEmployeeCount = (int)_departments.Average(x => x.EmployeeCount)//每个部门平均员工数量
                };
            });
        }

        public Task Add(Department department)
        {
            department.Id = _departments.Max(x => x.Id) + 1;//设置ID:现有部门中最大的ID+1 = 添加的记录ID
            _departments.Add(department);
            return Task.CompletedTask;
        }

        
    }
}
