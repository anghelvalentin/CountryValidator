using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidator.Countries
{
    public class EstoniaValidator : IdValidationAbstract
    {
        public EstoniaValidator()
        {
            CountryCode = nameof(Country.EE);
        }

        /// <summary>
        /// Registrikood (Estonian organisation registration code).
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678");
            }
            else if (number.Length != 8)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!(number[0] == '1' || number[0] == '7' || number[0] == '8' || number[0] == '9'))
            {
                return ValidationResult.Invalid("Invalid format. First digit must be 1 or 7 or 8 or 9");
            }
            else if ((int)char.GetNumericValue(number[number.Length - 1]) != CalculateChecksum(number))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }


        /// <summary>
        /// Isikukood (Personcal ID number)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(id, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }


            int calculatedCotrol = CalculateChecksum(id);

            return calculatedCotrol == (int)char.GetNumericValue(id[10]) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        private int CalculateChecksum(string id)
        {
            int[] multiplier_1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int[] multiplier_2 = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };

            int total = 0;

            for (int i = 0; i < id.Length - 1; i++)
            {
                total += int.Parse(id[i].ToString()) * multiplier_1[i];
            }
            int mod = total % 11;

            total = 0;
            if (10 == mod)
            {
                for (int i = 0; i < 10; i++)
                {
                    total += int.Parse(id[i].ToString()) * multiplier_2[i];
                }
                mod = total % 11;

                if (10 == mod)
                {
                    mod = 0;
                }
            }

            return mod;
        }

        /// <summary>
        /// Kaibemaksukohuslase (KMKR)  
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("ee", string.Empty).Replace("EE", string.Empty);
            if (!Regex.IsMatch(vatId, @"^10\d{7}$"))
            {
                return ValidationResult.Invalid("");
            }

            int[] Multipliers = { 3, 7, 1, 3, 7, 1, 3, 7 };

            var sum = vatId.Sum(Multipliers);

            var checkDigit = 10 - sum % 10;

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            return checkDigit == vatId[8].ToInt() ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }
    }
}
