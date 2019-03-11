using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public int Acreage { get; set; }
        public int ElevationInFeet { get; set; }
        public double MilesOfTrails { get; set; }
        public int NumOfCampsites { get; set; }
        public string Climate { get; set; }
        public int YearFounded { get; set; }
        public int AnnualVisitorCount { get; set; }
        public string InspirationalQuote { get; set; }
        public string InspirationalQuoteSource { get; set; }
        public string Description { get; set; }
        public decimal EntryFee { get; set; }
        public int NumOfAnimalSpecies { get; set; }
    }
}
