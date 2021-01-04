using System;

namespace Lab2
{
    class Program_Zad1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadanie [1]: funkcja kwadratowa");
            Console.WriteLine("Zadanie [2]: liczby binarne, hex i operatory binarne");
            Console.WriteLine("Zadanie [3]: druga najwieksza liczba");
            Console.Write("Podaj numer zadania: ");
            int nr = Convert.ToInt32(Console.ReadLine());
            switch (nr)
            {
                case 1:
                    zad1();
                    break;
                case 2:
                    zad2();
                    break;
                case 3:
                    zad3();
                    break;
                default:
                    Console.WriteLine("Zły numer");
                    break;

            }
        }



        public static void zad1()
        {
            Console.Write("Podaj parametr a: ");
            double par_a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Podaj parametr b: ");
            double par_b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Podaj parametr c: ");
            double par_c = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Twoje rownanie: ({par_a})x^2 + ({par_b})x + ({par_c}) = 0");

            double delta = Math.Pow(par_b, 2) - (4 * par_a * par_c);
            if (par_a == 0)
            {
                if (par_b == 0)
                {
                    if(par_c == 0)
                    {
                        Console.WriteLine("Twoje rownanie ma nieskonczenie wiele rozwiazan");
                    }
                    Console.WriteLine("Twoje rownanie nie ma rozwiazan");
                }
                else
                {
                    double x = (-par_c / par_b);
                    Console.WriteLine("Twoje rownanie ma jedno rozwiazanie: ");
                    Console.WriteLine("x = {0:F5}", x);
                }
            }
            else if (delta > 0)
            {
                Console.WriteLine($"delta = {delta}");
                double sqr_d = Math.Sqrt(delta);
                double x1 = ((-par_b - sqr_d) / (2 * par_a));
                double x2 = ((-par_b + sqr_d) / (2 * par_a));

                Console.WriteLine("Twoje rownanie ma 2 rozwiazania: ");
                Console.WriteLine($"x1 = {x1:F5}");
                Console.WriteLine($"x2 = {x2:F5}");

            }
            else if (delta == 0)
            {
                double x = ((-par_b) / (2 * par_a));
                Console.WriteLine("Twoje rownanie ma jedno rozwiazanie: ");
                Console.WriteLine("x = {0:F5}", x);
            }
            else
            {
                Console.WriteLine("Twoje rownanie nie ma rozwiazan");
            }
        }

        public static void zad2()
        {
            Console.Write("Podaj a: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Podaj b: ");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Binary representation: ");
            Console.WriteLine("a: {0}", Convert.ToString(a, 2));
            Console.WriteLine("b: {0}", Convert.ToString(b, 2));

            Console.WriteLine($"~a: {~a:X}");
            Console.WriteLine($"~b: {~b:X}");
            Console.WriteLine($"a & b: {a & b:X}");
            Console.WriteLine($"b & a: {b & a:X}");
            Console.WriteLine($"a | b: {a | b:X}");
            Console.WriteLine($"b | a: {b | a:X}");
        }

        static void zad3()
        {
            Console.Write("Podaj liczbe >= 1: ");
            int n = Convert.ToInt32(Console.ReadLine());

            double biggest = int.MinValue;
            double previous = int.MinValue;
            double second = int.MinValue;
            bool all_equal = true;

            int counter = 0;
            while (counter < n)
            {
                string line = Console.ReadLine();

                int index = 0;

                while (index < line.Length && counter < n)
                {
                    string number_string = "";
                    while (index < line.Length && !Char.IsWhiteSpace(line[index]) && counter < n)
                    {
                        number_string += line[index];
                        index++;
                    }

                    double current = 0;
                    bool isNum = double.TryParse(number_string.Replace('.', ','), out current);
                    if (isNum)
                    {
                        if (counter == 0)
                        {
                            biggest = current;
                            previous = biggest;
                        }

                        if (current != previous && all_equal)
                        {
                            all_equal = false;
                        }
                        if (current > biggest)
                        {
                            second = biggest;
                            biggest = current;
                        }
                        else if (current > second && current < biggest)
                        {
                            second = current;
                        }
                        previous = current;

                        counter++;
                    }
                    index++;
                }
            }

            if (all_equal)
            {
                Console.WriteLine("Brak rozwiazania");
            }
            else
            {
                Console.WriteLine(second);
            }

        }
    }
}
