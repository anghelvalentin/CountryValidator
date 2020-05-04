using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidator.Countries
{
    public class ItalyValidator : IdValidationAbstract
    {
        private static readonly string Months = "ABCDEHLMPRST";
        private static readonly string Vocals = "AEIOU";
        private static readonly string Consonants = "BCDFGHJKLMNPQRSTVWXYZ";
        private static readonly string OmocodeChars = "LMNPQRSTUV";
        private static readonly int[] ControlCodeArray = new[] { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };
        private static readonly Regex CheckRegex = new Regex(@"^[A-Z]{6}[\d]{2}[A-Z][\d]{2}[A-Z][\d]{3}[A-Z]$");

        public ItalyValidator()
        {
            CountryCode = nameof(Country.IT);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (string.IsNullOrEmpty(ssn) || ssn.Length < 16)
            {
                return ValidationResult.Invalid("Invalid length. The code must have 16 characters");
            }

            ssn = Normalize(ssn, false);
            if (!CheckRegex.Match(ssn).Success)
            {
                string nonOmocodeFC = ReplaceOmocodeChars(ssn);
                if (!CheckRegex.Match(nonOmocodeFC).Success)
                {
                    return ValidationResult.Invalid("");
                }
            }
            bool isValid = ssn[15] == GetControlChar(ssn.Substring(0, 15));
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        private string ReplaceOmocodeChars(string fc)
        {
            char[] fcChars = fc.ToCharArray();
            int[] pos = new[] { 6, 7, 9, 10, 12, 13, 14 };
            foreach (int i in pos) if (!Char.IsNumber(fcChars[i])) fcChars[i] = OmocodeChars.IndexOf(fcChars[i]).ToString()[0];
            return new string(fcChars);
        }

        private char GetControlChar(string f15)
        {
            int tot = 0;
            byte[] arrCode = Encoding.ASCII.GetBytes(f15.ToUpper());
            for (int i = 0; i < f15.Length; i++)
            {
                if ((i + 1) % 2 == 0) tot += (char.IsLetter(f15, i))
                    ? arrCode[i] - (byte)'A'
                    : arrCode[i] - (byte)'0';
                else tot += (char.IsLetter(f15, i))
                    ? ControlCodeArray[(arrCode[i] - (byte)'A')]
                    : ControlCodeArray[(arrCode[i] - (byte)'0')];
            }
            tot %= 26;
            char l = (char)(tot + 'A');
            return l;
        }

        private string Normalize(string s, bool normalizeDiacritics)
        {
            if (String.IsNullOrEmpty(s)) return s;
            s = s.Trim().ToUpper();
            if (normalizeDiacritics)
            {
                string src = "ÀÈÉÌÒÙàèéìòù";
                string rep = "AEEIOUAEEIOU";
                for (int i = 0; i < src.Length; i++) s = s.Replace(src[i], rep[i]);
                return s;
            }
            return s;
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        /// <summary>
        /// VAT - Partita IVA  
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("IT", string.Empty).Replace("it", string.Empty);

            if (!Regex.IsMatch(vat, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }

            int[] Multipliers = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            if (int.TryParse(vat.Substring(0, 7), out int res))
            {
                if (res == 0)
                {
                    return ValidationResult.Invalid("Invalid format");
                }
            }
            else
            {
                return ValidationResult.Invalid("Invalid format");
            }

            var temp = int.Parse(vat.Substring(7, 3));

            if ((temp < 1 || temp > 201) && temp != 999 && temp != 888)
            {
                return ValidationResult.Invalid("Invalid");
            }

            var index = 0;
            var sum = 0;
            foreach (var m in Multipliers)
            {
                temp = vat[index++].ToInt() * m;
                sum += temp > 9
                    ? (int)Math.Floor(temp / 10D) + temp % 10
                    : temp;
            }

            var checkDigit = 10 - sum % 10;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            var isValid = checkDigit == vat[10].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }
    }
}
