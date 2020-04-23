using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Data.Repositories.AppointmentDiaries
{
    public interface IAppointmentDiaryRepository
    {
        void InsertAppointmentDiary(AppointmentDiary entity);
        void UpdateAppointmentDiary(AppointmentDiary entity);
        void DeleteAppointmentDiary(AppointmentDiary entity);
        AppointmentDiary GetAppointmentDiaryById(int id, string userid, string[] includeProperties);
        IEnumerable<AppointmentDiary> GetAppointmentDiaries(string userid, string[] includeProperties);
    }
}
