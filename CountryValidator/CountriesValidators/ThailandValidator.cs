using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class ThailandValidator : IdValidationAbstract
    {
        public ThailandValidator()
        {
            CountryCode = nameof(Country.TH);
        }

        /// <summary>
        /// Validate Thailand citizen number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string value)
        {
            value = value.RemoveSpecialCharacthers();
            if (value.Length != 13)
            {
                return ValidationResult.InvalidLength();
            }

            var sum = 0;
            for (var i = 0; i < 12; i++)
            {
                sum += (int)Char.GetNumericValue(value[i]) * (13 - i);
            }

            return (11 - sum % 11) % 10 == (int)char.GetNumericValue(value[12]) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotImplementedException();
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
