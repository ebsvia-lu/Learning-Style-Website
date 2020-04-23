using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Business.Abstractions
{
    public interface IAppointmentDiaryService
    {
        void AddAppointmentDiary(AppointmentDiary appointmentDiary);
        void UpdateAppointmentDiary(AppointmentDiary appointmentDiary);
        void RemoveAppointmentDiary(AppointmentDiary appointmentDiary);
        AppointmentDiary RetrieveAppointmentDiary(int id, string userid);
        IEnumerable<AppointmentDiary> RetrieveAppointmentDiaries(string userid);
    }
}
