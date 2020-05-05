using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class ChinaValidator : IdValidationAbstract
    {
        private int GetWeight(int n)
        {
            return (int)Math.Pow(2, n - 1) % 11;
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (ssn.Length != 18)
            {
                return ValidationResult.Invalid("Invalid length! The code must have a length of 18");
            }


            if (!Regex.IsMatch(ssn, "^[0-9]{17}[0-9X]$"))
            {
                return ValidationResult.InvalidFormat("123456YYYYMMDD123X where YYYYMMDD - date of birth, X - checksum");
            }

            string dateString = ssn.Substring(6, 8);
            try
            {
                DateTime data = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!(data < DateTime.Now && data.Year > 1886))
                {
                    return ValidationResult.InvalidDate();
                }
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }

            string _char;
            int checkDigit;
            string identifier;
            int remainder;

            identifier = ssn.Substring(0, 17);
            checkDigit = int.Parse(ssn.Substring(17) == "X" ? "10" : ssn.Substring(17));


            int weightedSum = 0;
            int index = ssn.Length;

            for (int i = 0, len = identifier.Length; i < len; i++)
            {
                _char = identifier[i].ToString();
                weightedSum += int.Parse(_char) * GetWeight(index);
                index--;
            }

            remainder = (12 - weightedSum % 11) % 11 - checkDigit;
            bool isValid = remainder == 0;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
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
