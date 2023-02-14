using System;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type a number:");
            string input = Console.ReadLine();

            bool isOctal = input.StartsWith("0") && !input.StartsWith("0x");
            bool isHexadecimal = input.StartsWith("0x") || input.StartsWith("0X");

            double num;
            if (isOctal)
            {
                num = ParseOctal(input);
            }
            else if (isHexadecimal)
            {
                num = ParseHexadecimal(input);
            }
            else
            {
                num = double.Parse(input);
            }

            long integerPart = (long)num;
            double fractionPart = num - integerPart;

            Console.WriteLine($"Decimal: {num}");
            Console.WriteLine($"Binary:  {Convert.ToString(integerPart, 2)}{(fractionPart == 0 ? "" : "." + GetFractionalBinary(fractionPart))}");
            Console.WriteLine($"Octal:   {(isOctal ? input : "0" + Convert.ToString(integerPart, 8) + (fractionPart == 0 ? "" : "." + GetFractionalOctal(fractionPart)))}");
            Console.WriteLine($"Hex:     {(isHexadecimal ? input : "0x" + (Convert.ToString(integerPart, 16).ToUpper()) + (fractionPart == 0 ? "" : "." + GetFractionalHexadecimal(fractionPart)))}");
        }

        static string GetFractionalBinary(double fraction)
        {
            string result = "";
            for (int i = 0; i < 10 && fraction > 0; i++)
            {
                fraction *= 2;
                if (fraction >= 1)
                {
                    result += "1";
                    fraction -= 1;
                }
                else
                {
                    result += "0";
                }
            }
            return result;
        }

        static string GetFractionalOctal(double fraction)
        {
            string result = "";
            for (int i = 0; i < 10 && fraction > 0; i++)
            {
                fraction *= 8;
                int digit = (int)fraction;
                result += digit.ToString();
                fraction -= digit;
            }
            return result;
        }

        static string GetFractionalHexadecimal(double fraction)
        {
            string result = "";
            for (int i = 0; i < 10 && fraction > 0; i++)
            {
                fraction *= 16;
                int digit = (int)fraction;
                if (digit < 10)
                {
                    result += digit.ToString();
                }
                else
                {
                    result += (char)('A' + digit - 10);
                }
                fraction -= digit;
            }
            return result;
        }

        static double ParseOctal(string input)
        {
            int dotIndex = input.IndexOf('.');
            string intPart = dotIndex >= 0 ? input.Substring(0, dotIndex) : input;
            string fracPart = dotIndex >= 0 ? input.Substring(dotIndex) : "";

            long decimalIntPart = 0;
            for (int i = 0; i < intPart.Length; i++)
            {
                decimalIntPart = decimalIntPart * 8 + (intPart[i] - '0');
            }

            double decimalFracPart = 0;
            if (fracPart != "")
            {
                double factor = 1.0 / 8;
                for (int i = 1; i < fracPart.Length; i++)
                {
                    decimalFracPart += (fracPart[i] - '0') * factor;
                    factor /= 8;
                }
            }

            return decimalIntPart + decimalFracPart;
        }

        static double ParseHexadecimal(string input)
        {
            int dotIndex = input.IndexOf('.');
            string intPart = dotIndex >= 0 ? input.Substring(0, dotIndex) : input;
            string fracPart = dotIndex >= 0 ? input.Substring(dotIndex) : "";

            long decimalIntPart = 0;
            for (int i = 2; i < intPart.Length; i++)
            {
                decimalIntPart = decimalIntPart * 16 + Convert.ToInt32(intPart[i].ToString(), 16);
            }

            double decimalFracPart = 0;
            if (fracPart != "")
            {
                double factor = 1.0 / 16;
                for (int i = 1; i < fracPart.Length; i++)
                {
                    decimalFracPart += Convert.ToInt32(fracPart[i].ToString(), 16) * factor;
                    factor /= 16;
                }
            }
            return decimalIntPart + decimalFracPart;
        }
    }
}