using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class TurkeyValidator : IdValidationAbstract
    {
        public TurkeyValidator()
        {
            CountryCode = nameof(Country.TR);
        }


        /// <summary>
        /// VKN (Vergi Kimlik Numarası, Turkish tax identification number).
        /// </summary>
        /// <param name="vkn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string vkn)
        {
            return ValidateVAT(vkn);
        }

        /// <summary>
        /// T.C. Kimlik No. (Turkish personal identification number)
        /// </summary>
        /// <param name="kimlik"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string kimlik)
        {
            if (!kimlik.All(char.IsDigit) || kimlik[0] == '0')
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            else if (kimlik.Length != 11)
            {
                return ValidationResult.InvalidLength();
            }
            else if (CalculatChecksumKimlik(kimlik.Substring(0, kimlik.Length - 2)) != kimlik.Substring(kimlik.Length - 2))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        private string CalculatChecksumKimlik(string number)
        {
            int check1 = 0;
            int check2 = 0;
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum = sum + (i % 2 == 0 ? 3 : 1) * (int)char.GetNumericValue(number[i]);
            }

            check1 = (10 - sum).Mod(10);

            sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum = sum + (int)char.GetNumericValue(number[i]);
            }
            check2 = (check1 + sum).Mod(10);

            return $"{check1}{check2}";
        }

        private int CalculateChecksum(string number)
        {
            int s = 0;
            char[] charArray = number.ToCharArray();
            Array.Reverse(charArray);
            number = new string(charArray);

            int c1 = 0;
            int c2 = 0;
            for (int i = 1; i <= number.Length; i++)
            {
                c1 = ((int)char.GetNumericValue(number[i - 1]) + i) % 10;
                if (c1 != 0)
                {
                    c2 = (c1 * (int)Math.Pow(2, i)) % 9;
                    if (c2 == 0)
                    {
                        c2 = 9;
                    }
                    s += c2;
                }

            }

            return (10 - s).Mod(10);
        }

        /// <summary>
        /// VKN (Vergi Kimlik Numarası, Turkish tax identification number).
        /// </summary>
        /// <param name="vkn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vkn)
        {
            vkn = vkn.RemoveSpecialCharacthers().ToUpper().Replace("TR", string.Empty);


            if (!vkn.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }
            else if (vkn.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            else if (CalculateChecksum(vkn.Substring(0, vkn.Length - 1)) != (int)char.GetNumericValue(vkn[vkn.Length - 1]))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
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
