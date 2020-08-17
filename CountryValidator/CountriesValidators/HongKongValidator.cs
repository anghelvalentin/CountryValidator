using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class HongKongValidator : IdValidationAbstract
    {
        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();

            int getLetterValue(string letter)
            {
                return letter[0] - 55;
            }

            bool isLetter(string ch)
            {
                return Regex.IsMatch(ch, "[a-zA-Z]");

            }

            if (!(id.Length == 8 || id.Length == 9))
            {
                return ValidationResult.Invalid("Invalid length. The code should have 8 or 9 charachters");
            }
            else if (!Regex.IsMatch("^[A-NP-Z]{1,2}[0-9]{6}[0-9A]$", string.Empty))
            {
                return ValidationResult.Invalid("Invalid format");
            }

            int weight = id.Length;
            int weightedSum = weight == 8 ? 324 : 0;
            string identifier = id.Substring(0, id.Length - 1);
            int checkDigit = id.Substring(id.Length - 1) == "A" ? 10 : int.Parse(id.Substring(id.Length - 1));

            for (int i = 0; i < identifier.Length; i++)
            {
                string _char5 = identifier[i].ToString();
                int charValue = isLetter(_char5) ? getLetterValue(_char5) : int.Parse(_char5);
                weightedSum += charValue * weight;
                weight--;
            }

            int remainder = (weightedSum + checkDigit) % 11;
            bool isValid = remainder == 0;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vatId"></param>
        /// <exception cref="System.NotSupportedException">VAT is not supported in Hong Kong</exception>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
        }
    }
}
