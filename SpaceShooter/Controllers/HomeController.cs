using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/gamepage")]
        public ActionResult Gamepage()
        {
            return View();
        }

        [HttpGet("/profile")]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpGet("/search")]
        public ActionResult Search()
        {
            return View();
        }

    }
}
