using System;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose one:");
            Console.WriteLine("[1]. Classes");
            Console.WriteLine("[2]. Interface");

            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    zad1();
                    break;
                case 2:
                    zad2();
                    break;
            }

        }

        public static void zad1()
        {
            Vehicle[] vehicles = {
                new Bicycle(2, 1, true, true),
                new Bicycle(2, 2, false, false),
                new PassengerCar(4, 2, true,"BMW" ,"Red", true),
                new PassengerCar(4, 4, false,"AUDI" ,"Blue", false) ,
                new Truck(8,2,true,"TIR",5000, Truck.FuelValue.Diesel),
                new Truck(4,4,false,"BIGCAR", 2500, Truck.FuelValue.Petrol)
            };

            foreach (Vehicle v in vehicles)
            {
                Console.WriteLine(v);
            }
            foreach (Vehicle v in vehicles)
            {
                Console.WriteLine(v as Car);
            }
            int maxLoad = 0;
            foreach (Vehicle v in vehicles)
            {
                if (v is Truck)
                {
                    maxLoad += ((Truck)v).Capacity;
                }
            }
            Console.WriteLine($"My fleet can carry {maxLoad}");

            vehicles[0].BreakWheel();
            Console.WriteLine(vehicles[0].Wheels);
            try
            {
                vehicles[0].BreakWheel();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            ((Bicycle)vehicles[0]).Sound();
            ((Bicycle)vehicles[1]).Sound();
            ((PassengerCar)vehicles[2]).Drive();
            ((PassengerCar)vehicles[3]).Drive();
            ((Truck)vehicles[4]).AddTrailer(1000);
            ((Truck)vehicles[5]).AddTrailer(1000);

            Console.WriteLine(((Truck)vehicles[4]).Capacity);
            Console.WriteLine(((Truck)vehicles[5]).Capacity);

        }
        public static void zad2()
        {
            object[] objects =
            {
                new Figure(2,3),
                new InteriorFigure(3.0,2.0,"Red"),
                new InteriorFigure(1.5,0,"Blue"),
                new Figure(3.0,2.0),
                3
            };
            showColors(objects);
        }

        public static void showColors(object[] objects)
        {
            foreach (object obj in objects)
            {
                string color = (obj as IHasInterior)?.Color ?? "no color";
                Console.WriteLine(color);
            }
        }
    }

    public interface IFigure
    {
        void moveTo(double x, double y);
    }

    public interface IHasInterior
    {
        string Color
        {
            get; set;
        }
    }

    public class Figure : IFigure
    {
        public double PosX { get; private set; }
        public double PosY { get; private set; }
        public Figure(double x, double y)
        {
            PosX = x;
            PosY = y;
        }
        public void moveTo(double x, double y)
        {
            PosX = x;
            PosY = y;
        }
    }

    public class InteriorFigure : IFigure, IHasInterior
    {
        public double PosX { get; private set; }
        public double PosY { get; private set; }
        string IHasInterior.Color { get; set; }

        public InteriorFigure(double x, double y, string color)
        {
            PosX = x;
            PosY = y;
            ((IHasInterior)this).Color = color;
        }
        public void moveTo(double x, double y)
        {
            PosX = x;
            PosY = y;
        }

    }

    public abstract class Vehicle
    {
        protected int wheels;
        protected int seats;
        public int Wheels
        {
            get { return wheels; }
            private set
            {
                if (value >= 1)
                {
                    wheels = value;
                }
                else
                {
                    throw new ArgumentException("Vehicle must have at least 1 wheel");
                }
            }
        }
        public int Seats
        {
            get { return seats; }
            set
            {
                if (value >= 1)
                {
                    seats = value;
                }
                else
                {
                    throw new ArgumentException("Vehicle must have at least 1 seat");
                }
            }
        }

        public void BreakWheel()
        {
            Wheels -= 1;
        }

        public Vehicle(int wheels, int seats)
        {
            Wheels = wheels;
            Seats = seats;
        }

        public override string ToString()
        {
            return $"{Seats} seated vehicle on {Wheels} wheels";
        }
    }

    public abstract class Car : Vehicle
    {
        public bool ElectricWindows { get; set; }
        public string Brand { get; set; }
        public Car(int wheels, int seats, bool electricWindows, string brand) : base(wheels, seats)
        {
            ElectricWindows = electricWindows;
            Brand = brand;
        }


        public override string ToString()
        {
            string wind = ElectricWindows ? "has" : "doesn't have";
            return base.ToString() + $" that's a {Brand} car and {wind} electric windows.";
        }
        public void PlaceGroceries()
        {
            Seats -= 1;
        }
    }

    public class Bicycle : Vehicle
    {
        public bool Ring { set; get; }
        public bool Fast { set; get; }

        public Bicycle(int wheels, int seats, bool ring, bool fast) : base(wheels, seats)
        {
            Ring = ring;
            Fast = fast;
        }
        public override string ToString()
        {
            string r = Ring ? "can" : "cannot";
            string fast = Fast ? "fast" : "slow";
            return base.ToString() + $" that's a {fast} bicycle and {r} ring the bell.";
        }
        public void Sound()
        {
            string shout = Ring ? "RING RING" : "MOVE!";
            Console.WriteLine(shout);
        }
    }

    public class Truck : Car
    {
        private int capacity;
        public int Capacity
        {
            get { return capacity; }
            set
            {
                if (value > 0)
                {
                    capacity = value;
                }
                else
                {
                    throw new ArgumentException("Truck must be albe to carry some load!");
                }
            }
        }
        public FuelValue Fuel { get; set; }
        public enum FuelValue
        {
            Diesel, Gas, Petrol
        }
        public Truck(int wheels, int seats, bool electricWindows,string brand, int load,FuelValue fuel) : base(wheels, seats, electricWindows, brand)
        {
            Capacity = load;
            Fuel = fuel;
        }
        public override string ToString()
        {
            return base.ToString() + $" Also it's a truck that can carry {Capacity} and runs on {Fuel}";
        }
        public void AddTrailer(int trailerCapacity)
        {
            Capacity += trailerCapacity;
        }
    }

    public class PassengerCar : Car
    {
        public string Color { get; set; }
        public bool HasGoodTires { get; set; }
        public PassengerCar(int wheels, int seats, bool electricWidows,string brand, string color, bool hasGoodtires) : base(wheels, seats, electricWidows, brand)
        {
            Color = color;
            HasGoodTires = hasGoodtires;
        }
        public override string ToString()
        {
            string tires = HasGoodTires ? "good" : "bad";
            return base.ToString() + $" Also it's a {Color} passenger car with {tires}";
        }
        public void Drive()
        {
            string sound = (Color == "Red") ? "Vziuuuumm" : "Brum brum";
            Console.WriteLine(sound);
        }
    }

}
