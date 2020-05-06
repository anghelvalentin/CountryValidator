using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class UnitedArabEmiratesValidator : IdValidationAbstract
    {
        public UnitedArabEmiratesValidator()
        {
            CountryCode = nameof(Country.AE);
        }

        /*
         * 
         *     "784-1980-1234567-9",
    "123-1234-0123456-7",
    "784198012345679"
         */

        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^784[0-9]{4}[0-9]{7}[0-9]{1}$"))
            {
                return ValidationResult.InvalidFormat("xxx-xxxx-xxxxxxx-x");
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

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotImplementedException();
        }
    }
}
