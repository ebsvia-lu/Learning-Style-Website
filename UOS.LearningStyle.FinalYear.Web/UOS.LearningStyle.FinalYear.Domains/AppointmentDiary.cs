using System;
using System.ComponentModel.DataAnnotations;

namespace UOS.LearningStyle.FinalYear.Domains
{
    public class AppointmentDiary
    {
        public int ID { get; set; }

        public string Title { get; set; }
        [Display(Name = "Key")]
        public int SomeImportantKey { get; set; }

        [Display(Name = "DateTime Scheduled")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateTimeScheduled { get; set; }

        [Display(Name = "Appointment Duration")]
        public int AppointmentLength { get; set; }

        [Display(Name = "Status")]
        public int StatusENUM { get; set; }

        public string UserId { get; set; }
    }
}