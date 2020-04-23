using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Business.Abstractions;
using UOS.LearningStyle.FinalYear.Data.Repositories.AppointmentDiaries;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Services
{
    public class AppointmentDiaryService : IAppointmentDiaryService
    {
        private readonly IAppointmentDiaryRepository _appointmentDiaryRepository;

        public AppointmentDiaryService(IAppointmentDiaryRepository appointmentDiaryRepository)
        {
            try
            {
                _appointmentDiaryRepository = appointmentDiaryRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddAppointmentDiary(AppointmentDiary appointmentDiary)
        {
            try
            {
                _appointmentDiaryRepository.InsertAppointmentDiary(appointmentDiary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveAppointmentDiary(AppointmentDiary appointmentDiary)
        {
            try
            {
                _appointmentDiaryRepository.DeleteAppointmentDiary(appointmentDiary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<AppointmentDiary> RetrieveAppointmentDiaries(string userid)
        {
            try
            {
                return _appointmentDiaryRepository.GetAppointmentDiaries(userid, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AppointmentDiary RetrieveAppointmentDiary(int id, string userid)
        {
            try
            {
                return _appointmentDiaryRepository.GetAppointmentDiaryById(id, userid, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAppointmentDiary(AppointmentDiary appointmentDiary)
        {
            try
            {
                _appointmentDiaryRepository.UpdateAppointmentDiary(appointmentDiary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
