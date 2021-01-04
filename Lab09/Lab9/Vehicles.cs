using System;
using System.Collections.Generic;
using System.Text;

namespace Lab9
{
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
        public int GetCapacity()
        {
            return capacity;
        }
        public FuelValue Fuel { get; set; }
        public enum FuelValue
        {
            Diesel, Gas, Petrol
        }
        public Truck(int wheels, int seats, bool electricWindows, string brand, int load, FuelValue fuel) : base(wheels, seats, electricWindows, brand)
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
        public PassengerCar(int wheels, int seats, bool electricWidows, string brand, string color, bool hasGoodtires) : base(wheels, seats, electricWidows, brand)
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
