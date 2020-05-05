
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class ChileValidator : IdValidationAbstract
    {
        public ChileValidator()
        {
            CountryCode = nameof(Country.CL);
        }


        /// <summary>
        /// Validate  National Tax Number (RUN/RUT)  
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            return ValidateVAT(number);
        }

        /// <summary>
        /// Validate  National Tax Number (RUN/RUT)  
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            return ValidateVAT(number);
        }



        /// <summary>
        /// Validate  National Tax Number (RUN/RUT)  
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string number)
        {
            number = number.RemoveSpecialCharacthers();
            number = number.Replace("CL", string.Empty).Replace("cl", string.Empty);

            if (!(number.Length == 8 || number.Length == 9))
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.Substring(0, number.Length - 1).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678 or 123456789");
            }

            if (number[number.Length - 1] != CalculateChecksum(number))
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();

        }

        private int Mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        private char CalculateChecksum(string number)
        {
            int s = 0;

            char[] array = number.Substring(0, number.Length - 1).ToCharArray();
            Array.Reverse(array);
            number = new string(array);


            for (int i = 0; i < number.Length; i++)
            {
                s = s + ((int)char.GetNumericValue(number[i])) * (4 + Mod(5 - i, 6));
            }

            return "0123456789K"[s % 11];
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{7}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNNN or NNN-NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
