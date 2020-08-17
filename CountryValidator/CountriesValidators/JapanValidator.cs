using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class JapanValidator : IdValidationAbstract
    {
        public JapanValidator()
        {
            CountryCode = nameof(Country.JP);
        }

        private int CalculateChecksum(string number)
        {
            int[] weights = new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            char[] charArray = number.ToCharArray();
            Array.Reverse(charArray);
            number = new string(charArray);
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum = sum + weights[i] * (int)Char.GetNumericValue(number[i]);
            }

            sum = sum % 9;
            return (9 - sum);
        }

        /// <summary>
        /// CN hōjin bangō, Japanese Corporate Number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (number.Length != 13)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }
            else if (CalculateChecksum(number.Substring(1)) != (int)char.GetNumericValue(number[0]))
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();

        }

        /// <summary>
        /// Japan My Number
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^\d{12}$"))
            {
                return ValidationResult.InvalidFormat("123456789012");
            }
            else if (CalculateCheckSum(ssn.Substring(0, 11)) != (int)char.GetNumericValue(ssn[11]))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        private int CalculateCheckSum(string digits)
        {
            var n = 1;
            var _result = 0;

            char[] charArray = digits.ToCharArray();
            Array.Reverse(charArray);
            digits = new string(charArray);

            for (int i = 0; i < digits.Length; i++)
            {
                _result += (int)char.GetNumericValue(digits[i]) * Factor(n);

                n += 1;
            }

            int surplus = _result % 11;

            if (surplus <= 1)
            {
                return 0;
            }
            return 11 - surplus;
        }

        private int Factor(int n)
        {
            if (1 <= n && n <= 6)
            {
                return n + 1;
            }
            else
            {//7 <= n && n <= 11
                return n - 5;
            }
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{7}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNNN or NNN-NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
