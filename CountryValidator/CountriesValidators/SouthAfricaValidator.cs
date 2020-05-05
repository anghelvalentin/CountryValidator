using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CountryValidation.Countries;

namespace CountryValidation.Countries
{
    public class SouthAfricaValidator : IdValidationAbstract
    {
        public SouthAfricaValidator()
        {
            CountryCode = nameof(Country.ZA);
        }

        public override ValidationResult ValidateNationalIdentity(string number)
        {
            number = number.RemoveSpecialCharacthers();

            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }
            else if (number.Length != 13)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!(number[10] == '0' || number[10] == '1'))
            {
                return ValidationResult.Invalid("The eleven digit must be 1 or 0");
            }
            else if (!HasValidDate(number))
            {
                return ValidationResult.InvalidDate();
            }

            return number.CheckLuhnDigit() ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        private bool HasValidDate(string number)
        {
            try
            {
                int year = int.Parse(number.Substring(0, 2));
                int month = int.Parse(number.Substring(2, 2));
                int day = int.Parse(number.Substring(4, 2));
                DateTime date = new DateTime(year, month, day);
                return false;
            }
            catch
            {
                return false;
            }

        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            return ValidateVAT(ssn);
        }

        public override ValidationResult ValidateVAT(string number)
        {
            number = number.RemoveSpecialCharacthers();
            number = number?.Replace("ZA", string.Empty).Replace("za", string.Empty);

            if (!Regex.IsMatch(number, @"^[01239]\d{9}$"))
            {
                return ValidationResult.InvalidFormat("012391239");
            }

            bool isValid = number.CheckLuhnDigit();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{4}$"))
            {
                return ValidationResult.InvalidFormat("NNNN");
            }
            return ValidationResult.Success();
        }
    }
}
