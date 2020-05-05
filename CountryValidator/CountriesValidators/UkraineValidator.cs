using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class UkraineValidator : IdValidationAbstract
    {
        public UkraineValidator()
        {
            CountryCode = nameof(Country.UA);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{12}$"))
            {
                return ValidationResult.Invalid("123456789012");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(vatId, @"^\d{12}$"))
            {
                return ValidationResult.InvalidFormat("123456789012");
            }

            return ValidationResult.Success();
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
