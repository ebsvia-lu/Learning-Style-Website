using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Data.Infrastructure;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Data.Repositories.CourseGrades
{
    public class CourseGradeRepository : RepositoryManager<CourseGrade>, ICourseGradeRepository
    {
        public CourseGradeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public CourseGrade GetCourseGradeById(int id, string userid, string[] includeProperties)
        {
            return this.Get(e => e.ID.Equals(id) && e.UserId.Equals(userid), includeProperties);
        }

        public IEnumerable<CourseGrade> GetCourseGrades(string userid, string[] includeProperties)
        {
            return this.GetMany(e => e.UserId.Equals(userid), includeProperties);
        }

        public void InsertCourseGrade(CourseGrade entity)
        {
            this.Add(entity);
            this.Commit();
        }

        public void UpdateCourseGrade(CourseGrade entity)
        {
            this.Update(entity);
            this.Commit();
        }

        public void DeleteCourseGrade(CourseGrade entity)
        {
            this.Delete(entity);
            this.Commit();
        }
    }
}
