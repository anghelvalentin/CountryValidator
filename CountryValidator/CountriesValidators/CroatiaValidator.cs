using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers()?.ToUpper()?.Replace("HR", string.Empty);
            if (!Regex.IsMatch(vat, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }

            int product = 10;

            for (int index = 0; index < 10; index++)
            {
                int sum = (vat[index].ToInt() + product) % 10;

                if (sum == 0)
                {
                    sum = 10;
                }

                product = 2 * sum % 11;
            }

            int checkDigit = (product + vat[10].ToInt()) % 10;

            bool isValid = checkDigit == 1;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
