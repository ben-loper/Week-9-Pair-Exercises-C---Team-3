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

        public Dictionary<string, string> GetParksCodesAndNames()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            string getParksSql = "SELECT parkCode, parkName FROM park";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getParksSql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   results.Add(Convert.ToString(reader["parkCode"]), Convert.ToString(reader["parkName"]));
                }
            }

            return results;
        }

        public List<SurveyResultsViewModel> GetTopSurveyResults()
        {
            List<SurveyResultsViewModel> parks = new List<SurveyResultsViewModel>();

            string getParksSql = "SELECT COUNT(park.parkCode) AS numberOfVotes, park.parkCode, park.parkName, park.parkDescription FROM park JOIN survey_result ON park.parkCode = survey_result.parkCode GROUP BY park.parkName, park.parkDescription, park.parkCode ORDER BY numberOfVotes DESC, parkName, park.parkCode; ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getParksSql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SurveyResultsViewModel park = new SurveyResultsViewModel();

                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.Name = Convert.ToString(reader["parkName"]);
                    park.Description = Convert.ToString(reader["parkDescription"]);
                    park.NumOfVotes = Convert.ToInt32(reader["numberOfVotes"]);

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

        public int SaveSurvey(Survey survey)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES(@parkCode, @emailAddress, @state, @activityLevel);", conn);
                cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                cmd.Parameters.AddWithValue("@state", survey.StateOfResidence);
                cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("Failed!!!!!!!!!!");
                }
            }

            return result;
        }
    }
}
