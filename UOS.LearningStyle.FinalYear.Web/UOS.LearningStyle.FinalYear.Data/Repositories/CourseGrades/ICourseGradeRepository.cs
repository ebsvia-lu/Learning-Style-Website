using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Data.Repositories.CourseGrades
{
    public interface ICourseGradeRepository
    {
        void InsertCourseGrade(CourseGrade entity);
        void UpdateCourseGrade(CourseGrade entity);
        void DeleteCourseGrade(CourseGrade entity);
        CourseGrade GetCourseGradeById(int id, string userid, string[] includeProperties);
        IEnumerable<CourseGrade> GetCourseGrades(string userid, string[] includeProperties);
    }
}
