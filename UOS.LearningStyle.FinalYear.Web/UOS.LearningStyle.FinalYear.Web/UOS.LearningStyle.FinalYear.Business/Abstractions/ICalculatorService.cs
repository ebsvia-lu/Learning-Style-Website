using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Abstractions
{
    public interface ICalculatorService
    {
        IEnumerable<CourseGrade> CourseGrades { get; set; }
        int CalculateTotal();
        int CalculateBasicAverage();
        int CalculateWeightedAverage();
    }
}
