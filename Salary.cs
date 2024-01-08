using ProjectSchool2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectSchool2
{
    public class DepartmentSalaryResult
    {
        public string DepartmentName { get; set; }
        public decimal? AverageSalary { get; set; }
        public decimal? TotalSalary { get; set; }
    }
    internal class SalaryManager
    {
        private readonly ProjectSchoolContext _context;

        public SalaryManager(ProjectSchoolContext context)
        {
            _context = context;
        }

        public void DisplayAverageAndTotalSalaryPerDepartment()
        {
            var sql = @"
                        SELECT
                         d.DepartmentName,
                        AVG(s.SalaryAmount) AS AverageSalary,
                        SUM(s.SalaryAmount) AS TotalSalary
                        FROM
                        Deparment d
                        JOIN
                        Employee e ON d.DepartmentId = e.FkDepartmentId
                        JOIN
                        Salary s ON e.EmployeeId = s.FkEmployeeId
                        GROUP BY
                        d.DepartmentName;";

                var result = _context.Deparments
                .Include(d => d.Employees)
                .ThenInclude(e => e.Salaries)
                .GroupBy(d => new { d.DepartmentName })
                .Select(g => new DepartmentSalaryResult
                {
                 DepartmentName = g.Key.DepartmentName,
                 AverageSalary = g.SelectMany(d => d.Employees)
                        .SelectMany(e => e.Salaries)
                        .Average(s => (decimal?)s.SalaryAmount),
                 TotalSalary = g.SelectMany(d => d.Employees)
                       .SelectMany(e => e.Salaries)
                       .Sum(s => (decimal?)s.SalaryAmount)
                 })
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"Department: {item.DepartmentName}");
                Console.WriteLine($"Average Salary: {item.AverageSalary:C}");
                Console.WriteLine($"Total Salary: {item.TotalSalary:C}");
                Console.WriteLine();
            }
        }


    }
}

