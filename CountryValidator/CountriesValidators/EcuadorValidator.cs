using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class EcuadorValidator : IdValidationAbstract
    {
        public EcuadorValidator()
        {
            CountryCode = nameof(Country.EC);
        }


        private int Checksum(string number, int[] weights)
        {
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum = sum + weights[i] * (int)char.GetNumericValue(number[i]);
            }

            return sum % 11;
        }

        /// <summary>
        /// Registro Unico de Contribuyentes (RUC)  
        /// </summary>
        /// <param name="ruc"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string ruc)
        {
            ruc = ruc.RemoveSpecialCharacthers();

            if (ruc.Length != 13)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!ruc.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }
            else if ((int.Parse(ruc.Substring(0, 2)) < 1 || int.Parse(ruc.Substring(0, 2)) > 24) && !new string[] { "30", "50" }.Contains(ruc.Substring(0, 2)))
            {
                return ValidationResult.Invalid("Invalid province code");
            }
            else if (int.Parse(ruc.Substring(2, 1)) < 6) // 0..5 = natural RUC: CI plus establishment number
            {
                if (ruc.Substring(ruc.Length - 3) == "000")
                {
                    return ValidationResult.Invalid("Invalid code");
                }
                return ValidateCI(ruc.Substring(0, 10));
            }
            else if (ruc[2] == '6')   // 6 = public RUC
            {
                if (ruc.Substring(ruc.Length - 4) == "000")
                {
                    return ValidationResult.Invalid("Invalid code");
                }
                else if (Checksum(ruc.Substring(0, 9), new int[] { 3, 2, 7, 6, 5, 4, 3, 2, 1 }) != 0)
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            else if (ruc[2] == '9') // 9 = juridical RUC
            {
                if (ruc.Substring(ruc.Length - 3) == "000")
                {
                    return ValidationResult.Invalid("Establishment Number Wrong");
                }
                if (Checksum(ruc.Substring(0, 10), new int[] { 4, 3, 2, 7, 6, 5, 4, 3, 2, 1 }) != 0)
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            else
            {
                return ValidationResult.Invalid("Third digit is wrong");
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// Registro Unico de Contribuyentes (RUC) 
        /// </summary>
        /// <param name="ruc"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            return ValidateEntity(id);
        }

        /// <summary>
        /// Registro Unico de Contribuyentes (RUC) 
        /// </summary>
        /// <param name="ruc"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string ruc)
        {
            return ValidateEntity(ruc);
        }


        public int Checksum(string number)
        {
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                int temp = (i % 2 == 0 ? 2 : 1) * (int)char.GetNumericValue(number[i]);
                if (temp > 9)
                {
                    temp -= 9;
                }

                sum = sum + temp;
            }
            return sum % 10;
        }

        /// <summary>
        /// CI (Cédula de identidad, Ecuadorian personal identity code)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public ValidationResult ValidateCI(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (number.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }
            else if ((int.Parse(number.Substring(0, 2)) < 1 || int.Parse(number.Substring(0, 2)) > 24) && !new string[] { "30", "50" }.Contains(number.Substring(0, 2)))
            {
                return ValidationResult.Invalid("Invalid province code");
            }
            else if (Char.GetNumericValue(number[2]) > 5)
            {
                return ValidationResult.Invalid("Third digit is wrong");
            }
            else if (Checksum(number) != 0)
            {

                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
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
