using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Views
{
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Solve(int a=0, int b=0, int c=0)
        {
            List<string> colors = new List<string>
            {
                "orange",
                "blue",
                "green",
                "red"
            };

            ViewBag.Equation = $"Twoje rownanie: ({a})x^2 + ({b})x + ({c}) = 0";
            var res = QEsolver(a, b, c);
            string outcome;
            switch (res.Item1)
            {
                case 0:
                    outcome = "Twoje rownanie nie ma rozwiazan";
                    break;
                case 1:
                    outcome = "Twoje rownanie ma jedno rozwiazanie: ";
                    outcome += $"x = {res.Item2:F5}";
                    break;
                case 2:
                    outcome = "Twoje rownanie ma 2 rozwiazania: ";
                    outcome += $"x1 = {res.Item2:F5} oraz x2 = {res.Item3:F5}";
                    break;
                case 3:
                    outcome = "Twoje rownanie ma nieskonczenie wiele rozwiazania: ";
                    break;
                default:
                    outcome = "Something went wrong...";
                    break;
            }
            ViewData["result"] = outcome;
            ViewData["color"] = colors[res.Item1];
            return View("Solve");
        }

        private (int,double,double) QEsolver(int a, int b, int c)
        {
            (int count, double x1, double x2) result = (0,0,0);

            double delta = Math.Pow(b, 2) - (4 * a * c);
            if (a == 0)
            {
                if (b == 0)
                {
                    if (c == 0)
                    {
                        result.count = 3;
                    }
                    else
                    {

                        result.count = 0;
                    }
                }
                else
                {
                    result.x1 = (-c / b);

                    result.count = 1;
                }
            }
            else if (delta > 0)
            {
                double sqr_d = Math.Sqrt(delta);
                result.x1 = ((-b - sqr_d) / (2 * a));
                result.x2 = ((-b + sqr_d) / (2 * a));
                result.count = 2;

            }
            else if (delta == 0)
            {
                result.x1 = ((-b) / (2 * a));
                result.count = 1;
            }
            else
            {

                result.count = 0;
            }
            return result;
        }

        public IActionResult Info()
        {
            ViewData["port"] = HttpContext.Connection.LocalPort;
            return View("ToolInfo");
        }
    }

}
