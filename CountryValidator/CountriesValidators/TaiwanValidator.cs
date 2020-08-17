using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class TaiwanValidator : IdValidationAbstract
    {
        public TaiwanValidator()
        {
            CountryCode = nameof(Country.TW);
        }
        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate ssn for locals and residents
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (Regex.IsMatch(ssn, "^[A-Z][12][0-9]{8}$"))
            {
                return ValidateLocalSSN(ssn);
            }
            else if (Regex.IsMatch(ssn, "^[A-Z][A-D][0-9]{8}$"))
            {
                return ValidateResidentSSN(ssn);
            }
            return ValidationResult.Invalid("Invalid format");
        }

        /// <summary>
        /// Validate local SSN
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public ValidationResult ValidateLocalSSN(string ssn)
        {
            int idLen = ssn.Length;
            string letters = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            int letterIndex = letters.IndexOf(ssn[0]);
            decimal weightedSum = Math.Floor((decimal)letterIndex / 10 + 1) + letterIndex * (idLen - 1);
            string idTail = ssn.Substring(1);

            int weight = idLen - 2;

            for (int i = 0, len = idTail.Length; i < len; i++)
            {
                string _char2 = idTail[i].ToString();
                weightedSum += int.Parse(_char2) * weight;
                weight--;
            }

            decimal remainder = (weightedSum + int.Parse(ssn.Substring(9))) % 10;
            bool isValid = remainder == 0;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        /// <summary>
        /// Validate ssn of a resident
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public ValidationResult ValidateResidentSSN(string ssn)
        {
            int idLen = ssn.Length;

            string letters = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            int letterIndex = letters.IndexOf(ssn[0]);
            decimal weightedSum = Math.Floor((decimal)letterIndex / 10 + 1) + letterIndex * (idLen - 1);
            weightedSum += letters.IndexOf(ssn[1]) * (idLen - 2);
            string idTail = ssn.Substring(2);

            int weight = idLen - 3;

            for (int i = 0, len = idTail.Length; i < len; i++)
            {
                string _char3 = idTail[i].ToString();
                weightedSum += int.Parse(_char3) * weight;
                weight--;
            }

            decimal remainder = (weightedSum + int.Parse(ssn.Substring(9))) % 10;
            bool isValid = remainder == 0;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
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
