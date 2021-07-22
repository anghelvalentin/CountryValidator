using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class GermanyValidator : IdValidationAbstract
    {
        readonly Dictionary<string, string[]> regions = new Dictionary<string, string[]>()
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
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!id.All(char.IsDigit))
            {
                return ValidationResult.Invalid("Invalid format. Only numbers are allowed");
            }
            else if (!(id.Length == 10 || id.Length == 11 || id.Length == 13))
            {
                return ValidationResult.InvalidLength();
            }

            foreach (var item in regions)
            {
                for (int i = 0; i < item.Value.Length; i++)
                {
                    if (Regex.IsMatch(id, item.Value[i]))
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
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("01234567890");
            }

            else if (id[0] == '0')
            {
                return ValidationResult.Invalid("Invalid format. The first digit must never be 0.");
            }

            char[] digits = id.ToCharArray();
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

            bool isValid = int.Parse(id[10].ToString()) == checksum;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Umsatzsteur Identifikationnummer (VAT)  
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("DE", string.Empty).Replace("de", string.Empty);
            if (!Regex.IsMatch(vatId, @"^[1-9]\d{8}$"))
            {
                return ValidationResult.InvalidFormat("123456789");
            }

            var product = 10;
            for (var index = 0; index < 8; index++)
            {
                var sum = (vatId[index].ToInt() + product) % 10;
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

            bool isValid = checkDigit == vatId[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
