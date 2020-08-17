using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class VenezuelaValidator : IdValidationAbstract
    {
        public VenezuelaValidator()
        {
            CountryCode = nameof(Country.VE);
        }
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            return ValidateVAT(id);
        }

        /// <summary>
        /// Registro de Informacion Fiscal (RIF) 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string id)
        {
            id = id.RemoveSpecialCharacthers();
            id = id?.Replace("VE", string.Empty).Replace("ve", string.Empty);

            if (id.Length != 10)
            {
                return ValidationResult.Invalid("Invalid length");
            }
            else if (!Regex.IsMatch(id, "^[VEJPG][0-9]{9}$"))
            {
                return ValidationResult.InvalidFormat("[VEJPG]123456789");
            }

            Dictionary<char, int> types = new Dictionary<char, int>
            {
                { 'V',4},//natural person born in Venezuela
                { 'E', 8},//foreign natural person
                { 'J', 12},//company
                { 'P',16},//passport
                { 'G', 20}//government
            };

            var sum = types[id[0]];
            var weight = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };

            for (var i = 0; i < 8; i++)
            {
                sum += int.Parse(id[i + 1].ToString()) * weight[i];
            }

            sum = 11 - sum % 11;
            if (sum == 11 || sum == 10)
            {
                sum = 0;
            }

            bool isValid = sum.ToString() == id.Substring(9, 1);
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{4}(\\s[a-zA-Z]{1})?$"))
            {
                return ValidationResult.InvalidFormat("NNNN or NNNN A");
            }
            return ValidationResult.Success();
        }
    }
}
