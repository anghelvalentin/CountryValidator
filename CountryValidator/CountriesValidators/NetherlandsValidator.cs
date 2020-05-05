using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class NetherlandsValidator : IdValidationAbstract
    {
        public NetherlandsValidator()
        {
            CountryCode = nameof(Country.NL);
        }
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ValidationResult result = ValidateIndividualTaxCode(ssn);
            if (result.IsValid)
            {
                return result;
            }
            else
            {
                if (ValidateOnderwijsnummer(ssn).IsValid)
                {
                    return ValidationResult.Success();
                }
                return result;
            }
        }

        private int CheckSum(string number)
        {
            int sum = 0;
            for (int i = 0; i < number.Length - 1; i++)
            {
                sum += (9 - i) * (int)char.GetNumericValue(number[i]);
            }

            return (sum - (int)char.GetNumericValue(number[number.Length - 1])).Mod(11);
        }

        /// <summary>
        /// Burgerservicenummer (BSN) - Citizen Service Number
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!(number.All(char.IsDigit) || int.Parse(number) <= 0))
            {
                return ValidationResult.Invalid("Invalid format. Only digits are allowed");
            }
            else if (number.Length != 9)
            {
                return ValidationResult.InvalidLength();
            }
            else if (CheckSum(number) != 0)
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// Onderwijsnummer (the Dutch student identification number for students without BSN).
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public ValidationResult ValidateOnderwijsnummer(string number)
        {
            number = number.RemoveSpecialCharacthers();

            if (!number.All(char.IsDigit) || int.Parse(number) <= 0)
            {
                return ValidationResult.InvalidFormat("1034.56.789");
            }
            else if (!number.StartsWith("10"))
            {
                return ValidationResult.InvalidFormat("1034.56.789");
            }
            else if (number.Length != 9)
            {
                return ValidationResult.InvalidLength();
            }
            else if (Checksum(number) != 5)
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        private int Checksum(string number)
        {
            int sum = 0;
            for (int i = 0; i < number.Length - 1; i++)
            {
                sum += (9 - i) * (int)Char.GetNumericValue(number[i]);
            }
            return (sum - (int)Char.GetNumericValue(number[number.Length - 1])).Mod(11);
        }

        /// <summary>
        /// Omzetbelastingnummer (BTW)  
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {

            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId?.Replace("nl", string.Empty).Replace("NL", string.Empty);

            if (!Regex.IsMatch(vatId, @"^\d{9}B\d{2}$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }

            int[] multipliers = { 9, 8, 7, 6, 5, 4, 3, 2 };
            var sum = vatId.Sum(multipliers);

            var checkDigit = sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == vatId[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers().ToUpper();
            if (!Regex.IsMatch(postalCode, "^\\d{4}[A-Z]{2}$"))
            {
                return ValidationResult.InvalidFormat("NNNN WW");
            }
            return ValidationResult.Success();
        }
    }
}
