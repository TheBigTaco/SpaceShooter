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
    }
}
