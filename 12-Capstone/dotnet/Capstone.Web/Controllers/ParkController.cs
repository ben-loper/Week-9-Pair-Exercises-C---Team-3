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
            User user = GetActiveUser();
            List<IndexViewParkModel> parks = _nationParkDal.GetParks();

            return View(parks);
        }

        public IActionResult ParkDetails(string parkCode)
        {
            ParkDetailViewModel model = new ParkDetailViewModel();

            if(TempData["ParkCode"] != null && TempData["ParkCode"].ToString() != parkCode)
            {
                if(parkCode == null)
                {
                    parkCode = TempData["ParkCode"].ToString();
                }
                else
                {
                    TempData["ParkCode"] = parkCode;
                }                
            }
            
            TempData["ParkCode"] = parkCode;
            
          
            model.Park = _nationParkDal.GetParkDetailsByCode(parkCode);
            model.WeatherForecast = _nationParkDal.GetWeatherForecast(parkCode);
            model.CurrentUser = GetActiveUser();

            if(model.CurrentUser.DegreeUnits.ToLower() == "c")
            {
                model.ConvertToCelcius();
            }

            return View(model);
        }

        public IActionResult SetPreference()
        {
            User user = GetActiveUser();

            user.ChangeUnitPreference();

            SaveActiveUser(user);

            //TempData["ParkCode"] = TempData["ParkCode"];

            return RedirectToAction("ParkDetails");
        }

        [HttpGet]
        public IActionResult Survey()
        {
            Survey survey = new Survey();

            NewSurveyViewModel model = new NewSurveyViewModel();

            model.NewSurvey = survey;

            model.ParkNameDict = _nationParkDal.GetParksCodesAndNames();

            model.PopulateDropDown();

            return View(model);
        }

        [HttpPost]
        public IActionResult Survey(NewSurveyViewModel model)
        {
            // Call the method on the DAL to save the survey
            Survey survey = new Survey();

            survey.ActivityLevel = model.NewSurvey.ActivityLevel;

            survey.EmailAddress = model.NewSurvey.EmailAddress;

            survey.ParkCode = model.NewSurvey.ParkCode;

            survey.StateOfResidence = model.NewSurvey.StateOfResidence;

            _nationParkDal.SaveSurvey(survey);
            //Redirect to the survey results page
            
            return RedirectToAction("SurveyResults");
        }

        public IActionResult SurveyResults()
        {
            // Find out which parks has the most favorites
            List<SurveyResultsViewModel> model = _nationParkDal.GetTopSurveyResults();
            //Pass to view

            return View(model);
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