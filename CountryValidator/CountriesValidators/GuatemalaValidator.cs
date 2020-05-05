using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CountryValidation.Countries;

namespace CountryValidation.Countries
{
    public class GuatemalaValidator : IdValidationAbstract
    {
        public GuatemalaValidator()
        {
            CountryCode = nameof(Country.GT);
        }

        public string CalculateChecksum(string number)
        {
            int sum = 0;

            char[] charArray = number.ToCharArray();
            Array.Reverse(charArray);
            number = new string(charArray);

            for (int i = 2; i <= number.Length + 1; i++)
            {
                sum = sum + (i * (int)char.GetNumericValue(number[i - 2]));
            }



            int c = (-sum).Mod(11);
            if (c == 10)
            {
                return "K";
            }
            else
            {
                return c.ToString();
            }

        }

        /// <summary>
        /// NIT (Número de Identificación Tributaria, Guatemala tax number)
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string nit)
        {
            nit = nit.RemoveSpecialCharacthers();
            if (nit.Length < 2 || nit.Length > 12)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!nit.Substring(0, nit.Length - 1).All(char.IsDigit))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            else if (nit[nit.Length - 1] != 'K' && !char.IsDigit(nit[nit.Length - 1]))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            else if (nit[nit.Length - 1].ToString() != CalculateChecksum(nit.Substring(0, nit.Length - 1)))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();

        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NIT
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(vatId, @"^\d{8}$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            return ValidationResult.Success();

        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
