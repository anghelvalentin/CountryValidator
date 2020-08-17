using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class GeorgiaValidator : IdValidationAbstract
    {
        public GeorgiaValidator()
        {
            CountryCode = nameof(Country.GE);
        }

        public override ValidationResult ValidateEntity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^\d{9}$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidationResult.Success();

        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^(\d{9}|\d{11})$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
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
