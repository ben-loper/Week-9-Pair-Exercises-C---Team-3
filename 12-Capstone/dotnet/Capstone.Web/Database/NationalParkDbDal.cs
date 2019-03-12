using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.Database
{
    public class NationalParkDbDal : INationalParkDal
    {
        private string _connectionString;

        public NationalParkDbDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ParkModel GetParkDetailsByCode(string parkCode)
        {
            string getParksSql = "SELECT * FROM park WHERE parkCode = @parkCode";
            ParkModel park = new ParkModel();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getParksSql, conn);

                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    park.Code = Convert.ToString(reader["parkCode"]);
                    park.Name = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Acreage = Convert.ToInt32(reader["acreage"]);
                    park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                    park.MilesOfTrails = Convert.ToDouble(reader["milesOfTrail"]);
                    park.NumOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                    park.Climate = Convert.ToString(reader["climate"]);
                    park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                    park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                    park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    park.Description = Convert.ToString(reader["parkDescription"]);
                    park.EntryFee = Convert.ToDecimal(reader["entryFee"]);
                    park.NumOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                }
            }
            return park;
        }

        public List<IndexViewParkModel> GetParks()
        {
            List<IndexViewParkModel> parks = new List<IndexViewParkModel>();

            string getParksSql = "SELECT parkCode, parkName, state, parkDescription FROM park";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getParksSql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IndexViewParkModel park = new IndexViewParkModel();

                    park.Code = Convert.ToString(reader["parkCode"]);
                    park.Name = Convert.ToString(reader["parkName"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Description = Convert.ToString(reader["parkDescription"]);

                    parks.Add(park);
                }
            }


            return parks;
        }

        public List<WeatherModel> GetWeatherForecast(string parkCode)
        {
            List<WeatherModel> forecast = new List<WeatherModel>();

            string getForecastSql = "SELECT fiveDayForecastValue, low, high," +
                " forecast FROM weather WHERE parkCode = 'CVNP';";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getForecastSql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WeatherModel dayForecast = new WeatherModel();

                    dayForecast.ForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                    dayForecast.TempLow = Convert.ToInt32(reader["low"]);
                    dayForecast.TempHigh = Convert.ToInt32(reader["high"]);
                    dayForecast.Forecast = Convert.ToString(reader["forecast"]);

                    forecast.Add(dayForecast);
                }
            }
            return forecast;
        }
    }
}
