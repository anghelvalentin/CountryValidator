using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class ParaguayValidator : IdValidationAbstract
    {
        public ParaguayValidator()
        {
            CountryCode = nameof(Country.PY);
        }


        /// <summary>
        /// Registro Unico de Contribuyentes (RUC)  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        /// <summary>
        /// Registro Unico de Contribuyentes (RUC)  
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            return ValidateVAT(ssn);
        }


        /// <summary>
        /// Registro Unico de Contribuyentes (RUC)  
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (number.Length > 9 || number.Length < 6)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("123456789");
            }
            else if ((int)char.GetNumericValue(number[number.Length - 1]) != CalculateChecksum(number.Substring(0, number.Length - 1)))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }



        private int CalculateChecksum(string number)
        {
            int s = 0;

            char[] charArray = number.ToCharArray();
            Array.Reverse(charArray);
            number = new string(charArray);

            for (int i = 0; i < number.Length; i++)
            {
                s = s + ((i + 2) * (int)Char.GetNumericValue(number[i]));
            }

            int rez = -s.Mod(11);
            return rez.Mod(10);
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
