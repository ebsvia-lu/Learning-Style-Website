using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Abstractions
    //The Course grade (Calculator) Interface.
{
    public interface ICourseGradeService
    {
        //Add course grade input into the calculator
        void AddCourseGrade(CourseGrade courseGrade);
        //edit course grade input into the calculator
        void UpdateCourseGrade(CourseGrade courseGrade);
        //delete course grade input from the calculator
        void RemoveCourseGrade(CourseGrade courseGrade);
        //Course Grade is a class (in Domain) that contains the course grade information
        CourseGrade RetrieveCourseGrade(int id, string userid);
        //IEnumerable is the base interface for all non-generic collections that can be enumerated.
        //Each set of information will be retrieved one by one
        IEnumerable<CourseGrade> RetrieveCourseGrades(string userid);
    }
}
