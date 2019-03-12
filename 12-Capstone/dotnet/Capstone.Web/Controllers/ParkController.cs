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
            ParkDetailViewModel model = new ParkDetailViewModel();
            model.Park = _nationParkDal.GetParkDetailsByCode(parkCode);
            model.WeatherForecast = _nationParkDal.GetWeatherForecast(parkCode);
            

            return View(model);
        }

        public IActionResult SetPreference(ParkDetailViewModel model)
        {
            

            return View("ParkDetails", model);
        }

        private User GetActiveUser()
        {
            User user = null;

            // See if the user has a shopping cart stored in session            
            if (GetSessionData<User>("User") == null)
            {
                user = new User();
                SaveActiveUser(user);
            }
            else
            {
                user = GetSessionData<User>("User"); // <-- gets the shopping cart out of session
            }

            // If not, create one for them

            return user;
        }

        private void SaveActiveUser(User user)
        {
            SetSessionData("User", user);        // <-- saves the shopping cart into session
        }
    }
}