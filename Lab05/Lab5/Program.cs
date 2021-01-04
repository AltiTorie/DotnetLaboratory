using System;
using System.Xml.Schema;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz zadanie:");
            Console.WriteLine("[1]. MixedNumber");
            Console.WriteLine("[2]. Extended string");

            int input = Convert.ToInt32(Console.ReadLine());

            switch (input)
            {
                case 1:
                    zad1();
                    break;

                case 2:
                    zad2();
                    break;
                case 3:
                    test();
                    break;
            }

        }
        public static void test()
        {
            int x = 2;
            Console.WriteLine(x++ + ++x);
        }
        public static void zad1()
        {
            MixedNumber a = new MixedNumber(4, 6, 10);
            MixedNumber b = new MixedNumber(2, 2, 7);
            MixedNumber c = new MixedNumber(9);
            MixedNumber d = new MixedNumber(4, 5);
            MixedNumber e = new MixedNumber(-3, -5, -10);
            MixedNumber f = new MixedNumber(0);
            MixedNumber g = new MixedNumber(1, 5);
            MixedNumber h = new MixedNumber(7, 10);
            MixedNumber i = new MixedNumber(-5, 4, 5);


            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            Console.WriteLine($"c = {c}");
            Console.WriteLine($"d = {d}");
            Console.WriteLine($"e = {e}");
            Console.WriteLine($"f = {f}");
            Console.WriteLine($"g = {g}");
            Console.WriteLine($"h = {h}");
            Console.WriteLine($"i = {i}");
            Console.WriteLine("--------------Sumowanie-----------------");
            Console.WriteLine($"a + b = {a + b}");
            Console.WriteLine($"b + c = {b + c}");
            Console.WriteLine($"c + d = {c + d}");
            Console.WriteLine($"d + e = {d + e}");
            Console.WriteLine($"e + f = {e + f}");
            Console.WriteLine($"g + h = {g + h}");
            Console.WriteLine($"d + g = {d + g}");
            Console.WriteLine($"e + i = {e + i}");
            Console.WriteLine($"i + d = {i + d}");
            Console.WriteLine("--------------Jako double-----------------");
            Console.WriteLine($"c = {c.Double} as double");
            Console.WriteLine($"d = {d.Double} as double");
            Console.WriteLine($"e = {e.Double} as double");
            Console.WriteLine($"c + d = {(c + d).Double} as double");
            Console.WriteLine("--------------Liczba modyfikacji-----------------");
            Console.WriteLine($"a modifications: {MixedNumber.Modifications}");

            try
            {
                MixedNumber zero = new MixedNumber(5, 2, 0);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Caught division by zero exception");
            }
        }

        public static void zad2()
        {
            string line = "to123jest321moja, ()linia";
            Console.WriteLine(StringMod.modifyString(line));
            Console.WriteLine(line.modifyString());
            Console.WriteLine("to123jest321moja, ()linia".modifyString());
        }
    }

    public static class StringMod
    {
        public static string modifyString(this string str)
        {
            string mod = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsLetter(str[i]))
                {
                    mod += '.';
                }
                else if (i % 2 == 0)
                {
                    mod += Char.ToUpper(str[i]);
                }
                else
                {
                    mod += Char.ToLower(str[i]);
                }
            }
            return mod;
        }
    }

    class MixedNumber
    {
        private int denominator;
        private int nominator;
        public static int Modifications { get; private set; }
        public int Total { get; set; }
        public int Nominator
        {
            get { return nominator; }
            set
            {
                if (value < 0)
                {
                    nominator = -value;
                    Modifications++;
                }
                else
                {
                    nominator = value;
                }
            }
        }

        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0 && Nominator != 0)
                {
                    throw new DivideByZeroException();
                }
                if (value < 0)
                {
                    denominator = -value;
                    Modifications++;
                }
                else
                {
                    denominator = value;
                }
            }
        }

        public double Double
        {
            get
            {
                if (Nominator == 0 || Denominator == 0)
                {
                    return Total;
                }
                int sign = Math.Sign(Total) >= 0 ? 1 : -1;
                return ((double)(Total * Denominator + sign * Nominator)) / Denominator;
            }
        }
        public MixedNumber(int total, int nominator, int denominator)
        {
            Total = total;
            Nominator = nominator;
            Denominator = denominator;
            divideFraction();
            if (Nominator >= Denominator && Denominator != 0)
            {
                if (Total < 0)
                {
                    Total = Total - Nominator / Denominator;
                }
                else
                {
                    Total = Total + Nominator / Denominator;
                }
                Nominator = Nominator % Denominator;
            }
        }

        public MixedNumber(int nominator, int denominator) : this(0, nominator, denominator) { }

        public MixedNumber(int total) : this(total, 0, 0) { }


        public static MixedNumber operator +(MixedNumber number,
                                             MixedNumber other)
        {
            int newTotal = number.Total + other.Total;
            int newNominator;
            int newDenominator;

            if (number.Nominator == 0 || number.Denominator == 0)
            {
                newNominator = other.Nominator;
                newDenominator = other.Denominator;
            }
            else if (other.Nominator == 0 || other.Denominator == 0)
            {
                newNominator = number.Nominator;
                newDenominator = number.Denominator;
            }
            else
            {
                int ns = Math.Sign(number.Total) >= 0 ? 1 : -1;
                int os = Math.Sign(other.Total) >= 0 ? 1 : -1;
                int nom = number.Total * number.Denominator + ns * number.Nominator;
                int oth = other.Total * other.Denominator + os * other.Nominator;
                newDenominator = number.Denominator * other.Denominator;
                newNominator = nom * other.Denominator + oth * number.Denominator;
                if (Math.Abs(newNominator) >= newDenominator && newDenominator != 0)
                {
                    newTotal = newNominator / newDenominator;
                    newNominator = newNominator % newDenominator;
                }
            }
            return new MixedNumber(newTotal, newNominator, newDenominator);
        }


        private void divideFraction()
        {
            int gcd = GCD(Nominator, Denominator);
            if (gcd != 1)
            {
                if (gcd == 0)
                {
                    nominator = 0;
                    denominator = 0;
                    Modifications++;
                }
                else
                {
                    nominator = nominator / gcd;
                    denominator = denominator / gcd;
                    Modifications++;
                }
            }

        }

        public override string ToString()
        {
            if (Nominator == 0)
            {
                return $"{Total}";
            }
            if (Total == 0)
            {
                return $"{Nominator}/{Denominator}";
            }
            return $"{Total} {Nominator}/{Denominator}";
        }

        private static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }
    }


}
