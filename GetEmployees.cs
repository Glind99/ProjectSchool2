using ProjectSchool2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace ProjectSchool2
{
    internal class GetEmployees
    {
        private readonly string _connectionString;

        public GetEmployees(string connectionString)
        {
            _connectionString = connectionString;
        }
        
    }
    internal class EmployeeSqlDataProvider
    {
        private readonly string _connectionString;

        public EmployeeSqlDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetAllEmployeesWithInfoUsingSqlQuery()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT EmployeeID, [First Name], [Last Name], Position, YearsOfWork FROM Employee";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeId = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : 0,
                                FirstName = reader["First Name"] != DBNull.Value ? reader["First Name"].ToString() : string.Empty,
                                LastName = reader["Last Name"] != DBNull.Value ? reader["Last Name"].ToString() : string.Empty,
                                Position = reader["Position"] != DBNull.Value ? reader["Position"].ToString() : string.Empty,
                                YearsOfWork = reader["YearsOfWork"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["YearsOfWork"])) : default(DateOnly)
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }
        
    }
    internal class EmployeeSqlDataManager
    {
        private readonly string _connectionString;

        public EmployeeSqlDataManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddEmployee(string firstName, string lastName, string position, DateTime yearsOfWork)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Employee ([First Name], [Last Name], Position, YearsOfWork) " +
                               "VALUES (@FirstName, @LastName, @Position, @YearsOfWork)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@YearsOfWork", SqlDbType.DateTime).Value = yearsOfWork;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
