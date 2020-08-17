using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class BahrainValidator : IdValidationAbstract
    {
        public BahrainValidator()
        {
            CountryCode = nameof(Country.BH);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{9}$"))
            {
                return ValidationResult.Invalid("123456789");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{3,4}$"))
            {
                return ValidationResult.InvalidFormat("NNN or NNNN");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
        }
    }
}
