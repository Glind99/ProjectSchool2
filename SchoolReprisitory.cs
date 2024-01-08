using ProjectSchool2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectSchool2
{
    public class EmployeeService
    {
        private readonly ProjectSchoolContext dbContext;

        public EmployeeService()
        {
            dbContext = new ProjectSchoolContext();
        }

        public Dictionary<string, int> GetEmployeesCountPerDepartment()
        {
            var employeesCountPerDepartment = new Dictionary<string, int>();

            var departments = dbContext.Deparments.Include(d => d.Employees).ToList();

            foreach (var department in departments)
            {
                int employeesCount = department.Employees.Count;
                employeesCountPerDepartment.Add(department.DepartmentName, employeesCount);
            }

            return employeesCountPerDepartment;
        }

        public void DisplayEmployeesCountPerDepartment()
        {
            var employeesCountPerDepartment = GetEmployeesCountPerDepartment();

            Console.WriteLine("Employees Count per Department:");
            foreach (var entry in employeesCountPerDepartment)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }
}
