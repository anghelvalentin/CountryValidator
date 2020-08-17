using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class BelgiumValidator : IdValidationAbstract
    {
        public BelgiumValidator()
        {
            CountryCode = nameof(Country.BE);
        }

        /// <summary>
        /// BTW, TVA, NWSt, ondernemingsnummer (Belgian enterprise number).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        private int ModFunction(long nr)
        {
            return (int)(97 - (nr % 97));
        }

        /// <summary>
        /// Rijksregisternummer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            var checkDigit = id.Substring(id.Length - 2);

            var nrToCheck = long.Parse(id.Substring(0, 9));

            if (ModFunction(nrToCheck).ToString() == checkDigit)
            {
                return ValidationResult.Success();
            }

            nrToCheck = long.Parse('2' + id.Substring(0, 9));

            bool isValid = ModFunction(nrToCheck).ToString() == checkDigit;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// BTW, TVA, NWSt, ondernemingsnummer (Belgian enterprise number).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string id)
        {
            id = id.RemoveSpecialCharacthers();
            id = id.Replace("be", string.Empty).Replace("BE", string.Empty);

            if (id.Length == 9)
            {
                id = id.PadLeft(10, '0');
            }

            if (!Regex.IsMatch(id, @"^0?\d{9}$"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }


            var isValid = 97 - int.Parse(id.Substring(0, 8)) % 97 == int.Parse(id.Substring(8, 2));
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
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
