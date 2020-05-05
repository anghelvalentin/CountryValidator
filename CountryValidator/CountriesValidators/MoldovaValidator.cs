using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MoldovaValidator : IdValidationAbstract
    {
        public MoldovaValidator()
        {
            CountryCode = nameof(Country.MD);
        }

        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }
            else if (number.Length != 13)
            {
                return ValidationResult.InvalidLength();
            }
            else if ((int)char.GetNumericValue((number[number.Length - 1])) != CalculateChecksum(number.Substring(0, number.Length - 1)))
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// IDNP (Identification Number of Person)
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^\d{13}$"))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// Validate VAT code (Nr. de Inregistrare TVA)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(vatId, @"^\d{7}$"))
            {
                return ValidationResult.InvalidFormat("1234567");
            }
            return ValidationResult.Success();

        }

        private int CalculateChecksum(string number)
        {
            number = number.RemoveSpecialCharacthers();
            int[] weights = new int[] { 7, 3, 1, 7, 3, 1, 7, 3, 1, 7, 3, 1 };

            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum += weights[i] * (int)char.GetNumericValue(number[i]);
            }

            return sum % 10;
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^[Mm][Dd][-]{0,1}\\d{4}$"))
            {
                return ValidationResult.InvalidFormat("CCNNNN CC-NNNN");
            }
            return ValidationResult.Success();
        }
    }
}
