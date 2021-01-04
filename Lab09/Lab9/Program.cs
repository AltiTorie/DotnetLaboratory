using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz zadanie:");
            Console.WriteLine("[1]. LINQ search top 10");
            Console.WriteLine("[2]. Group Students");
            Console.WriteLine("[3a]. Group Topics");
            Console.WriteLine("[3b]. Group Topics by Gender");
            Console.WriteLine("[4]. StudentWithTopics");
            Console.WriteLine("[5]. Full capacity");
            //string input = Console.ReadLine();
            //switch (input)
            //{
            //    case "1":
            //        Zad1();
            //        break;
            //    case "2":
            //        Zad2();
            //        break;
            //    case "3a":
            //        Zad3a();
            //        break;
            //    case "3b":
            //        Zad3b();
            //        break;
            //    case "4":
            //        Zad4();
            //        break;
            //    case "5":
            //        Zad5();
            //        break;
            //    default:
            //        break;
            //}
            Zad1();
            Zad2();
            Zad3a();
            Zad3b();
            Zad4();
            Zad5();
        }
        private static void Zad1()
        {
            Console.WriteLine("Big:");
            var dict = TopOccurencesInFile("F:\\Workspace\\Dotnet\\Lab9\\TextFiles\\big.txt", 10);
            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            Console.WriteLine("Small:");
            dict = TopOccurencesInFile("F:\\Workspace\\Dotnet\\Lab9\\TextFiles\\small.txt", 10);
            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            
        }
        private static void Zad2()
        {

            List<Student> students = Generator.GenerateStudentsEasy();
            var list = GroupStudents(students, 5);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"Group nr.{i + 1}");
                foreach (Student s in list[i])
                {
                    Console.WriteLine($"\t{s}");
                }
            }
        }
        private static void Zad3a()
        {
            List<Student> students = Generator.GenerateStudentsEasy();
            foreach (string t in SortedTopics(students))
            {
                Console.WriteLine($"{t}");
            }
        }
        private static void Zad3b()
        {
            List<Student> students = Generator.GenerateStudentsEasy();
            foreach (var t in SortedTopicsByGender(students))
            {
                Console.WriteLine(t.Key);
                t.Value.ForEach(t => Console.WriteLine($"\t{t}"));
            }

        }
        private static void Zad4()
        {
            foreach (var s in ConvertStudentsToStudentsWithTopics(Generator.GenerateStudentsEasy()))
            {
                Console.WriteLine(s);
            }
        }
        private static void Zad5()
        {
            List<Vehicle> vehicles = new List<Vehicle>(){
                new Bicycle(2, 1, true, true),
                new Bicycle(2, 2, false, false),
                new PassengerCar(4, 2, true,"BMW" ,"Red", true),
                new PassengerCar(4, 4, false,"AUDI" ,"Blue", false) ,
                new Truck(8, 2, true, "TIR", 5000, Truck.FuelValue.Diesel),
                new Truck(4, 4, false, "BIGCAR", 2500, Truck.FuelValue.Petrol)
            };

            Scale scale = new Scale();
            MethodInfo info = scale.GetType().GetMethod("MaxLoad");
            if (info != null)
            {
                int res = (int)info.Invoke(scale, new object[] { vehicles });
                Console.WriteLine($"Result = {res}");
            }



        }

        private static Dictionary<string,int> TopOccurencesInFile(string path, int take)
        {
            if (path is null)
            {
                Console.WriteLine("Null path");
                return null;
            }
            if (path == "")
            {
                Console.WriteLine("Empty path");
                return null;
            }
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Console.WriteLine($"Directory does not exist {path}");
                return null;
            }
            if (!File.Exists(path))
            {
                Console.WriteLine($"File could not be found in path: {path}");
                return null;
            }

            try
            {
                using var sr = new StreamReader(path);

                string input_text = sr.ReadToEnd();
                var dict = Regex
                    .Matches(input_text, @"[A-Za-z]+")
                    .Select(w => w.Value.ToLowerInvariant())
                    .Where(w => w != "")
                    .GroupBy(w => w)
                    .ToDictionary(w => w.Key, w => w.Count())
                    .OrderByDescending(v => v.Value)
                    .Take(take)
                    .ToDictionary(q => q.Key, q => q.Value);

                return dict;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private static List<List<Student>> GroupStudents(List<Student> students, int groupCapacity)
        {
            return students
                    .OrderBy(s => s.Name)
                    .ThenBy(s => s.Index)
                    .Select((s, pos) => new { Student = s, Group = pos / groupCapacity })
                    .GroupBy(s =>s.Group)
                    .Select(s => s
                            .Select(v => v.Student)
                            .ToList()
                            )
                    .ToList();
        }

        private static List<string> SortedTopics(List<Student> students)
        {
            return students
                  .SelectMany(s => s.Topics)
                  .GroupBy(s => s)
                  .Distinct()
                  .OrderByDescending(s => s.Count())
                  .Select(s => s.Key)
                  .ToList();

        }

        private static Dictionary<Gender, List<string>> SortedTopicsByGender(List<Student> students)
        {
            return students
                .GroupBy(s => s.Gender)
                .Select(s => new
                {
                    Gender = s.Key,
                    Topics = s.SelectMany(s => s.Topics)
                          .GroupBy(s => s)
                          .Distinct()
                          .OrderByDescending(s => s.Count())
                          .Select(s => s.Key)
                          .ToList()
                })
                .ToDictionary(s => s.Gender, s => s.Topics);

        }

        private static List<StudentWithTopics> ConvertStudentsToStudentsWithTopics(List<Student> students)
        {
            return students.Select(s => new StudentWithTopics(s.Id,
                                                s.Index,
                                                s.Name,
                                                s.Gender,
                                                s.Active,
                                                s.DepartmentId,
                                                getTopicIds(s.Topics)
                                                ))
                .ToList();
        }

        private static List<int> getTopicIds(List<string> topics)
        {
            List<int> tp = new List<int>();
            var generated = Generator.GenerateTopicsEasy();

            tp = topics
                .Join(generated, st => st, t => t.Name, (s, t) => t.Id)
                .ToList();
            
            return tp;
        }


    }

    class Scale
    {
        public Scale() { }

        public int MaxLoad(List<Vehicle> list)
        {
            return list
                .Where(v => v is Truck)
                .Sum(t => ((Truck)t).Capacity);
        }
    }


}

