
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class AzerbaijanValidator : IdValidationAbstract
    {
        public AzerbaijanValidator()
        {
            CountryCode = nameof(Country.AZ);
        }

        /// <summary>
        /// PIN - Personal Identification Number
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(ssn, @"^\w{7}$"))
            {
                return ValidationResult.InvalidFormat("5VBK5VR");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// VÖEN/TIN Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"^\d{10}$"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// VÖEN/TIN Number
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            return ValidateEntity(ssn);
        }

        /// <summary>
        /// VÖEN/TIN Number
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
            if (!Regex.IsMatch(postalCode, "^[Aa][Zz]\\d{4}$"))
            {
                return ValidationResult.InvalidFormat("CCNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
