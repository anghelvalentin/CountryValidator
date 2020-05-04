using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class DenmarkValidator : IdValidationAbstract
    {
        public DenmarkValidator()
        {
            CountryCode = nameof(Country.DK);
        }
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }


        /// <summary>
        /// Det Centrale Personregister
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string value)
        {
            value = value.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(value, "^[0-9]{6}[-]{0,1}[0-9]{4}$"))
            {
                return ValidationResult.InvalidFormat("Invalid format");
            }

            var day = int.Parse(value.Substring(0, 2));
            var month = int.Parse(value.Substring(2, 2));
            var year = int.Parse(value.Substring(4, 2));


            if ("5678".IndexOf(value[6]) != -1 && year >= 58)
            {
                year += 1800;
            }
            else if ("0123".IndexOf(value[6]) != -1 || ("49".IndexOf(value[6]) != -1 && year >= 37))
            {
                year += 1900;

            }
            else
            {
                year += 2000;
            }

            try
            {
                DateTime dateTime = new DateTime(year, month, day);
                if (dateTime > DateTime.Now)
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
        /// Denmark Momsregistreringsnummer (CVR/VAT)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers().ToUpper().Replace("DK", string.Empty);

            if (vatId.Length != 8)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!vatId.All(char.IsDigit) || vatId[0] == '0')
            {
                return ValidationResult.InvalidFormat("12345678");
            }


            int[] multipliers = { 2, 7, 6, 5, 4, 3, 2, 1 };
            var sum = vatId.Sum(multipliers);

            bool isValid = sum % 11 == 0;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
