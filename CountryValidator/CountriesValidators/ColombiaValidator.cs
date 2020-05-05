
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class ColombiaValidator : IdValidationAbstract
    {
        public ColombiaValidator()
        {
            CountryCode = nameof(Country.CO);
        }


        /// <summary>
        /// Validat RUT (Registro Unico Tributario)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        /// <summary>
        /// NIT (Número De Identificación Tributaria, Colombian identity code)
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            return ValidateVAT(ssn);
        }

        public override ValidationResult ValidateVAT(string number)
        {
            number = number.RemoveSpecialCharacthers();
            number = number.Replace("CO", string.Empty).Replace("co", string.Empty);
            if (!(8 <= number.Length && number.Length <= 16))
            {
                return ValidationResult.InvalidChecksum();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.Invalid("Only digits are allowed");
            }
            if (CalculateChecksum(number.Substring(0, number.Length - 1)) != number[number.Length - 1])
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }


        private char CalculateChecksum(string number)
        {
            int s = 0;
            int[] weights = new int[] { 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 67, 71 };

            char[] charArray = number.ToCharArray();
            Array.Reverse(charArray);
            number = new string(charArray);

            for (int i = 0; i < number.Length; i++)
            {
                s = s + weights[i] * (int)char.GetNumericValue(number[i]);
            }

            s = s % 11;

            return "01987654321"[s];
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{6}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNN");
            }
            return ValidationResult.Success();
        }

    }
}
