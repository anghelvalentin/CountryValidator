using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class UzbekistanValidator : IdValidationAbstract
    {
        public UzbekistanValidator()
        {
            CountryCode = nameof(Country.UZ);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{14}$"))
            {
                return ValidationResult.InvalidFormat("12345678901234");
            }
            return ValidationResult.Success();
        }
        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{6}$"))
            {
                return ValidationResult.InvalidFormat("NNN NNN");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
        }
    }
}
