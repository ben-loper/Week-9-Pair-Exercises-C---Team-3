using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class User
    {
        public string DegreeUnits { get; set; } = "Fahrenheit";

        public List<ParkModel> FavParks { get; set; } = new List<ParkModel>();

        public void ChangeUnitPreference()
        {
            if (DegreeUnits == "Farenheit")
            {
                DegreeUnits = "Celcius";
            }
            else
            {
                DegreeUnits = "Farenheit";
            }
        }
    }
}
