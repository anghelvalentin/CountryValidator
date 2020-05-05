using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class PakistanValidator : IdValidationAbstract
    {

        public PakistanValidator()
        {
            CountryCode = nameof(Country.PK);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Validate CNIC (Computerized National Identity Card)
        /// </summary>
        /// <param name="cnic"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string cnic)
        {
            cnic = cnic.Trim();

            var isValid = Regex.IsMatch(cnic, "^[1-7][0-9]{4}-[0-9]{7}-[1-9]{1}$");
            if (isValid)
            {
                return ValidationResult.Success();
            }
            else
            {
                return ValidationResult.Invalid("Invalid format");
            }
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
