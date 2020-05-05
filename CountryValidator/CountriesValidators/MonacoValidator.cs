using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MonacoValidator : IdValidationAbstract
    {
        public MonacoValidator()
        {
            CountryCode = nameof(Country.MC);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidateVAT(string number)
        {
            number = number.RemoveSpecialCharacthers();
            number = number.Replace("FR", string.Empty).Replace("fr", string.Empty).Replace("mc", string.Empty).Replace("MC", string.Empty);


            if (number.Substring(2, 3) != "000")
            {
                return ValidationResult.Invalid("Invalid Code");
            }

            return new FranceValidator().ValidateVAT(number);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^980\\d{2}$"))
            {
                return ValidationResult.InvalidFormat("980NN");
            }
            return ValidationResult.Success();
        }
    }
}
