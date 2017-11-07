using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using SpaceShooter.Models;
using SpaceShooter.ViewModels;

namespace SpaceShooter.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            var sessionId = Request.Cookies["sessionId"];
            var model = new IndexModel(sessionId);

            return View(model);
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
        [HttpGet("/register")]
        public ActionResult Register()
        {
            var model = new RegisterModel();
            return View(model);
        }
        [HttpPost("/register")]
        public ActionResult RegisterNewUser()
        {
            var model = new RegisterModel();

            string username = Request.Form["user-name"];
            string password = Request.Form["user-password"];
            if (Player.DoesUsernameExist(username))
            {
                model.RegisterFailed = true;
                return View("Register", model);
            }
            else
            {
                string newSalt = Player.MakeSalt();
                Hash newHash = new Hash(password, newSalt);
                Player newPlayer = new Player(username, newHash.Result, newSalt);
                newPlayer.Save();
                model.RegisterSuccess = true;
                model.RegisteredPlayer = newPlayer;
                return View("Register", model);
            }
        }
        [HttpGet("/login")]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }
        [HttpPost("/login")]
        public ActionResult LoginUser()
        {
            var model = new LoginModel();
            Session newSession = Player.Login(Request.Form["user-name"], Request.Form["user-password"]);
            if(newSession == null)
            {
                model.LoginFailed = true;
                return View("Login", model);
            }
            else
            {
                model.LoginSuccess = true;
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("sessionId", newSession.SessionId, cookieOptions);
                return View("Login", model);
            }
        }
        [HttpGet("/logout")]
        public ActionResult Logout()
        {
            var sessionId = Request.Cookies["sessionId"];
            Session.DeleteById(sessionId);
            Response.Cookies.Delete("sessionId");
            return Redirect("/");
        }
    }
}
