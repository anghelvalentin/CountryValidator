using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class SwedenValidator : IdValidationAbstract
    {
        public SwedenValidator()
        {
            CountryCode = nameof(Country.SE);
        }

        /// <summary>
        /// Orgnr (Organisationsnummer, Swedish company number)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }
            else if (number.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            return number.CheckLuhnDigit() ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Validate PERSONNUMMER
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!(number.Length == 10 || number.Length == 12))
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.Invalid("Only numbers are allowed");
            }
            try
            {
                var year = int.Parse(number.Substring(0, 2)) + 1900;
                var month = int.Parse(number.Substring(2, 2));
                var day = int.Parse(number.Substring(4, 2));
                DateTime date = new DateTime(year, month, day);
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }

            return number.Substring(0, 10).CheckLuhnDigit() ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// VAT-nummer or momsnummer 
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat?.RemoveSpecialCharacthers();
            vat = vat?.Replace("SE", string.Empty).Replace("se", string.Empty);

            if (!Regex.IsMatch(vat, @"^\d{10}01$"))
            {
                return ValidationResult.InvalidFormat("123456789001");
            }
            int[] Multipliers = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            var index = 0;
            var sum = 0;
            foreach (var m in Multipliers)
            {
                var temp = vat[index++].ToInt() * m;
                sum += temp > 9 ? (int)Math.Floor(temp / 10D) + temp % 10 : temp;
            }

            var checkDigit = 10 - sum % 10;

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == vat[9].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNNNN or NNN NN");
            }
            return ValidationResult.Success();
        }
    }
}
