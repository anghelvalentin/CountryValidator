using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class RomaniaValidator : IdValidationAbstract
    {
        public RomaniaValidator()
        {
            CountryCode = nameof(Country.RO);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        /// <summary>
        /// CNP - Cod Numeric Personal
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            int hashResult = 0;
            int[] cnp = new int[ssn.Length];
            int[] hashTable = new int[] { 2, 7, 9, 1, 4, 6, 3, 5, 8, 2, 7, 9 };

            if (ssn.Length != 13)
            {
                return ValidationResult.Invalid("Invalid length. The CNP must have 13 digits");
            }
            for (int i = 0; i < 13; i++)
            {

                if (int.TryParse(ssn[i].ToString(), out cnp[i]))
                {
                    if (i < 12) { hashResult = hashResult + (cnp[i] * hashTable[i]); }
                }
                else
                {
                    return ValidationResult.InvalidFormat("1234567890123");
                }
            }
            hashResult = hashResult % 11;
            if (hashResult == 10)
            {
                hashResult = 1;
            }

            int year = (cnp[1] * 10) + cnp[2];
            switch (cnp[0])
            {
                case 1:
                case 2:
                    year += 1900;
                    break;

                case 3:
                case 4:
                    year += 1800;
                    break;

                case 5:
                case 6:
                    year += 2000;
                    break;

                case 7:
                case 8:
                case 9:
                    year += 2000;
                    if (year > (DateTime.Now.Year - 14))
                    {
                        year -= 100;
                    }
                    break;

                default: { return ValidationResult.Invalid("The first digit cannot be 0"); }
            }
            if (year < 1800 || year > 2099)
            {
                return ValidationResult.InvalidDate();
            }
            bool isValid = cnp[12] == hashResult;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("RO", string.Empty).Replace("ro", string.Empty);

            if (!Regex.IsMatch(vatId, @"^[0-9]{2,10}$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }

            int[] multipliers = { 7, 5, 3, 2, 1, 7, 5, 3, 2 };

            var end = vatId.Length - 1;

            var controlDigit = vatId[end].ToInt();

            var slice = vatId.Slice(0, end);

            vatId = slice.PadLeft(9, '0');

            var sum = vatId.Sum(multipliers);

            var checkDigit = sum * 10 % 11;

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == controlDigit;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
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
