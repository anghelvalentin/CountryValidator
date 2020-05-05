using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class CzechValidator : IdValidationAbstract
    {
        public CzechValidator()
        {
            CountryCode = nameof(Country.CZ);
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();

            if (id?.Length < 9 || id?.Length > 10)
            {
                return ValidationResult.Invalid("Invalid length. The code must have 9 or 10 digits");
            }

            int year, month, day;

            try
            {
                year = int.Parse(id.Substring(0, 2));
                month = int.Parse(id.Substring(2, 2));
                day = int.Parse(id.Substring(4, 2));
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }

            if (id.Length == 9 && (year >= 54))
            {
                return ValidationResult.Invalid("Invalid code. Age is wrong");
            }
            else if (id.Length == 10)
            {
                long mod = long.Parse(id.Substring(0, 9)) % 11;
                int lastNumber = int.Parse(id.Substring(9));
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

            return IsDayAndMonthValid(id, year, month, day);
        }


        private ValidationResult IsDayAndMonthValid(string value, int year, int month, int day)
        {
            year = GetYearWithHunders(year, value);
            month = GetRealMonth(year, month);

            if (month == 0 || month > 12)
            {
                return ValidationResult.InvalidDate();
            }

            if (day == 0)
            {
                return ValidationResult.InvalidDate();
            }

            int daysInMonth = DateTime.DaysInMonth(year, month - 1);

            if (daysInMonth < day)
            {
                return ValidationResult.InvalidDate();
            }

            return ValidationResult.Success();
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
        /// Danove Identifikacni Cislo (DIC/VAT)
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("cz", string.Empty).Replace("CZ", string.Empty);

            int total = 0;
            var multipliers = new int[] { 8, 7, 6, 5, 4, 3, 2 };


            if (Regex.IsMatch(vat, @"^\d{8}$"))
            {

                for (int i = 0; i < 7; i++)
                {
                    total += int.Parse(vat[i].ToString()) * multipliers[i];
                }

                total = 11 - total % 11;
                if (total == 10) total = 0;
                if (total == 11) total = 1;

                if (total == int.Parse(vat[7].ToString()))
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            else if (Regex.IsMatch(vat, @"^[0-5][0-9][0|1|5|6][0-9][0-3][0-9]\d{3}$"))
            {
                var temp = int.Parse(vat.Substring(0, 2));
                if (temp > 62)
                {
                    return ValidationResult.InvalidChecksum();
                }

                return ValidationResult.Success();
            }
            else if (Regex.IsMatch(vat, @"^6\d{8}$"))
            {

                for (int i = 0; i < 7; i++)
                {
                    total += int.Parse(vat[i + 1].ToString()) * multipliers[i];
                }
                int a;
                if (total % 11 == 0)
                {
                    a = total + 11;
                }
                else
                {
                    a = (int)Math.Ceiling((decimal)total / 11) * 11;
                }

                var pointer = a - total;

                var lookup = new int[] { 8, 7, 6, 5, 4, 3, 2, 1, 0, 9, 8 };
                if (lookup[pointer - 1] == int.Parse(vat.Substring(8, 1).ToString()))
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            else if (Regex.IsMatch(vat, @"^\d{2}[0-3|5-8][0-9][0-3][0-9]\d{4}$"))
            {
                var temp = int.Parse(vat.Substring(0, 2)) + int.Parse(vat.Substring(2, 2))
                    + int.Parse(vat.Substring(4, 2)) + int.Parse(vat.Substring(6, 2))
                    + int.Parse(vat.Substring(8));
                if (temp % 11 == 0 && long.Parse(vat) % 11 == 0)
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            return ValidationResult.Invalid("Invalid format");
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.Trim();
            if (!(Regex.IsMatch(postalCode, @"^\d{3}\s?\d{2}$") || Regex.IsMatch(postalCode, "^\\d{4}$")))
            {
                return ValidationResult.InvalidFormat("NNN NN");
            }
            return ValidationResult.Success();
        }
    }
}
