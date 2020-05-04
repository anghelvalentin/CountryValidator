using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidator.Countries
{
    public class SloveniaValidator : IdValidationAbstract
    {
        public SloveniaValidator()
        {
            CountryCode = nameof(Country.SI);
        }

        /// <summary>
        /// Enotna matična številka občana, EMŠO, Unique Master Citizen Number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string value)
        {
            value = value.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(value, @"^\d{13}$"))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }


            try
            {
                int day = int.Parse(value.Substring(0, 2));
                int month = int.Parse(value.Substring(2, 2));
                int year = int.Parse(value.Substring(4, 3));


                if (year >= 800)
                {
                    year = 1000 + year;
                }
                else
                {
                    year = 2000 + year;
                }
                DateTime date = new DateTime(year, month, day);
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }


            int rr = int.Parse(value.Substring(7, 2));
            int k = int.Parse(value.Substring(12, 1));

            // Validate checksum
            var sum = 0;
            for (var i = 0; i < 6; i++)
            {
                sum += (7 - i) * ((int)char.GetNumericValue(value[i]) + (int)char.GetNumericValue(value[i + 6]));
            }
            sum = 11 - sum % 11;
            if (sum == 10 || sum == 11)
            {
                sum = 0;
            }
            if (sum != k)
            {
                return ValidationResult.InvalidChecksum();
            }

            // Validate political region
            // rr is the political region of birth, which can be in ranges:
            // 10-19: Bosnia and Herzegovina
            // 20-29: Montenegro
            // 30-39: Croatia (not used anymore)
            // 41-49: Macedonia
            // 50-59: Slovenia (only 50 is used)
            // 70-79: Central Serbia
            // 80-89: Serbian province of Vojvodina
            // 90-99: Kosovo

            return 50 <= rr && rr <= 59 ? ValidationResult.Success() : ValidationResult.Invalid("Invalid Region. Slovenia region is between 50-59");
        }


        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            return ValidateVAT(ssn);
        }


        /// <summary>
        /// Identifikacijska številka za DDV
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("si", string.Empty).Replace("SI", string.Empty);

            if (!Regex.IsMatch(vat, @"^[1-9]\d{7}$"))
            {
                return ValidationResult.InvalidFormat("12345678");
            }

            int[] multipliers = { 8, 7, 6, 5, 4, 3, 2 };

            var sum = vat.Sum(multipliers);

            var checkDigit = 11 - sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == vat[7].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
