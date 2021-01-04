using System;

namespace Zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input your number:");
            int number = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < number; i++)
            {
                for (int j = number-1; j > i; j--)
                {
                    Console.Write(" ");
                }
                for (int k = 0; k <= i; k++)
                {
                    Console.Write("X");
                }
                Console.WriteLine();
            }
        }
    }
}
