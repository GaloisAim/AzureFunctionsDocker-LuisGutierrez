using System;
using System.Collections;

namespace docdemo
{
    public class UnitFactory
    {
        public static Unit getUnitObject(string from, string to, string value)
        {
            if (Distance.isValid(from, to))
            {
                return new Distance(value);
            }
            else if (Temperature.isValid(from, to))
            {
                return new Temperature(value);
            }
            else if (Weight.isValid(from, to))
            {
                return new Weight(value);
            }
            else
            {
                return null;
            }
        }
    }

    public abstract class Unit
    {
        public double originalValue;

        protected Unit(string stringValue)
        {
            this.originalValue = Convert.ToDouble(stringValue);
        }

        public abstract Nullable<double> convert(string from, string to);
    }

    public class Distance : Unit
    {
        private const string Meter = "meter";
        private const string Feet = "feet";

        public Distance(string stringValue) : base(stringValue) { }

        private static ArrayList validUnits = new ArrayList { Meter, Feet };

        public static bool isValid(string from, string to)
        {
            return validUnits.Contains(from) && validUnits.Contains(to);
        }

        public override Nullable<double> convert(string from, string to)
        {
            if (from == Meter && to == Feet)
            {
                return originalValue * 3.2808;
            }
            else if (from == Feet && to == Meter)
            {
                return originalValue / 3.2808;
            }
            else
            {
                return null;
            }
        }
    }

    public class Temperature : Unit
    {
        private const string Farenheit = "farenheit";
        private const string Celsius = "celsius";

        public Temperature(string stringValue) : base(stringValue) { }

        private static ArrayList validUnits = new ArrayList { Farenheit, Celsius };

        public static bool isValid(string from, string to)
        {
            return validUnits.Contains(from) && validUnits.Contains(to);
        }

        public override Nullable<double> convert(string from, string to)
        {
            if (from == Celsius && to == Farenheit)
            {
                return originalValue * 1.8 + 32;
            }
            else if (from == Farenheit && to == Celsius)
            {
                return (originalValue - 32) / 1.8;
            }
            else
            {
                return null;
            }
        }
    }

    public class Weight : Unit
    {
        private const string Gram = "gram";
        private const string Ounce = "ounce";

        public Weight(string stringValue) : base(stringValue) { }

        private static ArrayList validUnits = new ArrayList { Gram, Ounce };

        public static bool isValid(string from, string to)
        {
            return validUnits.Contains(from) && validUnits.Contains(to);
        }

        public override Nullable<double> convert(string from, string to)
        {
            if (from == Gram && to == Ounce)
            {
                return originalValue * 0.035274;
            }
            else if (from == Ounce && to == Gram)
            {
                return originalValue / 0.035274;
            }
            else
            {
                return null;
            }
        }
    }
}
