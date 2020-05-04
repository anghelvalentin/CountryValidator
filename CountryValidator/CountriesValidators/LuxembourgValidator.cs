using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidator.Countries
{
    public class LuxembourgValidator : IdValidationAbstract
    {
        public LuxembourgValidator()
        {
            CountryCode = nameof(Country.LU);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        int[,] D = {
             { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
             { 1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
             {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
             {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
             {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
             {5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
             {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
             {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
             {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
             {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}
        };

        int[,] P = {
        {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
        {1, 5, 7, 6, 2, 8, 3, 0, 9, 4},
        {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
        {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
        {9, 4, 5, 3, 1, 2, 6, 8, 7, 0},
        {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
        {2, 7, 9, 3, 8, 0, 6, 4, 1, 5},
        {7, 0, 4, 6, 9, 1, 3, 2, 5, 8}
    };


        public ValidationResult ValidateResident(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (ssn?.Length != 11)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!ssn.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }

            int[] mutlipliers = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < mutlipliers.Length; i++)
            {
                sum += (int)char.GetNumericValue(ssn[i]) * mutlipliers[i];
            }
            int check = 11 - sum % 11;
            return (int)char.GetNumericValue(ssn[ssn.Length - 1]) == check ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ValidationResult validationResult = ValidateNaturalPersons(ssn);
            if (validationResult.IsValid)
            {
                return validationResult;
            }
            else if (ValidateResident(ssn).IsValid)
            {
                return ValidationResult.Success();
            }
            return validationResult;
        }

        public ValidationResult ValidateNaturalPersons(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (ssn.Length != 13)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!Regex.IsMatch(ssn, "(1[89]|20)\\d{2}(0[1-9]|1[012])(0[1-9]|[1-2][0-9]|3[0-1])\\d{5}"))
            {
                return ValidationResult.Invalid("Invalid format");
            }

            try
            {
                var year = int.Parse(ssn.Substring(0, 4));
                var month = int.Parse(ssn.Substring(4, 2));
                var day = int.Parse(ssn.Substring(6, 2));
                DateTime date = new DateTime(year, month, day);
                if (date > DateTime.Now)
                {
                    return ValidationResult.InvalidDate();
                }
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }

            int sum = 0;
            for (int i = 0; i < ssn.Length - 1; i++)
            {
                if (i % 2 != 0)
                {
                    sum += (int)char.GetNumericValue(ssn[i]);
                }
                else
                {
                    sum += ((int)char.GetNumericValue(ssn[i]) * 2).ToString().ToCharArray().Sum(c => c - '0');
                }
            }

            if (sum % 10 != 0)
            {
                return ValidationResult.InvalidChecksum();
            }

            List<int> listNumbers = new List<int>();
            for (var i = 12; i >= 0; i--)
            {
                if (i != 11)
                {
                    listNumbers.Add((int)char.GetNumericValue(ssn[i]));
                }
            }
            var check = 0;
            for (var j = 0; j < listNumbers.Count; j++)
            {
                var item = listNumbers[j];
                var p = P[j % 8, item];
                check = D[check, p];
            }
            return check == 0 ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// TVA (taxe sur la valeur ajoutée, Luxembourgian VAT number)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("lu", string.Empty).Replace("LU", string.Empty);

            if (!Regex.IsMatch(vatId, @"^\d{8}$"))
            {
                return ValidationResult.InvalidFormat("12345678");
            }

            var isValid = int.Parse(vatId.Substring(0, 6)) % 89 == int.Parse(vatId.Substring(6, 2));
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
