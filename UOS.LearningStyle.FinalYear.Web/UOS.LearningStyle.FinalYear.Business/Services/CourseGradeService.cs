using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Data.Repositories.CourseGrades;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Services
{
    public class CourseGradeService : ICourseGradeService
    {
        private readonly ICourseGradeRepository _courseGradeRepository;
        public CourseGradeService(ICourseGradeRepository courseGradeRepository)
        {
            _courseGradeRepository = courseGradeRepository;
        }

        public void AddCourseGrade(CourseGrade courseGrade)
        {
            _courseGradeRepository.InsertCourseGrade(courseGrade);
        }

        public void UpdateCourseGrade(CourseGrade courseGrade)
        {
            _courseGradeRepository.UpdateCourseGrade(courseGrade);
        }

        public void RemoveCourseGrade(CourseGrade courseGrade)
        {
            _courseGradeRepository.DeleteCourseGrade(courseGrade);
        }

        public CourseGrade RetrieveCourseGrade(int id, string userid)
        {
            return _courseGradeRepository.GetCourseGradeById(id, userid, null);
        }

        public IEnumerable<CourseGrade> RetrieveCourseGrades(string userid)
        {
            return _courseGradeRepository.GetCourseGrades(userid, null);
        }
    }
}
