using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Business.Services;
using UOS.LearningStyle.FinalYear.Data.Repositories.CourseGrades;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Tests.Services
{
    [TestClass]
    public class CourseGradeServiceTest
    {
        Mock<ICourseGradeRepository> mockedCourseGradeRepository;
        CourseGrade sampleData = new CourseGrade { };

        [TestInitialize]
        public void Setup()
        {
            mockedCourseGradeRepository = new Mock<ICourseGradeRepository>();

            mockedCourseGradeRepository.Setup(c => c.InsertCourseGrade(It.IsAny<CourseGrade>()));

            mockedCourseGradeRepository.Setup(c => c.GetCourseGrades(It.IsAny<string>(), null)).Returns(It.IsAny<List<CourseGrade>>);

            mockedCourseGradeRepository.Setup(c => c.GetCourseGradeById(It.IsAny<int>(), It.IsAny<string>(), null)).Returns(It.IsAny<CourseGrade>);

            mockedCourseGradeRepository.Setup(c => c.DeleteCourseGrade(It.IsAny<CourseGrade>()));

            mockedCourseGradeRepository.Setup(c => c.UpdateCourseGrade(It.IsAny<CourseGrade>()));
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToCreateItShouldSave()
        {
            ICourseGradeService courseGradeService = new CourseGradeService(mockedCourseGradeRepository.Object);

            courseGradeService.AddCourseGrade(sampleData);

            mockedCourseGradeRepository.Verify(c => c.InsertCourseGrade(sampleData));
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToDeleteItShouldRemoveFromDatabase()
        {
            ICourseGradeService courseGradeService = new CourseGradeService(mockedCourseGradeRepository.Object);

            courseGradeService.RemoveCourseGrade(sampleData);

            mockedCourseGradeRepository.Verify(c => c.DeleteCourseGrade(sampleData));
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToUpdateItShouldSave()
        {
            ICourseGradeService courseGradeService = new CourseGradeService(mockedCourseGradeRepository.Object);

            courseGradeService.RemoveCourseGrade(sampleData);

            mockedCourseGradeRepository.Verify(c => c.DeleteCourseGrade(sampleData));
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToGetItShouldRetrieve()
        {
            ICourseGradeService courseGradeService = new CourseGradeService(mockedCourseGradeRepository.Object);

            courseGradeService.RetrieveCourseGrade(1, "testuser");

            mockedCourseGradeRepository.Verify(c => c.GetCourseGradeById(1, "testuser", null));
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToGetItShouldRetrieveAList()
        {
            ICourseGradeService courseGradeService = new CourseGradeService(mockedCourseGradeRepository.Object);

            courseGradeService.RetrieveCourseGrades("testuser");

            mockedCourseGradeRepository.Verify(c => c.GetCourseGrades("testuser", null));
        }
    }
}
