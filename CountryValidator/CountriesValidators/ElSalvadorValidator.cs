using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class ElSalvadorValidator : IdValidationAbstract
    {
        public ElSalvadorValidator()
        {
            CountryCode = nameof(Country.SV);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidationResult.InvalidChecksum();
        }


        private int CalculateChecksum(string number)
        {
            number = number.RemoveSpecialCharacthers();
            int inumber = int.Parse(number.Substring(10, 3));
            if (inumber <= 100)
            {
                var weights = new int[] { 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int total = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    total += (int)char.GetNumericValue(number[i]) * weights[i];
                }
                return total % 11 % 10;
            }
            else
            {// New NIT
                var weights = new int[] { 2, 7, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                int total = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    total += (int)char.GetNumericValue(number[i]) * weights[i];
                }

                return (-total).Mod(11).Mod(10);
            }
        }

        /// <summary>
        /// NIT (Número de Identificación Tributaria, El Salvador tax number)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            if (number.Length != 14)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678901234");
            }
            else if (!new char[] { '0', '1', '9' }.Contains(number[0]))
            {
                return ValidationResult.Invalid("Invalid code. First digit must be 0, 1 or 9");
            }
            if ((int)char.GetNumericValue(number[number.Length - 1]) != CalculateChecksum(number))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateVAT(vatId);
        }
        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^1101$"))
            {
                return ValidationResult.InvalidFormat("1101");
            }
            return ValidationResult.Success();
        }
    }
}
