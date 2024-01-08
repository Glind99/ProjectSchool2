using ProjectSchool2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSchool2
{
    public class GradeManager
    {
        private readonly ProjectSchoolContext _context;

        public GradeManager(ProjectSchoolContext dbContext)
        {
            _context = dbContext;
        }

        private int GenerateGradeId()
        {
            int? latestGradeId = _context.Grades.Max(g => (int?)g.GradeId);
            if (!latestGradeId.HasValue)
            {
                return 1;
            }
            return latestGradeId.Value + 1;
        }
        public void AddGrade(int studentId, int courseId, int teacherId, string grade, DateOnly gradeDate)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newGrade = new Grade
                    {
                        GradeId = GenerateGradeId(),
                        FkStudentId = studentId,
                        FkCourseId = courseId,
                        FkTeacherId = teacherId,
                        Grade1 = grade,
                        GradeDate = gradeDate
                    };
                    _context.Grades.Add(newGrade);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public List<Grade> GetGrades()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var grades = _context.Grades.ToList();

                    transaction.Commit();

                    return grades;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Rethrow the exception after rolling back the transaction
                }
            }
        }
    }
}
