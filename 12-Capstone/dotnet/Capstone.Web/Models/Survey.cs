using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {
        [Display(Name = "Favorite National Park")]
        public string ParkCode { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "State of Residence")]
        public string StateOfResidence { get; set; }

        [Display(Name = "Activity Level")]
        public string ActivityLevel { get; set; }
    }
}
