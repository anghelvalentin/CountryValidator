using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class ChinaValidator : IdValidationAbstract
    {
        private int GetWeight(int n)
        {
            return (int)Math.Pow(2, n - 1) % 11;
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (id.Length != 18&&id.Length!=15)
            {
                return ValidationResult.Invalid("Invalid length! The code must have a length of 18 or 15");
            }


            if (id.Length == 18)
            {
                if (!Regex.IsMatch(id, "^[0-9]{17}[0-9X]$"))
                {
                    return ValidationResult.InvalidFormat("123456YYYYMMDD123X where YYYYMMDD - date of birth, X - checksum");
                }

                string dateString = id.Substring(6, 8);
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

                identifier = id.Substring(0, 17);
                checkDigit = int.Parse(id.Substring(17) == "X" ? "10" : id.Substring(17));


                int weightedSum = 0;
                int index = id.Length;

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
            else
            {
                //15 digits
                if (!Regex.IsMatch(id, "^[0-9]{15}"))
                {
                    return ValidationResult.InvalidFormat("123456YYMMDD123 where YYMMDD - date of birth");
                }
                string dateString = "19"+id.Substring(6, 6);    //people born after 2000 doesn't have 15 digits id
                try
                {
                    DateTime data = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (data > DateTime.Now)
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
