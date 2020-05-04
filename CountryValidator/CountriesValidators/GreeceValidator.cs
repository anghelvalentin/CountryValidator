using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CountryValidator.Countries;

namespace CountryValidator.Countries
{
    public class GreeceValidator : IdValidationAbstract
    {
        public GreeceValidator()
        {
            CountryCode = nameof(Country.GR);
        }

        /// <summary>
        /// AMKA (Αριθμός Μητρώου Κοινωνικής Ασφάλισης, Greek social security number).
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string number)
        {
            number = number?.RemoveSpecialCharacthers();
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            else if (number.Length != 11)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.CheckLuhnDigit())
            {
                return ValidationResult.InvalidChecksum();
            }
            try
            {
                int day = int.Parse(number.Substring(0, 2));
                int month = int.Parse(number.Substring(2, 2));
                int year = int.Parse(number.Substring(4, 2)) + 1900;
                DateTime date = new DateTime(year, month, day);
                if (date > DateTime.Now)
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

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            return ValidateVAT(ssn);
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers().ToUpper().Replace("EL", string.Empty).Replace("GR", string.Empty);

            if (vatId.Length == 8)
            {
                vatId = "0" + vatId;
            }

            if (!Regex.IsMatch(vatId, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("123456789");
            }
            int[] multipliers = { 256, 128, 64, 32, 16, 8, 4, 2 };

            var sum = vatId.Sum(multipliers);
            var checkDigit = sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == vatId[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
