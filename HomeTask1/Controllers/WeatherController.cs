using HomeTask1.DAL.Models;
using HomeTask1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HomeTask1.Controllers
{
    public class WeatherController : Controller
    {
        private IWeatherService ws;

        public WeatherController(IWeatherService ws)
        {
            this.ws = ws;
        }

        private async void AddToDB(int id, string city, string country)
        {
            var context = new  WeatherDBContext();
            var item =  new Query()
            {
                cityId=id,
                name=city,
                country=country,
                time=DateTime.Now
            };
            context.Queries.Add(item);
            await context.SaveChangesAsync();
        }

        //
        // GET: /Weather/
        public async Task<ActionResult> Index(string city="Lviv")
        {
            ws.linkFormat = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid=fce02a07226c189ccac0cdbcb3d4325a";
            
            var x = await ws.GetWeather<WeatherObject>(city);
            ViewBag.City = x.name;
            ViewBag.Weather = x;
            AddToDB(x.id, city, x.sys.country);
            ViewBag.ListOfCities = await CityGenerator.Get();
            return View();
        }

        public async Task<ActionResult> ThreeDays(string city = "Lviv")
        {
            var context = new WeatherDBContext();
            var cities = context.Cities.ToList();
            ws.linkFormat = "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&APPID=fce02a07226c189ccac0cdbcb3d4325a";
            var x = await ws.GetWeather<WeatherObjectList>(city);
            ViewBag.City = x.city.name;
            ViewBag.Weather = x.list.Take(3);
            AddToDB(x.city.id, city, x.city.country);
            ViewBag.ListOfCities = await CityGenerator.Get();
            return View();
        }

        public async Task<ActionResult> SevenDays(string city = "Lviv")
        {
            ws.linkFormat = "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&APPID=fce02a07226c189ccac0cdbcb3d4325a";
            var x = await ws.GetWeather<WeatherObjectList>(city);
            ViewBag.City = x.city.name;
            ViewBag.Weather = x.list;
            AddToDB(x.city.id, city, x.city.country);
            ViewBag.ListOfCities = await CityGenerator.Get();
            return View();
        }

        [HttpPost]
        public ActionResult RemoveFavourite(string city = "Lvov")
        {
            var context = new WeatherDBContext();
            var item = context.Cities.Where(x => x.name == city).FirstOrDefault();
            context.Cities.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddFavourite(string city = "Lvov")
        {
            var context = new WeatherDBContext();
            HomeTask1.DAL.Models.City item = new HomeTask1.DAL.Models.City();

            ws.linkFormat = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid=fce02a07226c189ccac0cdbcb3d4325a";

            var x = await ws.GetWeather<WeatherObject>(city);

            item.Id = x.id;
            item.name = x.name;
            item.country = x.sys.country;
            var temp = context.Cities.Where(s=>s.name==city).FirstOrDefault();
            if(temp==null)
                context.Cities.Add(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFavouriteThree(string city = "Lvov")
        {
            var context = new WeatherDBContext();
            var item = context.Cities.Where(x => x.name == city).FirstOrDefault();
            context.Cities.Remove(item);
            context.SaveChanges();
            return RedirectToAction("ThreeDays");
        }

        [HttpPost]
        public async Task<ActionResult> AddFavouriteThree(string city = "Lvov")
        {
            var context = new WeatherDBContext();
            HomeTask1.DAL.Models.City item = new HomeTask1.DAL.Models.City();

            ws.linkFormat = "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&APPID=fce02a07226c189ccac0cdbcb3d4325a";
            var x = await ws.GetWeather<WeatherObjectList>(city);

            item.Id = x.city.id;
            item.name = x.city.name;
            item.country = x.city.country;
            var temp = context.Cities.Where(s => s.name == city).FirstOrDefault();
            if (temp == null)
                context.Cities.Add(item);
            context.SaveChanges();
            return RedirectToAction("ThreeDays");
        }

        [HttpPost]
        public ActionResult RemoveFavouriteSeven(string city = "Lvov")
        {
            var context = new WeatherDBContext();
            var item = context.Cities.Where(x => x.name == city).FirstOrDefault();
            context.Cities.Remove(item);
            context.SaveChanges();
            return RedirectToAction("SevenDays");
        }

        public async Task<ActionResult> AddFavouriteSeven(string city = "Lvov")
        {
            var context = new WeatherDBContext();
            HomeTask1.DAL.Models.City item = new HomeTask1.DAL.Models.City();

            ws.linkFormat = "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&APPID=fce02a07226c189ccac0cdbcb3d4325a";
            var x = await ws.GetWeather<WeatherObjectList>(city);

            item.Id = x.city.id;
            item.name = x.city.name;
            item.country = x.city.country;
            var temp = context.Cities.Where(s => s.name == city).FirstOrDefault();
            if (temp == null)
                context.Cities.Add(item);
            context.SaveChanges();
            return RedirectToAction("Sevendays");
        }
        public ActionResult History()
        {
            var context = new WeatherDBContext();
            ViewBag.List = context.Queries.OrderByDescending(x=>x.time).ToList();
            return View();
        }
	}
}