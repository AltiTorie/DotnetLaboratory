using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Controllers
{
    public class GameController : Controller
    {

        public static int Number { get; private set; } = 5;
        private static int Range { get; set; } = 10;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GuessNumber(int number)
        {
            
            ViewData["color"] = Colors.ORANGE;
            if (number == Number)
            {
                ViewData["response"] = $"Gratulacje! Wylosowana liczba to {number}";
                ViewData["color"] = Colors.GREEN;
            }
            else if(number < Number)
            {
                ViewData["response"] = $"Niestety, liczba {number} jest za mała!";
                if(number < 0)
                {
                    ViewData["response"] = $"Niestety, liczba {number} jest spoza zakresu 0 - {Range}!";
                    ViewData["color"] = Colors.RED;
                }
            }
            else
            {
                ViewData["response"] = $"Niestety, liczba {number} jest za duża!";
                if (number > Range)
                {
                    ViewData["response"] = $"Niestety, liczba {number} jest spoza zakresu 0 - {Range}!";
                    ViewData["color"] = Colors.RED;
                }
            }
            return View("Guess");
        }
        public IActionResult DrawNumber()
        {
            Random r = new Random();
            Number = r.Next(Range);
            ViewData["response"] = "Nowa liczba wylosowana";
            ViewData["color"] = Colors.GREEN;
            return View("Draw");

        }
        public IActionResult SetRange(int range)
        {
            ViewData["response"] = "Nie udało się ustawić zakresu, musi być większy od 0";
            ViewData["color"] = Colors.RED;
            Console.WriteLine(range);
            if (range > 0 && range != Range)
            {
                Range = range;
                ViewData["response"] = $"Zakres zmieniony na 0-{Range}";
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
