using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab12.Controllers
{

    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GuessNumber(int number)
        {

            ViewData["color"] = Colors.ORANGE;
            int correct = (int)HttpContext.Session.GetInt32("number");
            int range = (int)HttpContext.Session.GetInt32("range");


            if (number == correct)
            {
                ViewData["response"] = $"Gratulacje! Wylosowana liczba to {number}";
                ViewData["color"] = Colors.GREEN;
            }
            else if (number < correct)
            {
                ViewData["response"] = $"Niestety, liczba {number} jest za mała!";
                if (number < 0)
                {
                    ViewData["response"] = $"Niestety, liczba {number} jest spoza zakresu 0 - {range}!";
                    ViewData["color"] = Colors.RED;
                }
            }
            else
            {
                ViewData["response"] = $"Niestety, liczba {number} jest za duża!";
                if (number > range)
                {
                    ViewData["response"] = $"Niestety, liczba {number} jest spoza zakresu 0 - {range}!";
                    ViewData["color"] = Colors.RED;
                }
            }

            return View("Guess");
        }
        public IActionResult DrawNumber()
        {
            Random r = new Random();
            //Number = r.Next(Range);
            int range = (int)HttpContext.Session.GetInt32("range");
            HttpContext.Session.SetInt32("number", r.Next(range));
            ViewData["response"] = "Nowa liczba wylosowana";
            ViewData["color"] = Colors.GREEN;
            return View("Draw");

        }
        public IActionResult SetRange(int range)
        {
            ViewData["response"] = "Nie udało się ustawić zakresu, musi być większy od 0";
            ViewData["color"] = Colors.RED;
            if (range > 0)
            {
                //Range = range;
                HttpContext.Session.SetInt32("range", range);
                ViewData["response"] = $"Zakres zmieniony na 0-{range}";
                ViewData["color"] = Colors.GREEN;
            }
            return View("Set");
        }

        public IActionResult Info()
        {
            ViewData["port"] = HttpContext.Connection.LocalPort;
            return View("GameInfo");
        }
    }
    public enum Colors
    {
        RED,
        ORANGE,
        GREEN
    }
}
