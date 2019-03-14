using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class User
    {
        public string DegreeUnits { get; set; } = "F";

        public void ChangeUnitPreference()
        {
            if (DegreeUnits == "F")
            {
                DegreeUnits = "C";
            }
            else
            {
                DegreeUnits = "F";
            }
        }
    }
}
