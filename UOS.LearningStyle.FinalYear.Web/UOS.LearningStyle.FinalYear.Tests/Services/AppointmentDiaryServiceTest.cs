using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Business.Services;
using UOS.LearningStyle.FinalYear.Data.Repositories.AppointmentDiaries;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Tests.Services
{
    [TestClass]
    public class AppointmentDiaryServiceTest
    {
        Mock<IAppointmentDiaryRepository> mockedAppointmentDiaryRepository;
        AppointmentDiary sampleData = new AppointmentDiary { };

        [TestInitialize]
        public void Setup()
        {
            mockedAppointmentDiaryRepository = new Mock<IAppointmentDiaryRepository>();

            mockedAppointmentDiaryRepository.Setup(c => c.InsertAppointmentDiary(It.IsAny<AppointmentDiary>()));

            mockedAppointmentDiaryRepository.Setup(c => c.GetAppointmentDiaries(It.IsAny<string>(), null)).Returns(It.IsAny<List<AppointmentDiary>>);

            mockedAppointmentDiaryRepository.Setup(c => c.GetAppointmentDiaryById(It.IsAny<int>(), It.IsAny<string>(), null)).Returns(It.IsAny<AppointmentDiary>);

            mockedAppointmentDiaryRepository.Setup(c => c.DeleteAppointmentDiary(It.IsAny<AppointmentDiary>()));

            mockedAppointmentDiaryRepository.Setup(c => c.UpdateAppointmentDiary(It.IsAny<AppointmentDiary>()));
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToCreateItShouldSave()
        {
            IAppointmentDiaryService appointmentDiaryService = new AppointmentDiaryService(mockedAppointmentDiaryRepository.Object);

            appointmentDiaryService.AddAppointmentDiary(sampleData);

            mockedAppointmentDiaryRepository.Verify(c => c.InsertAppointmentDiary(sampleData));
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToDeleteItShouldRemoveFromDatabase()
        {
            IAppointmentDiaryService appointmentDiaryService = new AppointmentDiaryService(mockedAppointmentDiaryRepository.Object);

            appointmentDiaryService.RemoveAppointmentDiary(sampleData);

            mockedAppointmentDiaryRepository.Verify(c => c.DeleteAppointmentDiary(sampleData));
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToUpdateItShouldSave()
        {
            IAppointmentDiaryService appointmentDiaryService = new AppointmentDiaryService(mockedAppointmentDiaryRepository.Object);

            appointmentDiaryService.RemoveAppointmentDiary(sampleData);

            mockedAppointmentDiaryRepository.Verify(c => c.DeleteAppointmentDiary(sampleData));
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToGetItShouldRetrieve()
        {
            IAppointmentDiaryService appointmentDiaryService = new AppointmentDiaryService(mockedAppointmentDiaryRepository.Object);

            appointmentDiaryService.RetrieveAppointmentDiary(1, "testuser");

            mockedAppointmentDiaryRepository.Verify(c => c.GetAppointmentDiaryById(1, "testuser", null));
        }

        [TestMethod]
        public void GivenIHaveAppointmentDiaryToGetItShouldRetrieveAList()
        {
            IAppointmentDiaryService appointmentDiaryService = new AppointmentDiaryService(mockedAppointmentDiaryRepository.Object);

            appointmentDiaryService.RetrieveAppointmentDiaries("testuser");

            mockedAppointmentDiaryRepository.Verify(c => c.GetAppointmentDiaries("testuser", null));
        }
    }
}
