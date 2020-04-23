using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Business.Services;
using UOS.LearningStyle.FinalYear.Data.Infrastructure;
using UOS.LearningStyle.FinalYear.Data.Repositories.AppointmentDiaries;
using UOS.LearningStyle.FinalYear.Data.Repositories.CourseGrades;

namespace UOS.LearningStyle.FinalYear.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // Register services
            container.RegisterType<ICalculatorService, CalculatorService>();
            container.RegisterType<ICourseGradeService, CourseGradeService>();
            container.RegisterType<IAppointmentDiaryService, AppointmentDiaryService>();

            // Register repositories
            container.RegisterType<ICourseGradeRepository, CourseGradeRepository>();
            container.RegisterType<IAppointmentDiaryRepository, AppointmentDiaryRepository>();
            container.RegisterType<IDatabaseFactory, DatabaseFactory>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}