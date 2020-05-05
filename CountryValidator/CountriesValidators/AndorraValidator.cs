
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class AndorraValidator : IdValidationAbstract
    {
        public AndorraValidator()
        {
            CountryCode = nameof(Country.AD);
        }

        /// <summary>
        /// NRT (Número de Registre Tributari, Andorra tax number)
        /// </summary>
        /// <param name="nrt"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string nrt)
        {
            return ValidateVAT(nrt);
        }

        /// <summary>
        /// NRT (Número de Registre Tributari, Andorra tax number)
        /// </summary>
        /// <param name="nrt"></param>
        /// <returns></returns>
        /// https://www.oecd.org/tax/automatic-exchange/crs-implementation-and-assistance/tax-identification-numbers/Andorra-TIN.pdf
        public override ValidationResult ValidateIndividualTaxCode(string nrt)
        {
            nrt = nrt.RemoveSpecialCharacthers();
            nrt = nrt.Replace("AD", string.Empty).Replace("ad", string.Empty);

            if (nrt.Length != 8)
            {
                return ValidationResult.InvalidLength();
            }

            if (!char.IsLetter(nrt[0]) || !char.IsLetter(nrt[nrt.Length - 1]))
            {
                return ValidationResult.Invalid("Invalid format. First and last character must be letters");
            }
            else if (!nrt.Substring(1, 6).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("F-123456-Z");
            }
            else if (!Regex.IsMatch(nrt, "^[ACDEFGLOPU]"))
            {
                return ValidationResult.Invalid("Invalid format. First letter must be ACDEFGLOPU");
            }
            else if (nrt[0] == 'F' && int.Parse(nrt.Substring(1, 6)) > 699999)
            {
                return ValidationResult.Invalid("Invalid format.The number code cannot be higher than 699999");
            }
            if ((nrt[0] == 'A' || nrt[0] == 'L') && !(699999 < int.Parse(nrt.Substring(1, 6)) && int.Parse(nrt.Substring(1, 6)) < 800000))
            {
                return ValidationResult.Invalid("Invalid format.The number code must be between 699999 and 800000");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// NRT (Número de Registre Tributari, Andorra tax number)
        /// </summary>
        /// <param name="nrt"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string nrt)
        {
            nrt = nrt.RemoveSpecialCharacthers();
            nrt = nrt.Replace("AD", string.Empty).Replace("ad", string.Empty);

            if (nrt.Length != 8)
            {
                return ValidationResult.InvalidLength();
            }

            if (!char.IsLetter(nrt[0]) || !char.IsLetter(nrt[nrt.Length - 1]))
            {
                return ValidationResult.Invalid("Invalid format. First and last character must be letters");
            }
            else if (!nrt.Substring(1, 6).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("F-123456-Z");
            }
            else if (!Regex.IsMatch(nrt, "^[ACDEFGLOPU]"))
            {
                return ValidationResult.Invalid("Invalid format. First letter must be ACDEFGLOPU");
            }
            else if (nrt[0] == 'F' && int.Parse(nrt.Substring(1, 6)) > 699999)
            {
                return ValidationResult.Invalid("Invalid format.The number code cannot be higher than 699999");
            }
            if ((nrt[0] == 'A' || nrt[0] == 'L') && !(699999 < int.Parse(nrt.Substring(1, 6)) && int.Parse(nrt.Substring(1, 6)) < 800000))
            {
                return ValidationResult.Invalid("Invalid format.The number code must be between 699999 and 800000");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^[Aa][Dd]\\d{3}$"))
            {
                return ValidationResult.InvalidFormat("CCNNN");
            }
            return ValidationResult.Success();
        }
    }
}
