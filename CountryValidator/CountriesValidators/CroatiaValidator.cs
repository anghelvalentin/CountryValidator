using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class CroatiaValidator : IdValidationAbstract
    {
        public CroatiaValidator()
        {
            CountryCode = nameof(Country.HR);
        }


        /// <summary>
        /// OIB (Osobni identifikacijski broj, Croatian identification number)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);

        }

        /// <summary>
        /// OIB (Osobni identifikacijski broj, Croatian identification number)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            return ValidateVAT(id);
        }


        /// <summary>
        /// OIB (Osobni identifikacijski broj, Croatian identification number)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            if (vatId is null)
            {
                throw new ArgumentNullException(nameof(vatId));
            }

            vatId = vatId.RemoveSpecialCharacthers().ToUpper().Replace("HR", string.Empty);
            if (!Regex.IsMatch(vatId, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }

            int product = 10;

            for (int index = 0; index < 10; index++)
            {
                int sum = (vatId[index].ToInt() + product) % 10;

                if (sum == 0)
                {
                    sum = 10;
                }

                product = 2 * sum % 11;
            }

            int checkDigit = (product + vatId[10].ToInt()) % 10;

            bool isValid = checkDigit == 1;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
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
