using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class WeatherModel
    {
        public int ForecastValue { get; set; }
        public int TempLow { get; set; }
        public int TempHigh { get; set; }
        public string Forecast { get; set; }
    }
}
