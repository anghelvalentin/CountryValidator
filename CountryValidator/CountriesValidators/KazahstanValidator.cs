using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class KazahstanValidator : IdValidationAbstract
    {
        public KazahstanValidator()
        {
            CountryCode = nameof(Country.KZ);
        }

        /// <summary>
        /// BIN БСН – бизнес-сәйкестендіру нөмірі
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(id, @"\d{12}$"))
            {
                return ValidationResult.InvalidFormat("123456789012");
            }
            return ValidationResult.Success();

        }

        /// <summary>
        /// PIN
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"\d{12}$"))
            {
                return ValidationResult.InvalidFormat("123456789012");
            }

            try
            {
                int month = int.Parse(ssn.Substring(2, 2));
                int day = int.Parse(ssn.Substring(4, 2));
                if (month > 12 || month < 1 || day < 1 || day > 31)
                {
                    return ValidationResult.InvalidDate();
                }
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// BIN 
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
            if (!Regex.IsMatch(postalCode, "^\\d{6}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
