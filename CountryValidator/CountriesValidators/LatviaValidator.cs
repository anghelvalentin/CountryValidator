using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class LatviaValidator : IdValidationAbstract
    {
        public LatviaValidator()
        {
            CountryCode = nameof(Country.LV);
        }

        private static bool IsValidDate(string dateString)
        {
            if (!Regex.IsMatch(dateString, @"^\d{1,2}\.\d{1,2}\.\d{4}$"))
            {
                return false;
            }

            var parts = dateString.Split('.');
            var day = Int32.Parse(parts[0]);
            var month = int.Parse(parts[1].ToString());
            var year = int.Parse(parts[2]);

            if (year < 1000 || year > 3000 || month == 0 || month > 12)
            { return false; }
            var monthLength = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                monthLength[1] = 29;

            return day > 0 && day <= monthLength[month - 1];
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string identificationCode)
        {
            identificationCode = identificationCode.RemoveSpecialCharacthers();
            var match = Regex.Match(identificationCode, "([0-2]\\d|[3][0-1])([0]\\d|[1][0-2])(\\d{2})([0-2])(\\d{3})(\\d)");
            if (!match.Success)
            {
                return ValidationResult.Invalid("Invalid format");
            }

            var centuryNumber = int.Parse(match.Groups[4].Value);
            string century = centuryNumber == 1 ? "19" : centuryNumber == 2 ? "20" : "18";

            var inputDay = int.Parse(match.Groups[1].Value);
            var inputMonth = match.Groups[2].Value;
            var inputYear = century + match.Groups[3].Value;
            if (!IsValidDate(inputDay + "." + inputMonth + "." + inputYear))
            {
                return ValidationResult.InvalidDate();
            }

            var identificationCodeWithoutDash = match.Value.Replace("-", "");
            var checkSumDigits = new int[] { 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            var checkSum = 0;

            for (int i = 0; i < identificationCodeWithoutDash.Length && i < checkSumDigits.Length; i++)
            {
                checkSum += int.Parse(identificationCodeWithoutDash[i].ToString()) * checkSumDigits[i];

            }
            checkSum = (1 - checkSum) % 11;
            checkSum += (checkSum < -1) ? 11 : 0;


            return checkSum == int.Parse(match.Groups[6].Value) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// PVN (Pievienotās vērtības nodokļa, Latvian VAT number)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("LV", string.Empty).Replace("lv", string.Empty);

            if (!Regex.IsMatch(vatId, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            int[] multipliers = { 9, 1, 4, 8, 3, 10, 2, 5, 7, 6 };

            if (Regex.IsMatch(vatId, "^[0-3]"))
            {
                bool secondRegex = Regex.IsMatch(vatId, "^[0-3][0-9][0-1][0-9]");
                return secondRegex ? ValidationResult.Success() : ValidationResult.Invalid("Invalid");
            }
            var sum = vatId.Sum(multipliers);

            var checkDigit = sum % 11;

            if (checkDigit == 4 && vatId[0] == '9')
            {
                checkDigit -= 45;
            }

            if (checkDigit == 4)
            {
                checkDigit = 4 - checkDigit;
            }
            else
            {
                if (checkDigit > 4)
                {
                    checkDigit = 14 - checkDigit;
                }
                else
                {
                    if (checkDigit < 4)
                    {
                        checkDigit = 3 - checkDigit;
                    }
                }
            }

            var isValid = checkDigit == vatId[10].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
