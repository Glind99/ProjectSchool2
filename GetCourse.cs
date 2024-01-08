using ProjectSchool2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSchool2
{
    internal class GetCourse
    {
        private readonly ProjectSchoolContext _context;

        public GetCourse(ProjectSchoolContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            
            var courses = _context.Courses.ToList();

            
            return courses;
        }

        public void DisplayAllCourses()
        {
            
            var courses = GetAllCourses();

            
            Console.WriteLine("List of Courses:");
            foreach (var course in courses)
            {
                Console.WriteLine($"Course ID: {course.CourseId}, Course Name: {course.CourseName}");
                
            }
        }
    }
}
