using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class UnitedStatesValidator : IdValidationAbstract
    {
        public UnitedStatesValidator()
        {
            CountryCode = nameof(Country.US);
        }

        static readonly string[] campuses = new string[]{"10", "12", "60", "67", "50", "53", "01", "02", "03", "04", "05", "06", "11", "13", "14", "16", "21", "22", "23", "25", "34", "51", "52", "54", "55",
            "56", "57", "58", "59", "65","30", "32", "35","36", "37", "38", "61","15", "24","20", "26", "27", "45", "46", "47","40", "44","94",
            "95","80", "90","33", "39", "41", "42", "43", "46", "48", "62", "63", "64", "66", "68",
            "71", "72", "73", "74", "75", "76", "77", "81", "82", "83", "84", "85", "86", "87", "88", "91", "92", "93", "98", "99","31"};

        /// <summary>
        /// Validate EIN
        /// </summary>
        /// <param name="ein"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string ein)
        {
            if (!Regex.IsMatch(ein, @"^\d{2}[-]{0,1}\d{7}$"))
            {
                return ValidationResult.InvalidFormat("12-1234567");
            }

            bool isValid = campuses.Contains(ein.Substring(0, 2));
            return isValid ? ValidationResult.Success() : ValidationResult.Invalid("Invalid campus");
        }


        /// <summary>
        /// Validate ITIN
        /// </summary>
        /// <param name="fiscalCode"></param>
        /// <returns></returns>
        public ValidationResult ValidateITIN(string fiscalCode)
        {
            bool isValid = Regex.IsMatch(fiscalCode, @"^(9\d{2})[- ]{0,1}((7[0-9]{1}|8[0-8]{1})|(9[0-2]{1})|(9[4-9]{1}))[- ]{0,1}(\d{4})$");
            return isValid ? ValidationResult.Success() : ValidationResult.Invalid("Invalid code");

        }

        /// <summary>
        /// Validate SSN
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public ValidationResult ValidateSSN(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            Match match = Regex.Match(ssn, "^([0-9]{3})([0-9]{2})([0-9]{4})$");

            if (!match.Success)
            {
                return ValidationResult.InvalidFormat("123-456-789");
            }

            var area = match.Groups[1].Value;
            var group = match.Groups[2].Value;
            var serial = match.Groups[3].Value;
            if (area == "000" || area == "666" || area[0] == '9' || group == "00" || serial == "0000")
            {
                return ValidationResult.Invalid("Invalid code");
            }

            if (ssn == "078051120" || ssn == "457555462" || ssn == "219099999")
            {
                return ValidationResult.Invalid("Invalid code");
            }
            return ValidationResult.Success();
        }


        /// <summary>
        /// Validat social security number or itin
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (ValidateSSN(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            else if (ValidateITIN(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            return ValidationResult.Invalid("Invalid");
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException($"{CountryCode} doesn't have VAT");
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.Trim();
            if (!Regex.IsMatch(postalCode, @"^(?:(\d{5})(?:[ \-](\d{4}))?)$"))
            {
                return ValidationResult.InvalidFormat("NNNNN OR  NNNNN-NNNN");
            }
            return ValidationResult.Success();
        }
    }
}
