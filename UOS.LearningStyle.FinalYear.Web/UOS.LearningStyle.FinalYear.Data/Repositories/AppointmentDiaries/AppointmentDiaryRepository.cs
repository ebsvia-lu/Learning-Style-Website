using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Data.Infrastructure;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Data.Repositories.AppointmentDiaries
{
    public class AppointmentDiaryRepository : RepositoryManager<AppointmentDiary>, IAppointmentDiaryRepository
    {
        public AppointmentDiaryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void DeleteAppointmentDiary(AppointmentDiary entity)
        {
            this.Delete(entity);
        }

        public IEnumerable<AppointmentDiary> GetAppointmentDiaries(string userid, string[] includeProperties)
        {
            return this.GetMany(e => e.UserId.Equals(userid), includeProperties); 
        }

        public AppointmentDiary GetAppointmentDiaryById(int id, string userid, string[] includeProperties)
        {
            return this.Get(e => e.ID.Equals(id) && e.UserId.Equals(userid), includeProperties);
        }

        public void InsertAppointmentDiary(AppointmentDiary entity)
        {
            this.Add(entity);
            this.Commit();
        }

        public void UpdateAppointmentDiary(AppointmentDiary entity)
        {
            this.Update(entity);
            this.Commit();
        }
    }
}
