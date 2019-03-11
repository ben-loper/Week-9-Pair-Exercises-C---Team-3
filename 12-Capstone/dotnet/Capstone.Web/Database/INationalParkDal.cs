using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Database
{
    public interface INationalParkDal
    {
        List<IndexViewParkModel> GetParks();

        ParkModel GetParkDetailsByCode(string parkCode);

        List<WeatherModel> GetWeatherForecast(string parkCode);
    }
}
