using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Dojodachi.Models;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {

        private void setPetInSession(Pet pet = null)
        {
            if (pet == null)
            {
                pet = new Pet();
                HttpContext.Session.SetString("newGame", "true");
            }
            HttpContext.Session.SetInt32("happiness", pet.Happiness);
            HttpContext.Session.SetInt32("fullness", pet.Fullness);
            HttpContext.Session.SetInt32("energy", pet.Energy);
            HttpContext.Session.SetInt32("meals", pet.Meals);

        }
        private Pet getPetFromSession()
        {
            return new Pet
            (
                HttpContext.Session.GetInt32("happiness"),
                HttpContext.Session.GetInt32("fullness"),
                HttpContext.Session.GetInt32("energy"),
                HttpContext.Session.GetInt32("meals")
            );
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("newGame") == null)
                setPetInSession();

            return View(getPetFromSession());
        }
        [HttpGet("game/{gameType}")]
        public IActionResult Play(string gameType)
        {
            Pet currPet = getPetFromSession();
            TempData["message"] = currPet.Play(gameType);
            setPetInSession(currPet);
            return RedirectToAction("Index");
        }
        [HttpGet("restart")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}