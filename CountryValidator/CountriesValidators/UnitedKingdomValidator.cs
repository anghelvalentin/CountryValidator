using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidator.Countries
{
    public class UnitedKingdomValidator : IdValidationAbstract
    {

        public UnitedKingdomValidator()
        {
            CountryCode = nameof(Country.GB);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        /// <summary>
        /// NINO or NHS
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (ValidateIndividualTaxCode(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            else if (ValidateNHS(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            return ValidationResult.Invalid("Invalid");
        }

        public override ValidationResult ValidateIndividualTaxCode(string nino)
        {
            if (string.IsNullOrWhiteSpace(nino))
            {
                return ValidationResult.Invalid("Emtpy nino");
            }
            bool isValid = Regex.IsMatch(nino, "^(?!BG|GB|NK|KN|TN|NT|ZZ)((?![DFIQUV])([A-Z])(?![DFIQUVO])([A-Z]))[0-9]{6}[A-D ]$");
            return isValid ? ValidationResult.Success() : ValidationResult.Invalid("Invalid format");
        }

        public ValidationResult ValidateNHS(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (ssn.Length != 10 || !ssn.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(ssn[i].ToString()) * (10 - i);
            }
            sum = sum % 11;
            sum = 11 - sum;
            if (sum == 11)
            {
                sum = 0;
            }
            else if (sum == 10)
            {
                return ValidationResult.InvalidChecksum();
            }

            bool isValid = int.Parse(ssn[9].ToString()) == sum;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Value added tax registration number 
        /// </summary>
        /// <param name="vatNumber"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatNumber)
        {
            vatNumber = vatNumber.RemoveSpecialCharacthers();
            vatNumber = vatNumber.Replace("gb", string.Empty).Replace("GB", string.Empty);
            var multipliers = new int[] { 8, 7, 6, 5, 4, 3, 2 };

            if (vatNumber.Substring(0, 2) == "GD")
            {
                bool isValidGD = int.Parse(vatNumber.Substring(2, 3)) < 500;
                return isValidGD ? ValidationResult.Success() : ValidationResult.Invalid("Invalid");
            }
            else if (vatNumber.Substring(0, 2) == "HA")
            {
                bool isValidHA = int.Parse(vatNumber.Substring(2, 3)) > 499;
                return isValidHA ? ValidationResult.Success() : ValidationResult.Invalid("Invalid");
            }

            var total = 0;
            if (vatNumber[0] == '0')
            {
                return ValidationResult.Invalid("Invalid format. First digit cannot be 0");
            }

            var no = long.Parse(vatNumber.Substring(0, 7));

            for (int i = 0; i < 7; i++)
            {
                total += int.Parse(vatNumber[i].ToString()) * multipliers[i];
            }

            var cd = total;
            while (cd > 0) { cd = cd - 97; }

            cd = Math.Abs(cd);
            if (cd == int.Parse(vatNumber.Substring(7, 2)) && no < 9990001 && (no < 100000 || no > 999999) && (no < 9490001 || no > 9700000))
            {
                return ValidationResult.Success();
            }

            cd = cd >= 55 ? cd - 55 : cd + 42;

            bool isValid = cd == int.Parse(vatNumber.Substring(7, 2)) && no > 1000000;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();


        }
    }
}
