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
            if (!Regex.IsMatch(ssn, @"^\d{5,8}\w?$"))
            {
                return ValidationResult.InvalidFormat("1234567");
            }
            return ValidationResult.Success();

        }

        /// <summary>
        /// Número de Identificación Tributaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{10,}$"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }

            return ValidationResult.Success();
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            return ValidateEntity(id);
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
