
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class CostaRicaValidator : IdValidationAbstract
    {
        public CostaRicaValidator()
        {
            CountryCode = nameof(Country.CR);
        }

        /// <summary>
        /// Costa Rica CPJ (Cédula de Persona Jurídica, Costa Rica tax number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            number = number.Replace("cr", string.Empty).Replace("CR", string.Empty);
            var class_three_types = new string[]{"002", "003", "004", "005", "006", "007", "008",
                     "009", "010", "011", "012", "013", "014", "101",
                     "102", "103", "104", "105", "106", "107", "108",
                     "109", "110" };

            if (number.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("2234567890");
            }
            else if (!Regex.IsMatch(number, "^[2345]"))
            {
                return ValidationResult.Invalid("Invalid format. First digit must be 2,3,4 or 5");
            }
            else if (number[0] == '2' && !Regex.IsMatch(number, "^2(100|200|300|400)"))
            {
                return ValidationResult.Invalid("Invalid code");
            }
            else if (number[0] == '3' && !class_three_types.Contains(number.Substring(1, 3)))
            {
                return ValidationResult.Invalid("Invalid code");
            }
            else if (number[0] == '4' && number.Substring(1, 3) != "000")
            {
                return ValidationResult.Invalid("Invalid code");
            }
            else if (number[0] == '5' && number.Substring(1, 3) != "001")
            {
                return ValidationResult.Invalid("Invalid code");
            }
            return ValidationResult.Success();
        }


        /// <summary>
        /// CPF (Cédula de Persona Física, Costa Rica physical person ID number) or CR (Cédula de Residencia, Costa Rica foreigners ID number)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            if (ValidateCPF(number).IsValid)
            {
                return ValidationResult.Success();

            }
            else if (ValidateResident(number).IsValid)
            {
                return ValidationResult.Success();
            }
            return ValidationResult.Invalid("Invalid code");
        }

        public ValidationResult ValidateCPF(string number)
        {
            number = number.RemoveSpecialCharacthers();

            if (!(number.Length == 10 || number.Length == 9))
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12-345-678");
            }


            return ValidationResult.Success();
        }

        /// <summary>
        /// Costa Rica CPJ (Cédula de Persona Jurídica, Costa Rica tax number
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
        }

        /// <summary>
        /// CR (Cédula de Residencia, Costa Rica foreigners ID number)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public ValidationResult ValidateResident(string number)
        {
            number = number.RemoveSpecialCharacthers();

            if (!(number.Length == 11 || number.Length == 12))
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat(number);
            }
            else if (number[0] != '1')
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{4,5}$"))
            {
                return ValidationResult.InvalidFormat("NNNN OR NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
