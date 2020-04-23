using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOS.LearningStyle.FinalYear.Domains;

namespace UOS.LearningStyle.FinalYear.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base(
            ConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString)
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for Student from separate class
            modelBuilder.Entity<AppointmentDiary>()
                .ToTable("AppointmentDiary");

            modelBuilder.Entity<CourseGrade>()
                .ToTable("CourseGrade");
        }

        public DbSet<AppointmentDiary> AppointmentDiaries { get; set; }
        public DbSet<CourseGrade> CourseGrades { get; set; } 

        protected override void Dispose(bool disposing)
        {
            Configuration.LazyLoadingEnabled = false;
            base.Dispose(disposing);
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
