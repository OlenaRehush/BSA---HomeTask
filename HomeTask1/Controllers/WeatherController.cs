using HomeTask1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeTask1.Controllers
{
    public class WeatherController : Controller
    {
        private WeatherService ws;
       

        //
        // GET: /Weather/
        public ActionResult Index(string city="Lviv")
        {
            ws = new WeatherService("http://api.openweathermap.org/data/2.5/weather?q={0}&appid=fce02a07226c189ccac0cdbcb3d4325a");
            var x=ws.GetWeather<WeatherObject>(city);
            ViewBag.City = x.name;
            ViewBag.Weather = x;
            return View();
        }

        public ActionResult ThreeDays(string city = "Lviv")
        {
            ws = new WeatherService("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&APPID=fce02a07226c189ccac0cdbcb3d4325a");
            var x = ws.GetWeather<WeatherObjectList>(city);
            ViewBag.City = x.city.name;
            ViewBag.Weather = x.list.Take(3);
            return View();
        }

        public ActionResult SevenDays(string city = "Lviv")
        {
            ws = new WeatherService("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&APPID=fce02a07226c189ccac0cdbcb3d4325a");
            var x = ws.GetWeather<WeatherObjectList>(city);
            ViewBag.City = x.city.name;
            ViewBag.Weather = x.list;
            return View();
        }
	}
}