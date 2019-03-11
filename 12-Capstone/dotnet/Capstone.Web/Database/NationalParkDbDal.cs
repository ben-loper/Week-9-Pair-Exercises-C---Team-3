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
        ParkModel park = new ParkModel();

        private string _connectionString;

        public NationalParkDbDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ParkModel GetParkDetailsByCode(string parkCode)
        {
            string getParksSql = "SELECT * FROM park WHERE parkCode = @parkCode";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getParksSql, conn);

                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ParkModel park = new ParkModel();

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
    }
}
