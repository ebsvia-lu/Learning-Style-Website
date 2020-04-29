using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOS.LearningStyle.FinalYear.Domains
{
    public class CourseGrade
    {
        public int ID { get; set; }

        [Display(Name = "Number")]
        public string Number { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        public int Mark { get; set; }

        [Range(0, 25)]
        public int Credits { get; set; }

        public string UserId { get; set; }
    }
}

