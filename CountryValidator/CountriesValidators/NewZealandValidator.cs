using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class NewZealandValidator : IdValidationAbstract
    {
        public NewZealandValidator()
        {
            CountryCode = nameof(Country.NZ);
        }


        /// <summary>
        /// IRD number (New Zealand Inland Revenue Department (Te Tari Tāke) number).
        /// </summary>
        /// <param name="ird"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string ird)
        {
            ird = ird.RemoveSpecialCharacthers();
            ird = ird.Replace("NZ", string.Empty).Replace("nz", string.Empty);
            if (!(ird.Length != 8 || ird.Length != 9))
            {
                return ValidationResult.InvalidLength();
            }
            else if (!ird.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678 or 123456789");
            }
            else if (!(10000000 < long.Parse(ird) && long.Parse(ird) < 150000000))
            {
                return ValidationResult.Invalid("Invalid code");
            }
            else if (Char.GetNumericValue(ird[ird.Length - 1]) != CalculateChecksum(ird.Substring(0, ird.Length - 1)))
            {
                return ValidationResult.InvalidChecksum();
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// IRD number (New Zealand Inland Revenue Department (Te Tari Tāke) number).
        /// </summary>
        /// <param name="ird"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ird)
        {
            return ValidateEntity(ird);
        }

        private int CalculateChecksum(string number)
        {
            int[] primary_weights = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
            int[] secondary_weights = new int[] { 7, 4, 3, 2, 5, 2, 7, 6 };

            number = number.PadLeft(8, '0');
            int s = 0;
            for (int i = 0; i < number.Length; i++)
            {
                s = s + (primary_weights[i] * (int)char.GetNumericValue(number[i]));
            }

            s = Mod(-s, 11);


            if (s != 10)
            {
                return s;
            }

            s = 0;
            for (int i = 0; i < number.Length; i++)
            {
                s = s + (secondary_weights[i] * (int)char.GetNumericValue(number[i]));
            }
            s = Mod(-s, 11);

            return s;
        }


        public override ValidationResult ValidateVAT(string number)
        {
            return ValidateEntity(number);
        }

        private int Mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
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
