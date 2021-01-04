using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadanie:");
            Console.WriteLine("[1]. ListOfArrayList");
            Console.WriteLine("[2]. Lists addition");
            Console.Write("Input: ");
            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    zad1();
                    break;
                case 2:
                    zad2();
                    break;
                default:
                    break;
            }
        }

        private static void zad1()
        {

            Console.WriteLine("________________________INTS________________________");
            var a = new ListOfArrays<int>(5);
            a.Trim();
            a.Add(5);
            a.Remove(5);
            Console.WriteLine(a);
            var ints = new ListOfArrays<int>(4) { 1, 3, 6, 2, 8, 0, 4, 7, 1 };

            ints.Remove(3);
            Console.WriteLine(ints);
            Console.WriteLine("________________________STRINGS________________________");
            var strs = new ListOfArrays<string>() { "abc", "def", "g", "h", "i", "j" };
            Console.WriteLine(strs);
            Console.WriteLine("Remove at 0");
            strs.RemoveAt(0);
            Console.WriteLine("Remove g");
            strs.Remove("g");
            Console.WriteLine(strs);
            Console.WriteLine("Trim");
            strs.Trim();
            Console.WriteLine(strs);

            Console.WriteLine("________________________STUDENTS________________________");
            var studs = new ListOfArrays<Student>
            {
                new Student("Janek", "Kowalski", 5),
                new Student("Tomek", "Kowalski", 2),
                new Student("Ania", "Bania", 2),
                new Student("Jarek", "Kaczorek", 3)
            };
            Console.WriteLine(studs);
            Console.WriteLine("Remove Ania Bania 2");
            studs.Remove(new Student("Ania", "Bania", 2));
            Console.WriteLine($"Last elem = {studs[^1]}");
            Console.WriteLine(studs);

            Console.WriteLine("________________________MIXED NUMBERS________________________");
            MixedNumber n = new MixedNumber(4, 5);
            var nums = new ListOfArrays<MixedNumber>(2)
            {
                n,
                new MixedNumber(1, 3, 5),
                new MixedNumber(3, 2, 10),
                new MixedNumber(1, 3, 5),
                n
            };
            Console.WriteLine("Writing using foreach");
            foreach (var item in nums)
            {
                Console.WriteLine($"\'{item}\'");
            }
            Console.WriteLine("Remove 4/5");
            nums.Remove(n);
            Console.WriteLine(nums);
            Console.WriteLine("Trim");
            nums.Trim();
            Console.WriteLine(nums);
        }

        private static void zad2()
        {
            Console.WriteLine("________________________INTS SUM________________________");
            var ints1 = new ListOfArrays<int>(4) { 1, 3, 6, 2 };
            var ints2 = new ListOfArrays<int>(4) { 2, 4, 7, 3 };

            var ints_sum = ints1 + ints2;
            Console.WriteLine(ints1);
            Console.WriteLine(ints2);
            Console.WriteLine($"sum = {ints_sum}");

            Console.WriteLine("________________________MIXED NUMBERS SUM________________________");
            var nums1 = new ListOfArrays<MixedNumber>(2)
            {
                new MixedNumber(4, 5),
                new MixedNumber(1, 3, 5),
                new MixedNumber(3, 2, 10),
                new MixedNumber(1, 3, 5)
            };

            var nums2 = new ListOfArrays<MixedNumber>(2)
            {
                new MixedNumber(1, 5),
                new MixedNumber(9, 4, 10),
                new MixedNumber(7, 8, 40),
                new MixedNumber(0, 1, 2)
            };
            Console.WriteLine(nums1);
            Console.WriteLine(nums2);
            Console.WriteLine($"sum = {nums1 + nums2}");

        }

    }

    class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Year { get; set; }
        public Student(string name, string surname, int year)
        {
            Name = name;
            Surname = surname;
            Year = year;
        }
        public override string ToString()
        {
            return $"{Name} {Surname} - {Year}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Student)
            {
                var s = obj as Student;
                return Name == s.Name && Surname == s.Surname && Year == s.Year;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Year);
        }
    }

    class ListOfArrays<T> : IEnumerable<T>, IList<T>
    {
        private readonly List<T[]> list = new List<T[]>();
        private int Length { get; set; }
        private int LastIndex { get; set; }

        public ListOfArrays(int length = 5)
        {
            Length = length;
            list.Add(new T[Length]);
            LastIndex = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index > Count)
                {
                    throw new IndexOutOfRangeException();
                }
                (int listIndex, int arrayIndex) = GetRelativeIndexes(index);
                return list[listIndex][arrayIndex];

            }
            set
            {
                if (index > Count)
                {
                    throw new IndexOutOfRangeException();
                }
                (int listIndex, int arrayIndex) = GetRelativeIndexes(index);
                list[listIndex][arrayIndex] = value;
            }
        }

        private (int, int) GetRelativeIndexes(int index)
        {
            return (index / Length, index % Length);
        }
        public int Count
        {
            get { return LastIndex; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            if (Count == list.Count * Length)
            {
                list.Add(new T[Length]);
            }
            this[LastIndex] = item;
            LastIndex++;
        }

        public static ListOfArrays<T> operator +(ListOfArrays<T> first,
                                                    IEnumerable<T> other)
        {
            ListOfArrays<T> temp = new ListOfArrays<T>();
            foreach (T item in first)
            {
                temp.Add(item);
            }
            foreach (T item in other)
            {
                temp.Add(item);
            }
            return temp;
        }


        public void Clear()
        {
            list.Clear();
            list.Add(new T[Length]);
            LastIndex = 0;
        }

        public bool Contains(T item)
        {
            return list.Any(t => t.Contains(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            if (Contains(item))
            {
                for (int i = 0; i < list.Count * Length; i++)
                {
                    if (item.Equals(this[i]))
                    {
                        return i;
                    }
                }
            }
            return -1;

        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            bool wasDel = false;
            while (Contains(item))
            {
                RemoveAt(IndexOf(item));
                wasDel = true;
            }
            return wasDel;
        }

        public void RemoveAt(int index)
        {
            if (index < Count)
            {
                for (int i = index; i < Count - 1; i++)
                {
                    this[i] = this[i + 1];
                }
                LastIndex--;
                this[LastIndex] = default;
            }
        }

        public override string ToString()
        {
            string str = "{";
            for (int i = 0; i < list.Count; i++)
            {
                str += "[";
                for (int j = 0; j < list[i].Length - 1; j++)
                {
                    str += $"{list[i][j]}, ";
                }
                str += $"{list[i][^1]}]";
            }
            str += "}";
            return str;
        }

        public void Trim()
        {
            int empty = (list.Count * Length) - Count;
            int toCut = empty / Length;
            int start = list.Count - toCut;
            list.RemoveRange(start, toCut);
        }

        private class ListEnumerator : IEnumerator<T>
        {
            private int pos = -1;
            private ListOfArrays<T> List { get; set; }
            public T Current
            {
                get
                {
                    if (pos > List.LastIndex)
                    {
                        throw new InvalidOperationException();
                    }
                    return List[pos];
                }
            }

            public ListEnumerator(ListOfArrays<T> list)
            {
                List = list;
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
            bool IEnumerator.MoveNext()
            {
                pos++;
                return pos < List.LastIndex;
            }

            void IEnumerator.Reset()
            {
                pos = -1;
            }
        }

    }


}
