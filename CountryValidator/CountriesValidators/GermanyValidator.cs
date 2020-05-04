using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CountryValidator.Countries;

namespace CountryValidator.Countries
{
    public class GermanyValidator : IdValidationAbstract
    {
        Dictionary<string, string[]> regions = new Dictionary<string, string[]>()
        {
        {"Baden-Württemberg", new string[]{
                 @"\A(?<ff>\d{2})(?<bbb>\d{3})[\/]?(?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A28(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Bayern", new string[]{
                 @"\A(?<fff>\d{3})[\/]?(?<bbb>\d{3})[\/]?(?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A9(?<fff>\d{3})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Berlin", new string[]{
                 @"\A(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/]?(?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A11(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Brandenburg", new string[]{
                 @"\A0(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/]?(?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A30(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Bremen", new string[]{
                 @"\A(?<ff>\d{2})\s(?<bbb>\d{ 3})\s(?<uuuu>\d{ 4})(?<p>\d{1})\z",
                 @"\A24(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z/" } },
        {"Hamburg", new string[]{
                 @"\A(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/]?(?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A22(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Hessen", new string[]{
                 @"\A0(?<ff>\d{2})\s(?<bbb>\d{ 3})\s(?<uuuu>\d{ 4})(?<p>\d{1})\z",
                 @"\A26(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" }},
        {"Mecklenburg-Vorpommern", new string[]{
                 @"\A0(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A40(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Niedersachsen", new string[]{
                 @"\A(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A23(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Nordrhein-Westfalen", new string[]{
                 @"\A(?<fff>\d{3})[\/]?(?<bbbb>\d{4})[\/](?<uuu>\d{3})(?<p>\d{1})\z",
                 @"\A5(?<fff>\d{3})0(?<bbbb>\d{4})(?<uuu>\d{3})(?<p>\d{1})\z" } },
        {"Rheinland-Pfalz", new string[]{
                 @"\A(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A27(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Saarland", new string[]{
                 @"\A0(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A10(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Sachsen", new string[]{
                 @"\A2(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A32(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } } ,
        {"Sachsen-Anhalt", new string[]{
                 @"\A1(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A31(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } },
        {"Schleswig-Holstein", new string[]{
                 @"\A(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A21(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z",}},
        {"Thüringen", new string[]{
                 @"\A1(?<ff>\d{2})[\/]?(?<bbb>\d{3})[\/](?<uuuu>\d{4})(?<p>\d{1})\z",
                 @"\A41(?<ff>\d{2})0(?<bbb>\d{3})(?<uuuu>\d{4})(?<p>\d{1})\z" } }
    };

        public GermanyValidator()
        {
            CountryCode = nameof(Country.DE);

        }

        /// <summary>
        /// Steuernummer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.Invalid("Invalid format. Only numbers are allowed");
            }
            else if (!(number.Length == 10 || number.Length == 11 || number.Length == 13))
            {
                return ValidationResult.InvalidLength();
            }

            foreach (var item in regions)
            {
                for (int i = 0; i < item.Value.Length; i++)
                {
                    if (Regex.IsMatch(number, item.Value[i]))
                    {
                        return ValidationResult.Success();
                    }
                }
            }
            return ValidationResult.Invalid("Invalid code");
        }

        /// <summary>
        /// Validate German Tax Id Steueridentifikationsnummer
        /// </summary>
        /// <param name="taxNumber"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string taxNumber)
        {
            taxNumber = taxNumber.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(taxNumber, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("01234567890");
            }

            else if (taxNumber[0] == '0')
            {
                return ValidationResult.Invalid("Invalid format. The first digit is always 0");
            }

            char[] digits = taxNumber.ToCharArray();
            var first10Digits = digits.Take(10);


            var counts = first10Digits.GroupBy(x => x)
                  .Select(g => new { Value = g.Key, Count = g.Count() }).ToList();

            if (counts.Count != 9 && counts.Count != 8)
            {
                return ValidationResult.Invalid("Invalid");
            }
            int sum = 0;
            int product = 10;
            for (int i = 0; i <= 9; i++)
            {
                sum = (int)(Char.GetNumericValue(digits[i]) + product) % 10;
                if (sum == 0)
                {
                    sum = 10;
                }
                product = (sum * 2) % 11;
            }
            int checksum = 11 - product;
            if (checksum == 10)
            {
                checksum = 0;
            }

            bool isValid = int.Parse(taxNumber[10].ToString()) == checksum;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Umsatzsteur Identifikationnummer (VAT)  
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("DE", string.Empty).Replace("de", string.Empty);
            if (!Regex.IsMatch(vat, @"^[1-9]\d{8}$"))
            {
                return ValidationResult.InvalidFormat("123456789");
            }

            var product = 10;
            for (var index = 0; index < 8; index++)
            {
                var sum = (vat[index].ToInt() + product) % 10;
                if (sum == 0)
                {
                    sum = 10;
                }

                product = 2 * sum % 11;
            }

            var val = 11 - product;
            var checkDigit = val == 10
                ? 0
                : val;

            bool isValid = checkDigit == vat[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
