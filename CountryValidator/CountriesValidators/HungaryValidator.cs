using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidator.Countries
{
    public class HungaryValidator : IdValidationAbstract
    {
        public HungaryValidator()
        {
            CountryCode = nameof(Country.HU);
        }

        /// <summary>
        /// Szemelyi Szam Ellenorzese
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, "^[1-8][0-9]{2}(0[1-9]|1[12])(0[1-9]|[12][0-9]|3[01])[0-9]{3}[0-9]$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            var yearPrefix = "19";
            if (ssn[0] == '3' || ssn[0] == '4')
            {
                yearPrefix = "20";
            }

            try
            {
                var year = int.Parse(yearPrefix + ssn.Substring(1, 2));
                var month = int.Parse(ssn.Substring(3, 2));
                var day = int.Parse(ssn.Substring(5, 2));
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
            return (int)char.GetNumericValue(ssn[ssn.Length - 1]) == CheckSum(ssn) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Cegjegyzekszam Ellenorzese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^(?:[01][0-9]|20)(?:[01][0-9]|2[0-3])[0-9]{6}$"))
            {
                return ValidationResult.Invalid("Invalid code");
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// Adoazonosito jel Ellenorzese
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string code)
        {
            code = code.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(code, @"^8[2-5][0-9]{4}[0-9]{3}[0-9]$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }

            return (int)char.GetNumericValue(code[code.Length - 1]) == CheckSum(code) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        private int CheckSum(string value)
        {
            int sum = 0;
            for (int i = 0; i < value.Length - 1; i++)
            {
                sum += (int)value[i] * (i + 1);
            }

            return (sum % 11);
        }


        /// <summary>
        ///  Kozossegi Adoszam (ANUM)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("hu", string.Empty).Replace("HU", string.Empty);
            if (!Regex.IsMatch(vatId, @"^\d{8}$"))
            {
                return ValidationResult.InvalidFormat("12345678");
            }
            int[] multipliers = { 9, 7, 3, 1, 9, 7, 3 };
            var sum = vatId.Sum(multipliers);

            var checkDigit = 10 - sum % 10;

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == vatId[7].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
