using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class AlbaniaValidator : IdValidationAbstract
    {
        public AlbaniaValidator()
        {
            CountryCode = nameof(Country.AL);
        }

        /// <summary>
        /// NIPT (Numri i Identifikimit për Personin e Tatueshëm, Albanian VAT number).
        /// </summary>
        /// <param name="nipt"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string nipt)
        {
            return ValidateVAT(nipt);
        }

        /// <summary>
        /// Identity Number - Numri i Identitetit (NID)
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (ssn.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!Regex.IsMatch(ssn, @"^[A-O]\d(0[1-9]|1[0-2]|5[1-9]|6[0-2])(0?[1-9]|[1-3][0-9]|4[0-2])\d{3}\w$"))
            {
                return ValidationResult.InvalidFormat("YYMMDDSSSC");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// NIPT (Numri i Identifikimit për Personin e Tatueshëm).
        /// </summary>
        /// <param name="nipt"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string nipt)
        {
            return ValidateVAT(nipt);
        }

        /// <summary>
        /// NIPT (Numri i Identifikimit për Personin e Tatueshëm, Albanian VAT number).
        /// </summary>
        /// <param name="nipt"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string nipt)
        {
            nipt = nipt.RemoveSpecialCharacthers();
            nipt = nipt.Replace("AL", string.Empty).Replace("al", string.Empty);

            if (nipt.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            if (!Regex.IsMatch(nipt, "^[JKL][0-9]{8}[A-Z]$"))
            {
                return ValidationResult.InvalidFormat("[JKL]12345678A");
            }
            return ValidationResult.Success();
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
