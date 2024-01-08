using ProjectSchool2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ProjectSchool2
{
    internal class StudentSqlDataProvider
    {
        private readonly string _connectionString;

        public StudentSqlDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAllStudentsWithInfoUsingSqlQuery()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT StudentID, [First Name], [Last Name], FK_ClassID FROM Student";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                StudentId = reader["StudentId"] != DBNull.Value ? Convert.ToInt32(reader["StudentId"]) : 0,
                                FirstName = reader["First Name"] != DBNull.Value ? reader["First Name"].ToString() : string.Empty,
                                LastName = reader["Last Name"] != DBNull.Value ? reader["Last Name"].ToString() : string.Empty,
                                FkClassId = reader["FK_ClassID"] != DBNull.Value ? Convert.ToInt32(reader["FK_ClassID"]) : 0
                            };

                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }
    }
    internal class StudentSqlDataManager
    {
        private readonly string _connectionString;

        public StudentSqlDataManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddStudent(string firstName, string lastName, int? fkClassId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Student ([First Name], [Last Name], FK_ClassID) " +
                               "VALUES (@FirstName, @LastName, @FkClassId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@FkClassId", fkClassId ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
