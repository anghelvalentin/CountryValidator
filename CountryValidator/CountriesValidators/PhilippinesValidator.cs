using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class PhilippinesValidator : IdValidationAbstract
    {

        public PhilippinesValidator()
        {
            CountryCode = nameof(Country.PH);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers().ToUpper();
            if (!Regex.IsMatch(id, @"^\d{12}[VN]?$"))
            {
                return ValidationResult.InvalidFormat("123456789012");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers().ToUpper();
            if (!Regex.IsMatch(id, @"^\d{12}[VN]?$"))
            {
                return ValidationResult.InvalidFormat("1234-5678901-2");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers().ToUpper();
            if (!Regex.IsMatch(vatId, @"^\d{12}V$"))
            {
                return ValidationResult.InvalidFormat("123456789012V");
            }

            return ValidationResult.Success();
        }
    }
}
