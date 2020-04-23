using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Services
{
    public class CalculatorService : ICalculatorService
    {
        public IEnumerable<CourseGrade> CourseGrades { get; set; }
        public int CalculateBasicAverage()
        {
            var basicAverage = 0;

            var grade = CourseGrades.Select(g => g).Sum(x => x.Mark);

            var numberOfGrades = CourseGrades.Select(g => g.Mark).Count();

            basicAverage = grade / numberOfGrades;

            return basicAverage;
        }

        public int CalculateTotal()
        {
            var total = 0;

            total = CourseGrades.Select(g => g.Mark).Sum(x => x);

            return total;
        }

        public int CalculateWeightedAverage()
        {
            var weightedAverage = 0;

            var weightedGrade = CourseGrades.Select(g => g).Sum(x => x.Mark * x.Credits);

            var weightedSum = CourseGrades.Select(g => g).Sum(x => x.Credits);

            weightedAverage = weightedGrade / weightedSum;

            return weightedAverage;
        }
    }
}
