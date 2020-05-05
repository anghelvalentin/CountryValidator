using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class SlovakiaValidator : IdValidationAbstract
    {
        public SlovakiaValidator()
        {
            CountryCode = nameof(Country.SK);
        }
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (ssn.Length < 9 || ssn.Length > 10)
            {
                return ValidationResult.Invalid("Invalid length");
            }

            int year, month, day;

            try
            {
                year = int.Parse(ssn.Substring(0, 2));
                month = int.Parse(ssn.Substring(2, 2));
                day = int.Parse(ssn.Substring(4, 2));
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }

            if (ssn.Length == 9 && (year >= 54))
            {
                return ValidationResult.InvalidDate();
            }
            else if (ssn.Length == 10)
            {
                long mod = long.Parse(ssn.Substring(0, 9)) % 11;
                int lastNumber = int.Parse(ssn.Substring(9));
                if (mod == 10)
                {
                    if (lastNumber != 0)
                    {
                        return ValidationResult.InvalidChecksum();
                    }
                }
                else if (lastNumber != mod)
                {
                    return ValidationResult.InvalidChecksum();
                }
            }

            bool isValid = IsDayAndMonthValid(ssn, year, month, day);
            if (isValid)
            {
                return ValidationResult.Success();
            }
            else
            {
                return ValidationResult.InvalidDate();
            }
        }


        private bool IsDayAndMonthValid(string value, int year, int month, int day)
        {
            year = GetYearWithHunders(year, value);
            month = GetRealMonth(year, month);

            if (month == 0 || month > 12)
            {
                return false;
            }

            if (day == 0)
            {
                return false;
            }

            int daysInMonth = DateTime.DaysInMonth(year, month - 1);

            if (daysInMonth < day)
            {
                return false;
            }

            return true;
        }

        private int GetRealMonth(int year, int month)
        {
            if ((month > 70) && (year > 2003))
            {
                month -= 70;
            }
            else if (month > 50)
            {
                month -= 50;
            }
            else if ((month > 20) && (year > 2003))
            {
                month -= 20;
            }
            return month;
        }

        private int GetYearWithHunders(int year, string value)
        {
            if (value.Length == 9)
            {
                return year + 1900;
            }

            if (year > DateTime.Now.Year % 100)
            {
                return year + 1900;
            }
            else
            {
                return year + 2000;
            }

        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }


        /// <summary>
        /// Identifikačné číslo pre daň z pridanej hodnoty (IČ DPH)
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("SK", string.Empty).Replace("sk", string.Empty);
            if (!Regex.IsMatch(vat, @"^[1-9]\d[2346-9]\d{7}$"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }

            var nr = ulong.Parse(vat);
            bool isValid = nr % 11 == 0;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNN NN");
            }
            return ValidationResult.Success();
        }
    }
}
