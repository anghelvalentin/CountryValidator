using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidator.Countries
{
    public class MaltaValidator : IdValidationAbstract
    {

        public MaltaValidator()
        {
            CountryCode = nameof(Country.MT);
        }


        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers().ToUpper();
            if (id.Length > 8 || id.Length < 4)
            {
                return ValidationResult.InvalidLength();
            }
            id = id.PadLeft(8, '0');
            if (!Regex.IsMatch(id, @"^\d{8}$"))
            {
                return ValidationResult.InvalidFormat("12345678");
            }
            return ValidationResult.Success();
        }


        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers().ToUpper();
            if (id.Length > 8 || id.Length < 4)
            {
                return ValidationResult.InvalidLength();
            }
            id = id.PadLeft(8, '0');
            if (!Regex.IsMatch(id, @"^\d{7}[1-9MGAPLHBZ]$"))
            {
                return ValidationResult.InvalidFormat("1234567M");
            }
            return ValidationResult.Success();
        }


        /// <summary>
        /// VAT Number (VAT)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId?.Replace("mt", string.Empty)?.Replace("MT", string.Empty);
            if (!Regex.IsMatch(vatId, @"^[1-9]\d{7}$"))
            {
                return ValidationResult.InvalidFormat("12345678");
            }

            int[] multipliers = { 3, 4, 6, 7, 8, 9 };
            var sum = vatId.Sum(multipliers);

            var checkDigit = 37 - sum % 37;

            return checkDigit == int.Parse(vatId.Substring(6, 2)) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
