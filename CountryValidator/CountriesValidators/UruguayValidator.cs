using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class UruguayValidator : IdValidationAbstract
    {
        public UruguayValidator()
        {
            CountryCode = nameof(Country.UY);
        }

        /// <summary>
        /// Validate RUT numbers
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string rut)
        {
            return ValidateVAT(rut);
        }

        /// <summary>
        /// Validate RUT numbers
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string rut)
        {
            return ValidateVAT(rut);
        }


        /// <summary>
        /// Validate RUT numbers
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string rut)
        {
            rut = rut.RemoveSpecialCharacthers();
            rut = rut.Replace("UY", string.Empty).Replace("uy", string.Empty);

            if (rut.Length != 12)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!rut.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("0123456789012");
            }
            else if (int.Parse(rut.Substring(0, 2)) < 1 || int.Parse(rut.Substring(0, 2)) > 21)
            {
                return ValidationResult.Invalid("Invalid code");
            }
            else if (rut.Substring(2, 6) == "000000")
            {
                return ValidationResult.Invalid("Invalid code");
            }
            else if (rut.Substring(8, 3) != "001")
            {
                return ValidationResult.Invalid("Invalid code");
            }
            else if ((int)char.GetNumericValue(rut[rut.Length - 1]) != CalculateChecksum(rut.Substring(0, rut.Length - 1)))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        private int CalculateChecksum(string number)
        {
            int[] weights = new[] { 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int total = 0;

            for (int i = 0; i < number.Length; i++)
            {
                total = total + weights[i] * (int)char.GetNumericValue(number[i]);
            }

            return (-total).Mod(11);

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
