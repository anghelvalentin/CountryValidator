using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class IcelandValidator : IdValidationAbstract
    {
        public IcelandValidator()
        {
            CountryCode = nameof(Country.IS);
        }
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateIndividualTaxCode(id);
        }


        /// <summary>
        /// Kennitala
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string value)
        {
            value = value.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(value, "^[0-9]{6}[0-9]{4}$"))
            {
                return ValidationResult.InvalidFormat("Invalid format");
            }
            try
            {
                var day = int.Parse(value.Substring(0, 2));
                var month = int.Parse(value.Substring(2, 2));
                var year = int.Parse(value.Substring(4, 2));
                var century = (int)char.GetNumericValue(value[9]);

                year = (century == 9) ? (1900 + year) : ((20 + century) * 100 + year);
                DateTime date = new DateTime(year, month, day);
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }

            var sum = 0;
            var weight = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
            for (var i = 0; i < 8; i++)
            {
                sum += (int)char.GetNumericValue(value[i]) * weight[i];
            }
            sum = 11 - sum % 11;
            return sum == value[8] ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }


        /// <summary>
        /// Virdisaukaskattsnumer (VSK)  
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("is", string.Empty).Replace("IS", string.Empty);

            if (Regex.IsMatch(vatId, @"^[0-9]{5,6}$"))
            {
                return ValidationResult.Success();
            }
            return ValidationResult.Invalid("The VAT code should have 5 or 6 digits  ");
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
