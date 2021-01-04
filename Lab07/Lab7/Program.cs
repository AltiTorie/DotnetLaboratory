using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose program: ");
            Console.WriteLine("[1]. ");
            Console.WriteLine("[3]. ");

            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    zad1_2();
                    break;
                case 3:
                    zad3();
                    break;
            }

        }

        private static void zad1_2()
        {
            //string file_path = Console.ReadLine();
            //"F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\big.txt"
            //"F:\Workspace\Dotnet\Lab7\TextFiles\small.txt"
            Console.WriteLine("null path:");
            readFile(null);
            Console.WriteLine("empty string:");
            readFile("");
            Console.WriteLine("Non existing file:");
            readFile("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\NonExistingFile.txt");
            Console.WriteLine("Wrong path:");
            readFile("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\WrongPath\\file.txt");
            Console.WriteLine("Not an txt:");
            readFile("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\file.csv");
            Console.WriteLine("Big:");
            readFile("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\big.txt");
            Console.WriteLine("Small:");
            readFile("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\small.txt");
        }


        private static void readFile(string path)
        {
            try
            {
                using var sr = new StreamReader(path);
                string input_text = sr.ReadToEnd();
                string[] words = Regex
                    .Split(input_text, "[^A-Za-z]")
                    .Select(word => word.ToLowerInvariant())
                    .ToArray();
                Dictionary<string, int> dict = new Dictionary<string, int>();

                for (int i = 0; i < words.Length; i++)
                {
                    if (dict.ContainsKey(words[i]))
                    {
                        dict[words[i]] += 1;
                    }
                    else
                    {
                        dict.Add(words[i], 1);
                    }
                }
                dict.Remove("");
                var sorted = dict
                    .OrderByDescending(v => v.Value)
                    .Take(10);
                foreach (var s in sorted)
                {
                    Console.WriteLine(s);
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Path not specified");
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Path was an empty string");
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File could not be found {0}", path);
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Directory does not exist {0}", path);
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine("Specified path is invalid {0}", path);
                Console.WriteLine(e.Message);
            }
        }

        private static void zad3()
        {
            Console.WriteLine("null path:");
            readFile_NoExceptions(null);
            Console.WriteLine("empty string:");
            readFile_NoExceptions("");
            Console.WriteLine("Non existing file:");
            readFile_NoExceptions("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\NonExistingFile.txt");
            Console.WriteLine("Wrong path:");
            readFile_NoExceptions("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\WrongPath\\file.txt");
            Console.WriteLine("Not an txt:");
            readFile_NoExceptions("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\file.csv");
            Console.WriteLine("Big:");
            readFile_NoExceptions("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\big.txt");
            Console.WriteLine("Small:");
            readFile_NoExceptions("F:\\Workspace\\Dotnet\\Lab7\\TextFiles\\small.txt");
        }

        private static void readFile_NoExceptions(string path)
        {
            if (path is null)
            {
                Console.WriteLine("Null path");
                return;
            }
            if (path == "")
            {
                Console.WriteLine("Empty path");
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Console.WriteLine("Directory does not exist {0}", path);
                return;
            }
            if (!File.Exists(path))
            {
                Console.WriteLine("File could not be found in path: {0}", path);
                return;
            }

            try
            {
                using var sr = new StreamReader(path);

                string input_text = sr.ReadToEnd();
                string[] words = Regex
                    .Split(input_text, "[^A-Za-z]")
                    .Select(word => word.ToLowerInvariant())
                    .ToArray();
                Dictionary<string, int> dict = new Dictionary<string, int>();

                for (int i = 0; i < words.Length; i++)
                {
                    if (dict.ContainsKey(words[i]))
                    {
                        dict[words[i]] += 1;
                    }
                    else
                    {
                        dict.Add(words[i], 1);
                    }
                }
                dict.Remove("");
                var sorted = dict
                    .OrderByDescending(v => v.Value)
                    .Take(10);
                foreach (var s in sorted)
                {
                    Console.WriteLine(s);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
