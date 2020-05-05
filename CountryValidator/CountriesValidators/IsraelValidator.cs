using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class IsraelValidator : IdValidationAbstract
    {
        public IsraelValidator()
        {
            CountryCode = nameof(Country.IL);
        }

        public override ValidationResult ValidateEntity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (ssn?.Length > 9)
            {
                return ValidationResult.Invalid("Invalid length. The code must have 9 digits");
            }
            else if (ssn?.Length < 9)
            {
                ssn = ssn.PadLeft(9, '0');
            }

            if (!Regex.IsMatch(ssn, @"^0*5\d+$"))
            {
                return ValidationResult.Invalid("For companies the first digit must be 5");
            }
            else if (!ssn.CheckLuhnDigit())
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (ssn?.Length != 9)
            {
                return ValidationResult.Invalid("Invalid length. The code must have 9 digits");
            }

            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                int incNum = (int)char.GetNumericValue(ssn[i]);
                incNum *= (i % 2) + 1;
                if (incNum > 9)
                {
                    incNum -= 9;
                }

                counter += incNum;
            }
            bool isValid = counter % 10 == 0;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Company Number
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{7}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
