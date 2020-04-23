using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Abstractions
{
    public interface ICourseGradeService
    {
        void AddCourseGrade(CourseGrade courseGrade);
        void UpdateCourseGrade(CourseGrade courseGrade);
        void RemoveCourseGrade(CourseGrade courseGrade);
        CourseGrade RetrieveCourseGrade(int id, string userid);
        IEnumerable<CourseGrade> RetrieveCourseGrades(string userid);
    }
}
