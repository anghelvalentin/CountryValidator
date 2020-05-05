using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class CanadaValidator : IdValidationAbstract
    {
        public CanadaValidator()
        {
            CountryCode = nameof(Country.CA);
        }

        /// <summary>
        /// Validate Business Number
        /// </summary>
        /// <param name="bn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string bn)
        {
            bn = bn.RemoveSpecialCharacthers();

            var chardDigits = bn.ToCharArray();
            if (chardDigits.Length != 9)
            {
                return ValidationResult.Invalid("Invalid length! The code must have a length of 9");
            }
            else if (chardDigits[0] != '8')
            {
                return ValidationResult.Invalid("Invalid format! The first characther must be 8");
            }

            int[] digits = new int[chardDigits.Length];
            for (int i = 0; i < chardDigits.Length; i++)
            {
                if (!int.TryParse(chardDigits[i].ToString(), out digits[i]))
                {
                    return ValidationResult.Invalid("Invalid format! Only digits are allowed");
                }
            }

            var total = digits.Where((value, index) => index % 2 == 0 && index != 8).Sum()
                        + digits.Where((value, index) => index % 2 != 0).Select(v => v * 2)
                              .SelectMany(v => v.ToDigitEnumerable()).Sum();

            var checkDigit = (10 - (total % 10)) % 10;

            bool isValid = digits.Last() == checkDigit;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Validate SIN
        /// </summary>
        /// <param name="sin"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string sin)
        {
            sin = sin.RemoveSpecialCharacthers();

            var chardDigits = sin.ToCharArray();
            if (Regex.IsMatch(sin, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("123-456-789");
            }

            int[] digits = new int[chardDigits.Length];
            for (int i = 0; i < chardDigits.Length; i++)
            {
                if (!int.TryParse(chardDigits[i].ToString(), out digits[i]))
                {
                    return ValidationResult.Invalid("Invalid format! Only digits are allowed");
                }
            }

            var total = digits.Where((value, index) => index % 2 == 0 && index != 8).Sum()
                        + digits.Where((value, index) => index % 2 != 0).Select(v => v * 2)
                              .SelectMany(v => v.ToDigitEnumerable()).Sum();

            var checkDigit = (10 - (total % 10)) % 10;

            bool isValid = digits.Last() == checkDigit;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        /// <summary>
        /// Validate Business Number
        /// </summary>
        /// <param name="bn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string bn)
        {
            return ValidateEntity(bn);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^(?=[^DdFfIiOoQqUu\\d\\s])[A-Za-z]\\d(?=[^DdFfIiOoQqUu\\d\\s])[A-Za-z]\\s{0,1}\\d(?=[^DdFfIiOoQqUu\\d\\s])[A-Za-z]\\d$"))
            {
                return ValidationResult.InvalidFormat("ANA NAN");
            }
            return ValidationResult.Success();
        }
    }
}
