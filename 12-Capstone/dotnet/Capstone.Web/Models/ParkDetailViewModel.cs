using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkDetailViewModel
    {
        public ParkModel Park { get; set; }
        public List<WeatherModel> WeatherForecast { get; set; }
    }
}
