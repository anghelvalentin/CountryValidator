using CountryValidation;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in CountryValidator.SupportedCountries)
            {
                Console.WriteLine(item);
            }
        }
    }
}
