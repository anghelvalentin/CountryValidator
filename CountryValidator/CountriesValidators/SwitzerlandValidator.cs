using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CountryValidation.Countries;

namespace CountryValidation.Countries
{
    public class SwitzerlandValidator : IdValidationAbstract
    {
        public SwitzerlandValidator()
        {
            CountryCode = nameof(Country.CH);
        }

        private int GetCheckDigit(string ssn)
        {
            var total = 0;

            for (var i = 0; i < 12; i += 1)
            {
                if (i % 2 == 0)
                {
                    total += Int32.Parse(ssn[i].ToString());
                }
                else
                {
                    total += Int32.Parse((ssn[i].ToString())) * 3;
                }
            }

            var expectedCheckDigit = 0;
            if (total % 10 != 0)
            {
                var roundTen = Math.Floor((decimal)total / 10) * 10 + 10;
                expectedCheckDigit = (int)roundTen - total;
            }

            return expectedCheckDigit;
        }

        /// <summary>
        /// Validate AHV (Sozialversicherungsnummer)
        /// </summary>
        /// <param name="ahv"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ahv)
        {
            ahv = ahv.RemoveSpecialCharacthers();

            var checkDigit = GetCheckDigit(ahv);
            return (int)char.GetNumericValue(ahv[12]) == checkDigit ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// UID (Unternehmens-Identifikationsnummer, Swiss business identifier)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers().ToUpper();
            if (number.Length != 12)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!number.StartsWith("CHE"))
            {
                return ValidationResult.Invalid("Invalid company. First 3 letters must be 'CHE'");
            }
            else if (!number.Substring(3).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("CHE123456789");
            }
            else if ((int)char.GetNumericValue(number[number.Length - 1]) != CalculateEntityCheckSum(number.Substring(3, 8)))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        private int CalculateEntityCheckSum(string v)
        {

            var weights = new int[] { 5, 4, 3, 2, 7, 6, 5, 4 };
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (int)char.GetNumericValue(v[i]);
            }
            return (11 - sum).Mod(11);
        }

        /// <summary>
        /// VAT, MWST, TVA, IVA, TPV (Mehrwertsteuernummer, the Swiss VAT number).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string value)
        {
            value = value.RemoveSpecialCharacthers();
            value = value.Replace("CH", string.Empty).Replace("ch", string.Empty);
            value = value.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(value, "^E?[0-9]{9}(MWST|IVA|TVA)?$"))
            {
                return ValidationResult.InvalidFormat("E123456789IVA");
            }
            else if (!Char.IsNumber(value[0]))
            {
                value = value.Substring(1);
            }

            var sum = 0;
            int[] weight = new int[] { 5, 4, 3, 2, 7, 6, 5, 4 };
            for (var i = 0; i < 8; i++)
            {
                sum += (int)(Char.GetNumericValue(value[i])) * weight[i];
            }

            sum = 11 - sum % 11;
            if (sum == 10)
            {
                return ValidationResult.InvalidChecksum();
            }
            if (sum == 11)
            {
                sum = 0;
            }

            return sum.ToString() == value.Substring(8, 1) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
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
