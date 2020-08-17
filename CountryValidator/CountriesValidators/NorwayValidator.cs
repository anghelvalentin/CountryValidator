using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class NorwayValidator : IdValidationAbstract
    {
        public NorwayValidator()
        {
            CountryCode = nameof(Country.NO);
        }
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }


        /// <summary>
        /// Fødselsnummer 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number.RemoveSpecialCharacthers();

            if (number.Length != 11)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            else if ((int)char.GetNumericValue(number[9]) != CalculateChecksum(number, new int[] { 3, 7, 6, 1, 8, 9, 4, 5, 2 }))
            {
                return ValidationResult.InvalidChecksum();
            }
            else if ((int)char.GetNumericValue(number[10]) != CalculateChecksum(number, new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 }))
            {
                return ValidationResult.InvalidChecksum();
            }
            else if (!HasValidDate(number))
            {
                return ValidationResult.InvalidDate();
            }

            return ValidationResult.Success();
        }

        private int CalculateChecksum(string number, int[] weights)
        {
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (int)char.GetNumericValue(number[i]);
            }

            return 11 - sum % 11;
        }

        private bool HasValidDate(string number)
        {
            var day = int.Parse(number.Substring(0, 2));
            var month = int.Parse(number.Substring(2, 2));
            var year = int.Parse(number.Substring(4, 2));
            var individual_digits = int.Parse(number.Substring(6, 3));

            if (day >= 80)
            {
                //'This number is an FH-number, and does not contain birth date information by design.')
                return false;
            }
            if (day > 40)
            {
                day -= 40;
            }
            if (month > 40)
            {
                month -= 40;
            }

            if (individual_digits < 500)
            {
                year += 1900;
            }
            else if (500 <= individual_digits && individual_digits < 750 && year >= 54)
            {
                year += 1800;
            }
            else if (500 <= individual_digits && individual_digits < 1000 && year < 40)
            {
                year += 2000;
            }
            else if (900 <= individual_digits && individual_digits < 1000 && year >= 40)
            {
                year += 1900;
            }
            else
            {

                return false;
            }
            try
            {
                DateTime date = new DateTime(year, month, day);
                return date < DateTime.Now;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Organisasjonsnummer (Organization number) Orgnr
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId?.RemoveSpecialCharacthers();
            vatId = vatId?.Replace("NO", string.Empty).Replace("no", string.Empty)
                .Replace("MVA", string.Empty).Replace("mva", string.Empty);

            if (!Regex.IsMatch(vatId, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("123456789");
            }

            var total = 0;
            int[] multipliers = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };

            for (var i = 0; i < 8; i++)
            {
                total += int.Parse(vatId[i].ToString()) * multipliers[i];
            }

            total = 11 - total % 11;
            if (total == 11)
            {
                total = 0;
            }
            if (total < 10)
            {

                bool isValid = total == int.Parse(vatId.Substring(8));
                return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
            }
            return ValidationResult.InvalidChecksum();
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
