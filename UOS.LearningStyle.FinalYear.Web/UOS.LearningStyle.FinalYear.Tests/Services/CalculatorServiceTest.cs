using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Business.Services;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Tests.Services
{
    [TestClass]
    public class CalculatorServiceTest
    {
        private ICalculatorService calculatorService;
        List<CourseGrade> courseGrades;

        [TestInitialize]
        public void Setup()
        {
            calculatorService = new CalculatorService();

            courseGrades = new List<CourseGrade> { new CourseGrade { Credits = 15, Mark = 70 } };

            calculatorService.CourseGrades = courseGrades;
        }

        [TestMethod]
        public void GivenIHaveCreditsAndMarksItShouldGiveMeTheTotal()
        {
            var expected = 70;
            var actual = calculatorService.CalculateTotal();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenIHaveCreditsAndMarksItShouldGiveMeTheBasicAverage()
        {
            var expected = 70;
            var actual = calculatorService.CalculateBasicAverage();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivenIHaveCreditsAndMarksItShouldGiveMeTheWeightedAverage()
        {
            var expected = 70;
            var actual = calculatorService.CalculateWeightedAverage();

            Assert.AreEqual(expected, actual);
        }
    }
}
