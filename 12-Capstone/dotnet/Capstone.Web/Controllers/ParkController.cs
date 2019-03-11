using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Database;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class ParkController : SessionController
    {
        private INationalParkDal _nationParkDal;

        public ParkController(INationalParkDal nationalParkDal)
        {
            _nationParkDal = nationalParkDal;
        }

        public IActionResult Index()
        {
            List<IndexViewParkModel> parks = _nationParkDal.GetParks();

            return View(parks);
        }

        public IActionResult ParkDetails(string parkCode)
        {
            ParkModel park = _nationParkDal.GetParkDetailsByCode(parkCode);

            return View(park);
        }
    }
}