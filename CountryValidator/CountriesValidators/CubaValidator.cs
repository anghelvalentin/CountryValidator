
using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class CubaValidator : IdValidationAbstract
    {
        public CubaValidator()
        {
            CountryCode = nameof(Country.CU);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Validate NI (Número de identidad, Cuban identity card numbers)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            if (number.Length != 11)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            else if (!ValidateDate(number))
            {
                return ValidationResult.InvalidDate();
            }
            return ValidationResult.Success();
        }


        private bool ValidateDate(string number)
        {
            try
            {
                int year = int.Parse(number.Substring(0, 2));
                int month = int.Parse(number.Substring(2, 2));
                int day = int.Parse(number.Substring(4, 2));
                if (!int.TryParse(number[6].ToString(), out int m))
                {
                    return false;
                }
                else if (m == 9)
                {
                    year += 1800;
                }
                else if (0 <= m && m <= 5)
                {
                    year += 1900;
                }
                else
                {
                    year += 2000;
                }

                DateTime datetime = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Not Supported
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
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
