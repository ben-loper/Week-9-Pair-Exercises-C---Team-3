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
        public User CurrentUser { get; set; }


        public void ConvertToCelcius()
        {
            foreach(var forecast in WeatherForecast)
            {
                forecast.TempHigh = (forecast.TempHigh - 32) * 5 / 9;
                forecast.TempLow = (forecast.TempLow - 32) * 5 / 9;
            }            
            
        }
    }
}
