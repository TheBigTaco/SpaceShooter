using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Rename.Models;

namespace Rename.Controllers
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
