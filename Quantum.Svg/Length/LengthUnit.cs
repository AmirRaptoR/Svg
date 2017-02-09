using System;
using System.Collections.Generic;
using System.Linq;

namespace Quantum.Svg.Length
{
    public struct LengthUnit
    {
        private static readonly List<Tuple<string, LengthType>> LengthTypes = new List<Tuple<string, LengthType>>
        {
            new Tuple<string, LengthType>("mm", LengthType.Milimeter),
            new Tuple<string, LengthType>("cm", LengthType.Centimeter),
            new Tuple<string, LengthType>("m", LengthType.Meter),
            new Tuple<string, LengthType>("in", LengthType.Inch),
            new Tuple<string, LengthType>("", LengthType.Pixel),
            new Tuple<string, LengthType>("px", LengthType.Pixel)
        };

        private const double MilimeterPerInch = 25.4;
        private const double PixelsPerInch = 1000;
        public double Value { get; }
        public LengthType Type { get; }

        public LengthUnit(double value, LengthType type)
        {
            Value = value;
            Type = type;
        }

        public LengthUnit(LengthUnit lenght, LengthType type)
        {
            Type = type;
            Value = lenght.Convert(type).Value;
        }

        public LengthUnit Convert(LengthType type)
        {
            if (type == Type)
                return this;
            if (type == LengthType.Inch)
            {
                switch (Type)
                {
                    case LengthType.Milimeter:
                        return new LengthUnit(Value / MilimeterPerInch, LengthType.Inch);
                    case LengthType.Centimeter:
                        return new LengthUnit(Value * 100 / MilimeterPerInch, LengthType.Inch);
                    case LengthType.Meter:
                        return new LengthUnit(Value * 1000 / MilimeterPerInch, LengthType.Inch);
                    case LengthType.Inch:
                        return this;
                    case LengthType.Pixel:
                        return new LengthUnit(Value / PixelsPerInch, LengthType.Inch);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            var inchValue = Convert(LengthType.Inch);
            switch (type)
            {
                case LengthType.Milimeter:
                    return new LengthUnit(inchValue.Value * MilimeterPerInch, LengthType.Milimeter);
                case LengthType.Centimeter:
                    return new LengthUnit(inchValue.Value * MilimeterPerInch / 100, LengthType.Milimeter);
                case LengthType.Meter:
                    return new LengthUnit(inchValue.Value * MilimeterPerInch / 1000, LengthType.Milimeter);
                case LengthType.Inch:
                    return inchValue;
                case LengthType.Pixel:
                    return new LengthUnit(inchValue.Value * PixelsPerInch, LengthType.Pixel);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static LengthUnit Parse(string value)
        {
            var pos = value.LastIndexOfAny("0123456789".ToCharArray());
            var number = double.Parse(value.Substring(0, pos + 1));
            var measure = ParseLengthType(value.Substring(pos + 1));

            return new LengthUnit(number, measure);
        }

        private static LengthType ParseLengthType(string value)
        {
            var tmp = LengthTypes.FirstOrDefault(x => x.Item1 == value);
            if (tmp == null)
                throw new ArgumentException("Measure value is incorrect");
            return tmp.Item2;
        }

        public static implicit operator LengthUnit(string value)
        {
            return Parse(value);
        }

        public override string ToString()
        {
            return ToString(Type);
        }

        public string ToString(LengthType type)
        {
            var tmp = Convert(type);
            return $"{tmp.Value:F2}{GetPosteriorString(tmp.Type)}";
        }

        private static string GetPosteriorString(LengthType type)
        {
            return LengthTypes.First(x => x.Item2 == type).Item1;
        }
    }
}