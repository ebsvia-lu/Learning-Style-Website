using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Abstractions
{//Calculator Interface
    public interface ICalculatorService
    {
        //IEnumerable is the base interface for all non-generic collections that can be enumerated.
        //Each set of information will be retrieved one by one from the course grade class
        IEnumerable<CourseGrade> CourseGrades { get; set; }
        //All calculations
        int CalculateTotal();
        int CalculateBasicAverage();
        int CalculateWeightedAverage();
    }
}
