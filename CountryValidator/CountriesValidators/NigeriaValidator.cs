using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class NigeriaValidator : IdValidationAbstract
    {
        public NigeriaValidator()
        {
            CountryCode = nameof(Country.NG);
        }

        /// <summary>
        /// Nigerian National Identification Number (NIN)
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateIndividualTaxCode(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^\d{10}$"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// JBT TIN
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateIndividualTaxCode(vatId);
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
