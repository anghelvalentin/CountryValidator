using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class BelarusValidator : IdValidationAbstract
    {
        public BelarusValidator()
        {
            CountryCode = nameof(Country.BY);
        }

        /// <summary>
        /// Payer's account number (UNP)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id?.Replace("УНП", string.Empty).Replace("UNP", string.Empty);
            id = id.Translit();
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, "^[AaBbCcEeHhKkMmOoPpTt]{2}"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidateUNP(id);
        }

        /// <summary>
        /// Payer's account number (UNP)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number?.Replace("УНП", string.Empty).Replace("UNP", string.Empty);
            number = number.Translit();
            number = number.RemoveSpecialCharacthers();
            if (!number.Substring(0, 2).All(char.IsDigit))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidateUNP(number);

        }

        /// <summary>
        ///  Payer's account number (UNP)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string number)
        {
            number = number?.Replace("УНП", string.Empty).Replace("UNP", string.Empty);
            number = number.Translit();
            number = number.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(number, "^[AaBbCcEeHhKkMmOoPpTt]{2}"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidateUNP(number);
        }

        private ValidationResult ValidateUNP(string number)
        {
            if (number.Length != 9)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.Substring(2).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("AA1234567");
            }
            else if (!Regex.IsMatch(number, "^[1234567AaBbCcEeHhKkMm]"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            else if (CalculatChecksum(number) != number.Substring(number.Length - 1))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        private string CalculatChecksum(string number)
        {
            number = number.ToUpper();
            string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int[] weights = new int[] { 29, 23, 19, 17, 13, 7, 5, 3 };
            if (!number.All(char.IsDigit))
            {
                number = string.Format("{0}{1}{2}", number[0], "ABCEHKMOPT".IndexOf(number[1]), number.Substring(2));
            }
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * alphabet.IndexOf(number[i]);
            }
            sum %= 11;
            if (sum > 9)
            {
                return string.Empty;
            }
            return sum.ToString();

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
