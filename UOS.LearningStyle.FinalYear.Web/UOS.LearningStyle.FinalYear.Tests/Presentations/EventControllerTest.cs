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
    public class EventControllerTest
    {
        Mock<IAppointmentDiaryService> mockedAppointmentDiaryService;
        Mock<AppointmentDiary> mockedAppointmentDiary;
        EventController eventController;
        AppointmentDiary sampleData = new AppointmentDiary { DateTimeScheduled = DateTime.Parse("01/03/2020") };

        [TestInitialize]
        public void Setup()
        {
            mockedAppointmentDiaryService = new Mock<IAppointmentDiaryService>();
            mockedAppointmentDiary = new Mock<AppointmentDiary>();

            //mockedAppointmentDiary.SetupGet(x => x.DateTimeScheduled).Returns(DateTime.Parse("2020-03-10"));

            mockedAppointmentDiaryService.Setup(a => a.AddAppointmentDiary(mockedAppointmentDiary.Object));

            mockedAppointmentDiaryService.Setup(a => a.RetrieveAppointmentDiaries(It.IsAny<string>())).Returns(It.IsAny<List<AppointmentDiary>>);
                                           
            mockedAppointmentDiaryService.Setup(a => a.RetrieveAppointmentDiary(It.IsAny<int>(), It.IsAny<string>())).Returns(It.IsAny<AppointmentDiary>);
                                           
            mockedAppointmentDiaryService.Setup(a => a.RemoveAppointmentDiary(It.IsAny<AppointmentDiary>()));
                                           
            mockedAppointmentDiaryService.Setup(a => a.UpdateAppointmentDiary(It.IsAny<AppointmentDiary>()));

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

            eventController = new EventController(mockedAppointmentDiaryService.Object);

            eventController.ControllerContext = controllerContextMock.Object;
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToCreateItShouldSave()
        {
            eventController.Create(sampleData, "12:00 AM");

            mockedAppointmentDiaryService.Verify(a => a.AddAppointmentDiary(sampleData));
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToDeleteItShouldRemoveFromDatabase()
        {
            var result = (RedirectToRouteResult)eventController.DeleteConfirmed(1);
            
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToUpdateItShouldSave()
        {
            var result = (RedirectToRouteResult)eventController.Edit(sampleData, "12:00 AM");

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(result.RouteValues["controller"]);
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToGetItShouldRetrieve()
        {
            var result = eventController.GetDiaryEvents(DateTime.Now, DateTime.UtcNow);

            Assert.IsNotNull(result);
        }
    }
}
