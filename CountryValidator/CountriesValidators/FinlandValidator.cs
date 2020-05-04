using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class FinlandValidator : IdValidationAbstract
    {
        public FinlandValidator()
        {
            CountryCode = nameof(Country.FI);
        }

        /// <summary>
        /// Y-tunnus (Finnish business identifier).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }


        /// <summary>
        /// Henkilotunnus (HETU)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string value)
        {
            if (!Regex.IsMatch(value, "^[0-9]{6}[-+A][0-9]{3}[0-9ABCDEFHJKLMNPRSTUVWXY]$"))
            {
                return ValidationResult.Invalid("Invalid code");
            }

            var day = int.Parse(value.Substring(0, 2));
            var month = int.Parse(value.Substring(2, 2));
            var year = int.Parse(value.Substring(4, 2));
            var centuries = new Dictionary<char, int>(){
            { '+',  1800 },
            { '-', 1900},
            {  'A', 2000}
            };
            year = centuries[value[6]] + year;
            try
            {
                DateTime date = new DateTime(year, month, day);
                if (date > DateTime.Now)
                {
                    return ValidationResult.InvalidDate();
                }
            }
            catch
            {

                return ValidationResult.InvalidDate();
            }

            var individual = int.Parse(value.Substring(7, 3));
            if (individual < 2)
            {
                return ValidationResult.Invalid("Invalid");
            }
            var n = value.Substring(0, 6) + value.Substring(7, 3);
            long intn = long.Parse(n);
            return "0123456789ABCDEFHJKLMNPRSTUVWXY"[(int)intn % 31] == value[10] ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        /// <summary>
        /// Arvonlisaveronumero (ALV)
        /// </summary>
        /// <param name="alv"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string alv)
        {
            alv = alv.RemoveSpecialCharacthers().ToUpper().Replace("FI", string.Empty);
            int[] multipliers = { 7, 9, 10, 5, 8, 4, 2 };

            if (!Regex.IsMatch(alv, @"^\d{8}$"))
            {
                return ValidationResult.InvalidFormat("12345678");
            }

            var sum = alv.Sum(multipliers);

            var checkDigit = 11 - sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == alv[7].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
