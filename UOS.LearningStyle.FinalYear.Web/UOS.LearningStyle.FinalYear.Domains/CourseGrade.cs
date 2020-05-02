using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOS.LearningStyle.FinalYear.Domains
{//Course Grade information
    public class CourseGrade
    {//get and set allows the property to be read and write
        public int ID { get; set; }

        [Display(Name = "Number")]
        public string Number { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        public int Mark { get; set; }
        //Number of credits must be between 0 and 25
        [Range(0, 25)]
        public int Credits { get; set; }

        public string UserId { get; set; }
    }
}

