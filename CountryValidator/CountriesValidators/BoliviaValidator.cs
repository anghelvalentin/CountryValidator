using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class BoliviaValidator : IdValidationAbstract
    {
        public BoliviaValidator()
        {
            CountryCode = nameof(Country.BO);
        }

        /// <summary>
        /// CI Number
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            if (!Regex.IsMatch(ssn, @"^\d{5,7}\w?$"))
            {
                return ValidationResult.InvalidFormat("1234567");
            }
            return ValidationResult.Success();

        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
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
            if (!Regex.IsMatch(postalCode, "^\\d{4}$"))
            {
                return ValidationResult.InvalidFormat("NNNN");
            }
            return ValidationResult.Success();
        }
    }
}
