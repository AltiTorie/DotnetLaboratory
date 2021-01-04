#define VERBOSE

using System;
using System.Linq;

namespace Lab4
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz zadanie: ");
            Console.WriteLine("[1a]. GetFromConsoleXY krotka");
            Console.WriteLine("[1b]. GetFromConsoleXY parametr");
            Console.WriteLine("[2]. DrawCard");
            Console.WriteLine("[3]. CountMyTypes");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1a":
                    (int a, int b) = GetFromConsoleXYa("Wpisz pierwsza liczbe: ", "Wpisz druga liczbe: ");
                    Console.WriteLine($"a: {a}, b: {b}");
                    break;
                case "1b":
                    int val1, val2;
                    GetFromConsoleXYb("Wpisz pierwsza liczbe: ", "Wpisz druga liczbe: ", out val1, out val2);
                    Console.WriteLine($"a: {val1}, b: {val2}");
                    break;
                case "2":
                    DrawCard("Ryszard", "Rys", 'X', 2, 21);
                    DrawCard("ABCDEFGHIJKLMNOPQRSTUWXYZ", "ALAMAKOTA", '-', 3, 40);
                    DrawCard("Tomasz", cardWidth: 35);
                    DrawCard("JanuszeX", frameWidth: 7, frame: '*');
                    break;
                case "3":
                    (int even, int realPlus, int fiveString, int others) = CountMyTypes(2, 4, 3, -8, 5.0, 7.6, "abrakadabra", -4.0, "five", 3);
                    Console.WriteLine($"even: {even}, realPlus: {realPlus}, fiveString: {fiveString}, others: {others}");
                    break;
                default:
                    Console.WriteLine("Zly wybor");
                    break;
            }

        }

        private static (int, int) GetFromConsoleXYa(string comment1, string comment2)
        {
            Console.WriteLine(comment1);
#if (VERBOSE)
            Console.WriteLine("Now input your integer: ");
#endif
            int inputOne = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(comment2);
#if (VERBOSE)
            Console.WriteLine("Now input your second integer: ");
#endif
            int inputTwo = Convert.ToInt32(Console.ReadLine());
            return (inputOne, inputTwo);
        }

        private static void GetFromConsoleXYb(string comment1, string comment2, out int inputOne, out int inputTwo)
        {
            Console.WriteLine(comment1);
#if (VERBOSE)
            Console.WriteLine("Now input your integer: ");
#endif
            inputOne = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(comment2);
#if (VERBOSE)
            Console.WriteLine("Now input your second integer: ");
#endif
            inputTwo = Convert.ToInt32(Console.ReadLine());
        }

        private static void DrawCard(string name, string surname = "", char frame = '+', int frameWidth = 3, int cardWidth = 25)
        {
            string line = "";
            cardWidth = new[] { cardWidth, (frameWidth * 2 + name.Length), (frameWidth * 2 + surname.Length) }.Max();
            int nameSpacesLength = (cardWidth - name.Length) - (2 * frameWidth);
            if (nameSpacesLength % 2 != 0)
            {
                nameSpacesLength += 1;
            }
            nameSpacesLength /= 2;
            int surnameSpacesLength = (cardWidth - surname.Length) - (2 * frameWidth);
            if (surnameSpacesLength % 2 != 0)
            {
                surnameSpacesLength += 1;
            }
            surnameSpacesLength /= 2;
            cardWidth = new[] { cardWidth, (frameWidth * 2 + nameSpacesLength * 2 + name.Length), (frameWidth * 2 + surnameSpacesLength * 2 + surname.Length) }.Max();
#if (VERBOSE)
            Console.WriteLine($"Card will be drawn for {name} {surname}, width of {cardWidth}, framed with {frame}");
#endif
            //top chars 2 times
            for (int i = 0; i < frameWidth; i++)
            {
                addLine(ref line, frame, cardWidth);
                Console.WriteLine(line);
                line = "";
            }
            //name line
            line = "";
            addLine(ref line, frame, frameWidth);
            addLine(ref line, ' ', nameSpacesLength);
            line += name;
            addLine(ref line, ' ', nameSpacesLength);
            addLine(ref line, frame, frameWidth);
            Console.WriteLine(line);
            //surname line
            line = "";
            addLine(ref line, frame, frameWidth);
            addLine(ref line, ' ', surnameSpacesLength);
            line += surname;
            addLine(ref line, ' ', surnameSpacesLength);
            addLine(ref line, frame, frameWidth);
            Console.WriteLine(line);
            line = "";
            //bottom chars 2 times
            for (int i = 0; i < frameWidth; i++)
            {
                addLine(ref line, frame, cardWidth);
                Console.WriteLine(line);
                line = "";
            }
        }

        private static void addLine(ref string line, char frame, int times)
        {
            for (int i = 0; i < times; i++)
            {
                line += frame;
            }
        }

        private static (int, int, int, int) CountMyTypes(params object[] ps)
        {
            int evenCount = 0, realPlusCount = 0, fiveStringCount = 0, othersCount = 0;
            foreach (object item in ps)
            {
#if (DEBUG)
                Console.WriteLine($"Now checking: {item}");
#endif
                switch (item)
                {
                    case int even when (even % 2 == 0):
                        {
                            evenCount++;
#if (DEBUG)
                            Console.WriteLine($"{even} is even");
#endif
                            break;
                        }
                    case double real when (real > 0):
                        {

                            realPlusCount++;
#if (DEBUG)
                            Console.WriteLine($"{real} is real+");
#endif
                            break;
                        }
                    case string word when (word.Length >= 5):
                        {

                            fiveStringCount++;
#if (DEBUG)
                            Console.WriteLine($"{word} is string with length > 5");
#endif


                            break;
                        }
                    default:
                        othersCount++;
#if (DEBUG)
                        Console.WriteLine($"{item} is other");
#endif
                        break;
                }
            }
            return (evenCount, realPlusCount, fiveStringCount, othersCount);
        }
    }
}
