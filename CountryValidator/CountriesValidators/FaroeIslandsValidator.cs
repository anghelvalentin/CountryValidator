using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class FaroeIslandsValidator : IdValidationAbstract
    {
        public FaroeIslandsValidator()
        {
            CountryCode = nameof(Country.FO);
        }


        /// <summary>
        /// V-number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{6}$"))
            {
                return ValidationResult.InvalidFormat("123 456");
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// P-number
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("ddmmyyxxx");
            }


            return ValidationResult.Success();

        }

        /// <summary>
        /// V-number
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{3}$"))
            {
                return ValidationResult.InvalidFormat("NNN");
            }
            return ValidationResult.Success();
        }
    }
}
