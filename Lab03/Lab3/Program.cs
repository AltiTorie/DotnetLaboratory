using System;
using System.Security.Cryptography;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[1a] Wczytanie tablicy a [,]");
            Console.WriteLine("[1b] Wczytanie tablicy b [][]");
            Console.WriteLine("[2] Krotki");
            Console.WriteLine("[3] Zmienna o nazwie class");
            Console.WriteLine("[4] 5 roznych metod z System.Arrays");
            Console.WriteLine("[5] Typ anonimowy");
            string inp = Console.ReadLine();
            switch (inp)
            {
                case "1a":
                    zad1a();
                    break;
                case "1b":
                    zad1b();
                    break;
                case "2":
                    zad2();
                    break;
                case "3":
                    zad3();
                    break;
                case "4":
                    zad4();
                    break;
                case "5":
                    zad5();
                    break;
                default:
                    Console.WriteLine("Zły wybór!");
                    break;
            }
        }

        private static void print2DArr(int[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                Console.Write("[");

                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i, j] + " ");
                }
                Console.WriteLine("]");
            }
            Console.WriteLine("");
        }

        private static (int, int) readDims()
        {
            Console.WriteLine("Input first dimension: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input second dimension:");
            int m = Convert.ToInt32(Console.ReadLine());
            return (n, m);
        }
        private static void zad1a()
        {
            (int n, int m) = readDims();
            int[,] tab = new int[n, m];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    tab[i, j] = rnd.Next(0, 50);
                }
            }
            print2DArr(tab);
            for (int i = 0, k = (n - 1); i < n / 2; i++, k--)
            {
                for (int j = 0; j < m; j++)
                {
                    (tab[i, j], tab[k, j]) = (tab[k, j], tab[i, j]);
                }
            }
            print2DArr(tab);

        }
        private static void prinArrofArrs(int[][] tab)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Console.Write("[");

                for (int j = 0; j < tab[0].Length; j++)
                {
                    Console.Write(tab[i][j] + " ");
                }
                Console.WriteLine("]");
            }
            Console.WriteLine("");
        }

        private static void zad1b()
        {
            (int n, int m) = readDims();
            int[][] tab = new int[n][];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                tab[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    tab[i][j] = rnd.Next(0, 50);
                }
            }
            prinArrofArrs(tab);
            Array.Reverse(tab);
            prinArrofArrs(tab);
        }

        private static void zad2fun((string name, string surname, int age, double pay) tuple)
        {
            Console.WriteLine($"1(names). {tuple.name} {tuple.surname}, aged {tuple.age} earns: {tuple.pay}zl");
            Console.WriteLine($"2(Items). {tuple.Item1} {tuple.Item2}, aged {tuple.Item3} earns: {tuple.Item4}zl");
            (string first, string family, int years, double earnings) = tuple;
            Console.WriteLine($"3(assigned). {first} {family}, aged {years} earns: {earnings}zl");
        }

        private static void zad2()
        {
            (string name, string surname, int age, double pay) tuple = (Console.ReadLine(), Console.ReadLine(), Convert.ToInt32(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()));
            zad2fun(tuple);
        }

        private static void zad3()
        {
            int @class = 5;
            Console.WriteLine(@class);
        }
        private static void printArr<T>(T[] arr)
        {
            Console.Write("[");
            for (int i = 0; i < arr.Length - 1; i++)
            {
                Console.Write(arr[i] + ",");
            }
            Console.WriteLine(arr[arr.Length - 1] + "]");
        }

        private static bool findIntBT4(int val)
        {
            return val > 4;
        }
        private static void zad4()
        {
            int[] arr1 = new int[] { 5, 4, 2, 1, 3 };
            Console.WriteLine("1. Array.Sort");
            Console.WriteLine("arr1");
            printArr(arr1);
            Array.Sort(arr1);
            Console.WriteLine("arr1 after sort: ");
            printArr(arr1);
            Console.WriteLine("");

            Console.WriteLine("2. Array.Copy");
            Console.WriteLine("arr1: ");
            printArr(arr1);
            int[] arr2 = new int[5];
            Console.WriteLine("arr2: ");
            printArr(arr2);
            Array.Copy(arr1, arr2, 3);
            Console.WriteLine("arr2 after Copy");
            printArr(arr2);
            Console.WriteLine("");

            Console.WriteLine("3. Array.Clear");
            Console.WriteLine("arr2: ");
            printArr(arr2);
            Array.Clear(arr2, 0, arr2.Length);
            Console.WriteLine("arr2: ");
            printArr(arr2);
            Console.WriteLine("");

            Console.WriteLine("4. Array.Find");
            arr2 = new int[] { 4, 3, 2, 1, 7, 3, 9, 4 };
            int found = Array.Find(arr2, findIntBT4);
            printArr(arr2);
            Console.WriteLine($"found: {found}");
            Console.WriteLine("");

            Console.WriteLine("5. Array.Reverse");
            Console.WriteLine("arr1");
            printArr(arr1);
            Array.Reverse(arr1);
            Console.WriteLine("arr1 after reverse:");
            printArr(arr1);
            Console.WriteLine("");


        }

        private static void zad5()
        {
            var jasio = new { imie = Console.ReadLine(), nazwisko = Console.ReadLine(), wiek = Convert.ToInt32(Console.ReadLine()), pensja = Convert.ToDouble(Console.ReadLine()) };
            zad5fun(jasio);
        }

        private static void zad5fun(dynamic person)
        {
            Console.WriteLine($"1(names). {person.imie} {person.nazwisko}, aged {person.wiek} earns: {person.pensja}zl");
        }    
    }


}
