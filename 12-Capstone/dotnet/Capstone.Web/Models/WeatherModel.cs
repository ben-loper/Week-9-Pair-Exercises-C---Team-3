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

        public List<string> WeatherAdvice { get; set; } = new List<string>();

        public void GenerateAdvice()
        {
            if(Forecast.ToLower() == "snow")
            {
                WeatherAdvice.Add("Pack snowshoes.");
            }
            else if (Forecast.ToLower() == "rain")
            {
                WeatherAdvice.Add("Pack rain gear and wear waterproof shoes.");
            } 
            else if(Forecast.ToLower() == "thunderstorms")
            {
                WeatherAdvice.Add("Seek shelter and avoid hiking on exposed ridges.");
            }
            else if(Forecast.ToLower() == "sun")
            {
                WeatherAdvice.Add("Pack sunblock.");
            }

            if(TempHigh > 75)
            {
                WeatherAdvice.Add("Bring an extra gallon of water.");
            }

            if(TempHigh - TempLow > 20)
            {
                WeatherAdvice.Add("Wear breathable layers.");
            }

            if(TempLow < 20)
            {
                WeatherAdvice.Add("Frigid temperatures can cause hypothermia and frostbite.");
            }
            
        }
    }
}
