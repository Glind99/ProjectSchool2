using Microsoft.EntityFrameworkCore;
using ProjectSchool2.Models;
using System.Globalization;


namespace ProjectSchool2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ProjectSchool; Integrated Security = True";
            var employeeService = new EmployeeService();
            var employeeSqlDataProvider = new EmployeeSqlDataProvider(connectionString);
            var employeeSqlDataManager = new EmployeeSqlDataManager(connectionString);
            var studentSqlDataProvider = new StudentSqlDataProvider(connectionString);
            var studentSqlDataManager = new StudentSqlDataManager(connectionString);
            var students = studentSqlDataProvider.GetAllStudentsWithInfoUsingSqlQuery();
            var employees = employeeSqlDataProvider.GetAllEmployeesWithInfoUsingSqlQuery();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Welcome to School!");
                Console.WriteLine("[1] See all employees & add Employees");
                Console.WriteLine("[2] See all Students & add Students");
                Console.WriteLine("[3] See how many Employees in each department");
                Console.WriteLine("[4] Add Grade");
                Console.WriteLine("[5] See all active Courses");
                Console.WriteLine("[6] See Salary.");
                Console.WriteLine("[0] Exit");
                String menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("[1] See all employees");
                        Console.WriteLine("[2] Add employee");
                        string employeeChoice = Console.ReadLine();
                        switch (employeeChoice)
                        {
                            case "1":
                                foreach (var employee in employees)
                                {
                                    Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}");
                                    
                                }
                                break;
                            case "2":
                                Console.WriteLine("Enter employee information:");

                                Console.Write("First Name: ");
                                string firstName = Console.ReadLine();

                                Console.Write("Last Name: ");
                                string lastName = Console.ReadLine();

                                Console.Write("Position: ");
                                string position = Console.ReadLine();

                                Console.Write("Years of Work (yyyy-MM-dd): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime yearsOfWork))
                                {
                                    
                                    employeeSqlDataManager.AddEmployee(firstName, lastName, position, yearsOfWork);

                                    Console.WriteLine("Employee added successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Employee not added.");
                                }
                                break;
                        }
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("[1] See all students");
                        Console.WriteLine("[2] Add student");
                        string studentChoice = Console.ReadLine();
                        switch (studentChoice)
                        {
                            case "1":
                                Console.WriteLine("List of Students:");
                                foreach (var student in students)
                                {
                                    Console.WriteLine($"Student ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}");
                                }
                                break;
                            case "2":
                                Console.WriteLine("Enter new student information:");
                                Console.Write("First Name: ");
                                string firstName = Console.ReadLine();

                                Console.Write("Last Name: ");
                                string lastName = Console.ReadLine();

                                Console.Write("Class ID (if applicable): ");
                                Console.WriteLine("ID:1 = Math");
                                Console.WriteLine("ID:2 = Sience");
                                Console.WriteLine("ID:3 = English");
                                Console.WriteLine("ID:4 = Swedish");
                                if (int.TryParse(Console.ReadLine(), out int classId))
                                {
                                    studentSqlDataManager.AddStudent(firstName, lastName, classId);

                                    Console.WriteLine("Student added successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid class ID. Student not added.");
                                }
                                break;
                        }
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        employeeService.DisplayEmployeesCountPerDepartment();
                        Console.ReadKey();
                        break;
                    case "4":
                        using (var dbContext = new ProjectSchoolContext())
                        {
                            var gradeService = new GradeManager(dbContext);
                            Console.WriteLine("[1] Add Grade");
                            Console.WriteLine("[2] See Grade List");
                            string gradeOption = Console.ReadLine();
                            switch (gradeOption)
                            {
                                case "1":
                                    EnterGradeInfo(gradeService);
                                    break;
                                case "2":
                                    DisplayAllGrades(gradeService);
                                    break;
                            }
                        }
                        break;
                    case "5":
                        Console.Clear();
                        using (var context = new ProjectSchoolContext())
                        {
                            var courseManager = new GetCourse(context);
                            courseManager.DisplayAllCourses();
                        }
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.Clear();
                        using (var context = new ProjectSchoolContext())
                        {
                            var salaryManager = new SalaryManager(context);
                            salaryManager.DisplayAverageAndTotalSalaryPerDepartment();
                        }
                        Console.ReadKey();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }

        static void EnterGradeInfo(GradeManager gradeManager)
        {
            Console.WriteLine("Enter student ID:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter course ID:");
            int courseId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter teacher ID:");
            int teacherId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter grade:");
            string grade = Console.ReadLine();

            Console.WriteLine("Enter grade date (yyyy-MM-dd):");
            DateOnly gradeDate = DateOnly.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            gradeManager.AddGrade(studentId, courseId, teacherId, grade, gradeDate);
        }
        static void DisplayAllGrades(GradeManager grademanager)
        {
            var allGrades = grademanager.GetGrades();
            Console.WriteLine("All Grades:");
            foreach (var g in allGrades)
            {
                Console.WriteLine($"GradeID: {g.GradeId}, StudentID: {g.FkStudentId}, CourseID: {g.FkCourseId}, TeacherID: {g.FkTeacherId}, Grade: {g.Grade1}, GradeDate: {g.GradeDate}");
            }
        }

    }
            
}
