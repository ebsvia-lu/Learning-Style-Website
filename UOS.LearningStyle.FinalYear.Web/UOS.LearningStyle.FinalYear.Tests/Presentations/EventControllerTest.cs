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
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Domains;
using UOS.LearningStyle.FinalYear.Web.Controllers;

namespace UOS.LearningStyle.FinalYear.Tests.Presentations
{
    [TestClass]
    public class EventControllerTest
    {
        Mock<IAppointmentDiaryService> mockedAppointmentDiaryService;
        EventController eventController;
        AppointmentDiary sampleData = new AppointmentDiary { DateTimeScheduled = DateTime.Parse("2020-04-01") };

        [TestInitialize]
        public void Setup()
        {
            mockedAppointmentDiaryService = new Mock<IAppointmentDiaryService>();

            mockedAppointmentDiaryService.Setup(a => a.AddAppointmentDiary(It.IsAny<AppointmentDiary>()));

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

        [Ignore]
        [TestMethod]
        public void GivenIHaveAppointmentDiaryToCreateItShouldSave()
        {
            eventController.Create(sampleData, "12:00");

            mockedAppointmentDiaryService.Verify(a => a.AddAppointmentDiary(sampleData));
        }

        [Ignore]
        [TestMethod]
        public void GivenIHaveAppointmentDiaryToDeleteItShouldRemoveFromDatabase()
        {
            eventController.DeleteConfirmed(1);

            mockedAppointmentDiaryService.Verify(a => a.RemoveAppointmentDiary(sampleData));
        }

        [Ignore]
        [TestMethod]
        public void GivenIHaveAppointmentDiaryToUpdateItShouldSave()
        {
            eventController.Edit(sampleData, "12:00");

            mockedAppointmentDiaryService.Verify(a => a.UpdateAppointmentDiary(sampleData));
        }

        [Ignore]
        [TestMethod]
        public void GivenIHaveAppointmentDiaryToGetItShouldRetrieve()
        {
            eventController.GetDiaryEvents(DateTime.Now, DateTime.UtcNow);

            mockedAppointmentDiaryService.Verify(a => a.RetrieveAppointmentDiaries("testuser"));
        }
    }
}
