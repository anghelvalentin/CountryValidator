using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class PeruValidator : IdValidationAbstract
    {

        public PeruValidator()
        {
            CountryCode = nameof(Country.PE);
        }


        private string CalculateChecksumNationalIdentity(string number)
        {
            int[] weights = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
            int sum = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (int)char.GetNumericValue(number[i]);
            }
            sum %= 11;
            return string.Format("{0}{1}", "65432110987"[sum], "KJIHGFEDCBA"[sum]);
        }

        /// <summary>
        /// Cédula Única de Identidad (CUI)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!(number.Length == 8 || number.Length == 9))
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.Substring(0, 8).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678");
            }
            else if (number.Length > 8 && !CalculateChecksumNationalIdentity(number).Contains(number[number.Length - 1]))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// RUC Peruvian company tax number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateIndividualTaxCode(id);
        }

        private int CalculateChecksum(string number)
        {
            int[] weights = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (int)char.GetNumericValue(number[i]);
            }
            sum %= 11;
            return (11 - sum).Mod(10);
        }

        /// <summary>
        /// RUC 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number.RemoveSpecialCharacthers();
            string[] validNumbers = new string[] { "10", "15", "17", "20" };
            if (number.Length != 11)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            else if (!validNumbers.Contains(number.Substring(0, 2)))
            {
                return ValidationResult.Invalid("Invalid");
            }
            else if (!number.EndsWith(CalculateChecksum(number).ToString()))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
