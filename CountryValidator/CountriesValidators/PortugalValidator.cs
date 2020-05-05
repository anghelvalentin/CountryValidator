using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class PortugalValidator : IdValidationAbstract
    {
        public PortugalValidator()
        {
            CountryCode = nameof(Country.PT);
        }

        Dictionary<char, int> chars = new Dictionary<char, int>{
            { '0', 0},
            {'1', 1},
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'A', 10},
            {'B', 11},
            {'C', 12},
            {'D', 13},
            {'E', 14},
            {'F', 15},
            {'G', 16},
            {'H', 17},
            {'I', 18},
            {'J', 19},
            {'K', 20},
            {'L', 21},
            {'M', 22},
            {'N', 23},
            {'O', 24},
            {'P', 25},
            {'Q', 26},
            {'R', 27},
            {'S', 28},
            {'T', 29},
            {'U', 30},
            {'V', 31},
            {'W', 32},
            {'X', 33},
            {'Y', 34},
            {'Z', 35}
        };

        /// <summary>
        /// Número de identificação civil - NIC
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ValidationResult validation = ValidateCartaoCidadao(ssn);
            if (validation.IsValid)
            {
                return validation;
            }
            else
            {
                return ValidateBilhetedeIdentidade(ssn);
            }
        }

        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            id = id.Replace("PT", string.Empty).Replace("pt", string.Empty);
            int[] multipliers = { 9, 8, 7, 6, 5, 4, 3, 2 };

            if (!Regex.IsMatch(id, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("123456789");

            }
            else if (Regex.IsMatch(id, "^[123]"))
            {
                return ValidationResult.Invalid("Invalid code. This is not a company nif.");
            }

            var sum = id.Sum(multipliers);

            var checkDigit = 11 - sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }
            bool isValid = checkDigit == id[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidateIndividualTaxCode(string code)
        {
            code = code.RemoveSpecialCharacthers();
            code = code.Replace("PT", string.Empty).Replace("pt", string.Empty);
            int[] multipliers = { 9, 8, 7, 6, 5, 4, 3, 2 };

            if (!Regex.IsMatch(code, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("123456789");

            }
            else if (Regex.IsMatch(code, "^[5]"))
            {
                return ValidationResult.Invalid("Invalid code. This is not a personal nif.");
            }

            var sum = code.Sum(multipliers);

            var checkDigit = 11 - sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }
            bool isValid = checkDigit == code[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public int CheckSum(string value)
        {
            var sum = 0;

            for (var i = 0; i < value.Length; i++)
            {
                sum += value[i] * (value.Length + 1 - i);
            }

            var mod = sum % 11;
            return ((mod == 0 || mod == 1) ? 0 : 11 - mod);
        }

        /// <summary>
        /// Bilhete de Identidade
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValidationResult ValidateBilhetedeIdentidade(string value)
        {
            value = value.RemoveSpecialCharacthers();

            if (value?.Length != 9)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!value.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("123456789");
            }


            return CheckSum(value.Substring(0, 8)) == (int)char.GetNumericValue(value[8])
                ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Cartao do Cidadao
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValidationResult ValidateCartaoCidadao(string value)
        {
            value = value.RemoveSpecialCharacthers();
            if (value?.Length != 12)
            {
                return ValidationResult.InvalidLength();
            }


            return CalculateSum(value) == 0 ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        private int CalculateSum(string value)
        {
            var sum = 0;

            for (var i = value.Length - 1; i >= 0; i--)
            {
                int d = -1;
                try
                {
                    d = chars[value[i]];
                }
                catch
                {
                    return -1;
                }
                if (i < 9 && d > 9)
                {
                    return -1;
                }

                if (i % 2 == 0)
                {
                    d *= 2;

                    if (d > 9)
                    {
                        d -= 9;
                    }
                }

                sum += d;
            }

            return sum % 10;
        }

        /// <summary>
        /// Numero de Identificacao Fiscal (NIF) 
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("PT", string.Empty).Replace("pt", string.Empty);
            int[] multipliers = { 9, 8, 7, 6, 5, 4, 3, 2 };

            if (!Regex.IsMatch(vat, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("123456789");

            }
            else if (Regex.IsMatch(vat, "^[123]"))
            {
                return ValidationResult.Invalid("Invalid code. This is not a company nif.");
            }

            var sum = vat.Sum(multipliers);

            var checkDigit = 11 - sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }
            bool isValid = checkDigit == vat[8].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{7}$"))
            {
                return ValidationResult.InvalidFormat("NNNN-NNN");
            }
            return ValidationResult.Success();
        }
    }
}
