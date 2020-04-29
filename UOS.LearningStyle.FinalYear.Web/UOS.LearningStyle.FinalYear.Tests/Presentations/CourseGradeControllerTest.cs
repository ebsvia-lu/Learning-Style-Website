using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Domains;
using UOS.LearningStyle.FinalYear.Web.Controllers;

namespace UOS.LearningStyle.FinalYear.Tests.Presentations
{
    [TestClass]
    public class CourseGradeControllerTest
    {
        Mock<ICourseGradeService> mockedCourseGradeService;
        Mock<ICalculatorService> mockedCalculatorService;
        CourseGradeController courseGradeController;
        CourseGrade sampleData = new CourseGrade { };

        [TestInitialize]
        public void Setup()
        {
            mockedCourseGradeService = new Mock<ICourseGradeService>();
            mockedCalculatorService = new Mock<ICalculatorService>();

            mockedCourseGradeService.Setup(c => c.AddCourseGrade(It.IsAny<CourseGrade>()));

            mockedCourseGradeService.Setup(c => c.RetrieveCourseGrades(It.IsAny<string>())).Returns(It.IsAny<List<CourseGrade>>);

            mockedCourseGradeService.Setup(c => c.RetrieveCourseGrade(It.IsAny<int>(), It.IsAny<string>())).Returns(It.IsAny<CourseGrade>);

            mockedCourseGradeService.Setup(c => c.RemoveCourseGrade(It.IsAny<CourseGrade>()));

            mockedCourseGradeService.Setup(c => c.UpdateCourseGrade(It.IsAny<CourseGrade>()));

            var data = new Dictionary<string, object>()
            {
                {"a", "b"} // fake whatever  you need here.
            };

            var identityMock = new Mock<ClaimsIdentity>();
            identityMock.Setup(p => p.FindFirst(It.IsAny<string>())).Returns(new Claim("foo", "someid"));

            var userMock = new Mock<IPrincipal>();
            userMock.Setup(p => p.IsInRole("Organization")).Returns(true);
            userMock.SetupGet(p => p.Identity).Returns(identityMock.Object);

            var userMock3 = userMock.Object.Identity;
            //var asldkfj = userMock3.GetUserId();

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<System.Web.Mvc.ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            //var contextMock = new Mock<HttpContext>();
            //HttpContext.Current.Items.Add("owin.Environment", data);

            courseGradeController = new CourseGradeController(mockedCourseGradeService.Object, mockedCalculatorService.Object);

            courseGradeController.ControllerContext = controllerContextMock.Object;
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToCreateItShouldSave()
        {
            courseGradeController.Create(sampleData);

            mockedCourseGradeService.Verify(c => c.AddCourseGrade(sampleData));
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToDeleteItShouldRemoveFromDatabase()
        {
            var result = (RedirectToRouteResult)courseGradeController.DeleteConfirmed(1);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GivenIHaveCourseGradeToUpdateItShouldSave()
        {
            courseGradeController.Edit(sampleData);

            mockedCourseGradeService.Verify(c => c.UpdateCourseGrade(sampleData));
        }
    }
}
