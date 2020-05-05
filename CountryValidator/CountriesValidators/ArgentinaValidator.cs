
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CountryValidation.Countries
{
    public class ArgentinaValidator : IdValidationAbstract
    {
        public ArgentinaValidator()
        {
            CountryCode = nameof(Country.AR);
        }

        /// <summary>
        /// Validate CUIT Number - Código Único de Identificación Tributaria
        /// </summary>
        /// <param name="cuit"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string cuit)
        {
            return ValidateCuit(cuit);
        }


        /// <summary>
        /// Validate CUIT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateCuit(id);

        }

        /// <summary>
        /// Validate VAT/IVA
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateCuit(vatId);
        }



        private ValidationResult ValidateCuit(string cuit)
        {

            cuit = cuit.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(cuit, @"^\d{11}$"))
            {
                return ValidationResult.Invalid("12345678901");
            }
            else
            {
                int calculado = CalculateDigitCuit(cuit);
                int digito = int.Parse(cuit.Substring(10));
                bool isValid = calculado == digito;
                return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
            }
        }



        /// <summary>
        /// Validate DNI number (SSN for Argentina)
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string dni)
        {
            dni = dni.RemoveSpecialCharacthers();
            if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !dni.All(Char.IsNumber))
            {
                return ValidationResult.InvalidFormat("12345678");
            }
            else if (!Int32.TryParse(dni, out int dniNumber))
            {
                return ValidationResult.InvalidFormat("12345678");
            }
            else
            {
                if (dniNumber >= 10000000 && dniNumber <= 99999999)
                {
                    return ValidationResult.Success();
                }
                return ValidationResult.Invalid("Invalid");
            }
        }

        private int CalculateDigitCuit(string cuit)
        {
            int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++)
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }
            var resto = total % 11;
            return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
        }


        /// <summary>
        /// Validate Argentina CBU (Bank Account)
        /// </summary>
        /// <param name="cbu"></param>
        /// <returns></returns>
        public ValidationResult ValidateCBU(string cbu)
        {
            cbu = cbu.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(cbu, "^[0-9]{22}$"))
            {
                return ValidationResult.InvalidFormat("1234567890123456789012");
            }

            string getChecksumDigit(string value)
            {
                int[] ponderador = new int[] { 3, 1, 7, 9 };
                var sum = 0;
                int j = 0;
                for (int i = value.Length - 1; i >= 0; --i)
                {
                    sum += (int.Parse(value[i].ToString()) * ponderador[j % 4]);
                    ++j;
                }

                return ((10 - sum % 10) % 10).ToString();
            }


            if (cbu[7].ToString() != getChecksumDigit(cbu.Substring(0, 7)))
            {
                return ValidationResult.InvalidChecksum();
            }
            if (cbu[21].ToString() != getChecksumDigit(cbu.Substring(8, 13)))
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{4}|[A-Za-z]\\d{4}[a-zA-Z]{3}$"))
            {
                return ValidationResult.InvalidFormat("NNNN OR ANNNNAAA");
            }
            return ValidationResult.Success();
        }
    }
}
