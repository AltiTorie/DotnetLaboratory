using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
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
