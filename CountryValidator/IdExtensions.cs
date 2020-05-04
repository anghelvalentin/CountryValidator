using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountryValidator
{
    public static class IdExtensions
    {
        public static IEnumerable<int> ToDigitEnumerable(this int number)
        {
            IList<int> digits = new List<int>();

            while (number > 0)
            {
                digits.Add(number % 10);
                number = number / 10;
            }

            //digits are currently backwards, reverse the order
            return digits.Reverse();
        }

        public static string RemoveSpecialCharacthers(this string ssn)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ssn?.Length; i++)
            {
                if (char.IsLetterOrDigit(ssn[i]))
                {
                    sb.Append(ssn[i]);
                }
            }
            return sb.ToString();
        }

        public static int ToInt(this char c)
        {
            return Convert.ToInt32(c) - Convert.ToInt32('0');
        }



        public static int Sum(this string input, int[] multipliers, int start = 0)
        {
            var sum = 0;

            for (var index = start; index < multipliers.Length; index++)
            {
                var digit = multipliers[index];
                sum += input[index].ToInt() * digit;
            }

            return sum;
        }

        public static string Slice(this string input, int startIndex)
        {
            return input.Substring(startIndex);
        }

        public static string Slice(this string input, int startIndex, int length)
        {
            return input.Substring(startIndex, length);
        }

        public static bool CheckLuhnDigit(this string stringDigits)
        {
            int lastDigit = (int)Char.GetNumericValue(stringDigits[stringDigits.Length - 1]);
            stringDigits = stringDigits.Substring(0, stringDigits.Length - 1);
            var digits = stringDigits.Select(c => (int)Char.GetNumericValue(c)).ToList();
            int[] results = { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };
            var i = 0;
            var lengthMod = digits.Count % 2;
            return lastDigit == (digits.Sum(d => i++ % 2 == lengthMod ? d : results[d]) * 9) % 10;
        }

        public static string Translit(this string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya" };
            string[] rus_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }

        public static int Mod(this int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
