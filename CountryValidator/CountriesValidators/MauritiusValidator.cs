using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MauritiusValidator : IdValidationAbstract
    {
        public MauritiusValidator()
        {
            CountryCode = nameof(Country.MU);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ID number (Mauritian national identifier)
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (number.Length != 14)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!Regex.IsMatch(number, "^[A-Z][0-9]+[0-9A-Z]$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            else if (CalculateChecksum(number.Substring(0, 13)) != number[number.Length - 1])
            {
                return ValidationResult.InvalidChecksum();
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
                int day = int.Parse(number.Substring(1, 2));
                int month = int.Parse(number.Substring(3, 2));
                int year = int.Parse(number.Substring(5, 2));

                if (day > 31)
                {
                    return false;
                }
                else if (month > 12)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public char CalculateChecksum(string number)
        {
            string _alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum += (14 - i) * (int)char.GetNumericValue(number[i]);
            }

            sum = (17 - sum).Mod(17);

            return _alphabet[sum];
        }
        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            throw new NotSupportedException();
        }
    }
}
