
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidator.Countries
{
    public class AustriaValidator : IdValidationAbstract
    {
        public AustriaValidator()
        {
            CountryCode = nameof(Country.AT);
        }


        private int CalculateChecksum(string number)
        {
            int[] weights = new int[] { 3, 7, 9, 0, 5, 8, 4, 2, 1, 6 };
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (int)char.GetNumericValue(number[i]);
            }
            sum = sum % 11;

            return sum;
        }

        /// <summary>
        /// Austia Versicherungsnummer (VNR, SVNR, VSNR)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!number.All(char.IsDigit) || number.StartsWith("0"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }
            else if (number.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            else if (CalculateChecksum(number) != (int)char.GetNumericValue(number[3]))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// Austrian Company Register Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers().Replace("FN", string.Empty).Replace("fn", string.Empty);

            if (!Regex.IsMatch(id, "^[0-9]+[a-z]$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidationResult.Success();
        }

        public int CalculateChecksumTaxCode(string number)
        {
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                int n = (int)char.GetNumericValue(number[i]);
                if (i % 2 != 0)
                {
                    sum += (int)char.GetNumericValue("0246813579"[n]);
                }
                else
                {
                    sum += n;
                }
            }
            return (10 - sum).Mod(10);
        }

        /// <summary>
        /// Austrian tax identification number (Abgabenkontonummer)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (number.Length != 9)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("123456789");
            }
            else if (CalculateChecksumTaxCode(number) != (int)char.GetNumericValue(number[number.Length - 1]))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }


        /// <summary>
        /// UID (Umsatzsteuer-Identifikationsnummer)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("AT", string.Empty).Replace("at", string.Empty);

            if (!Regex.IsMatch(vatId, @"^U\d{8}$"))
            {
                return ValidationResult.InvalidFormat("U12345678");
            }

            var index = 1;
            var sum = 0;
            int[] _multipliers = { 1, 2, 1, 2, 1, 2, 1 };

            foreach (var digit in _multipliers)
            {
                var temp = vatId[index++].ToInt() * digit;
                sum += temp > 9 ? (int)Math.Floor(temp / 10D) + temp % 10 : temp;
            }

            var checkDigit = 10 - (sum + 4) % 10;

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            var isValid = checkDigit == vatId[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
