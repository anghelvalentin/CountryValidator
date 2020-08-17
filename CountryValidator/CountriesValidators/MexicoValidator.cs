using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MexicoValidator : IdValidationAbstract
    {
        private static int DigitVerification(string curp17)
        {
            var dictionary = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            var sum = 0;

            for (var i = 0; i < 17; i++)
            {
                sum = sum + dictionary.IndexOf(curp17[i]) * (18 - i);
            }

            int checkDigit = 10 - sum % 10;
            if (checkDigit == 10)
                return 0;

            return checkDigit;
        }


        /// <summary>
        /// Mexico Unique Population Registry Code - CURP
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            var match = Regex.Match(ssn, @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$");

            if (!match.Success)
            {
                return ValidationResult.Invalid("Invalid format");
            }

            if (match.Groups[2].Value != DigitVerification(match.Groups[1].Value).ToString())
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// RFC (Registro Federal de Contribuyentes, Mexican tax number)
        /// </summary>
        /// <param name="rfc"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string rfc)
        {
            rfc = rfc.RemoveSpecialCharacthers();

            if (rfc.Length == 12) //# number assigned to company
            {
                if (!Regex.IsMatch(rfc, "^[A-Z&Ñ]{3}[0-9]{6}[0-9A-Z]{3}$"))
                {
                    return ValidationResult.Invalid("Invalid format");
                }
                else if (!HasValidDate(rfc))
                {
                    return ValidationResult.InvalidDate();
                }
            }
            else
            {
                return ValidationResult.InvalidLength();
            }

            if (rfc.Length >= 12)
            {
                if (!Regex.IsMatch(rfc.Substring(rfc.Length - 3), @"^[1-9A-V][1-9A-Z][0-9A]$"))
                {
                    return ValidationResult.Invalid("Invalid");
                }
                else if (rfc[rfc.Length - 1] != CalculateChecksum(rfc.Substring(0, rfc.Length - 1)))
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            return ValidationResult.Success();
        }

        private char CalculateChecksum(string number)
        {
            string alphabet = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ";
            number = ("   " + number);
            number = number.Substring(number.Length - 12);


            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum += alphabet.IndexOf(number[i]) * (13 - i);
            }

            return alphabet[(11 - sum).Mod(11)];
        }
        private bool HasValidDate(string number)
        {
            try
            {
                int year = int.Parse(number.Substring(0, 2));
                int month = int.Parse(number.Substring(2, 2));
                int day = int.Parse(number.Substring(4, 2));
                DateTime date = new DateTime(1900 + year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// RFC (Registro Federal de Contribuyentes, Mexican tax number)
        /// </summary>
        /// <param name="rfc"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string rfc)
        {
            string[] name_blacklist = new string[] {
                "BUEI", "BUEY", "CACA", "CACO", "CAGA", "CAGO", "CAKA", "CAKO", "COGE",
                "COJA", "COJE", "COJI", "COJO", "CULO", "FETO", "GUEY", "JOTO", "KACA",
                "KACO", "KAGA", "KAGO", "KAKA", "KOGE", "KOJO", "KULO", "MAME", "MAMO",
                "MEAR", "MEAS", "MEON", "MION", "MOCO", "MULA", "PEDA", "PEDO", "PENE",
                "PUTA", "PUTO", "QULO", "RATA", "RUIN"
            };

            rfc = rfc.RemoveSpecialCharacthers();



            if (rfc.Length == 10 || rfc.Length == 13)//# number assigned to person
            {
                if (!Regex.IsMatch(rfc, @"^[A-Z&Ñ]{4}[0-9]{6}[0-9A-Z]{0,3}$"))
                {
                    return ValidationResult.Invalid("Invalid format");
                }
                else if (name_blacklist.Contains(rfc.Substring(0, 4)))
                {
                    return ValidationResult.Invalid("Name is blacklisted");
                }
                else if (!HasValidDate(rfc.Substring(4, 6)))
                {
                    return ValidationResult.InvalidDate();
                }
            }
            else
            {
                return ValidationResult.InvalidLength();
            }

            if (rfc.Length >= 12)
            {
                if (!Regex.IsMatch(rfc.Substring(rfc.Length - 3), @"^[1-9A-V][1-9A-Z][0-9A]$"))
                {
                    return ValidationResult.Invalid("Invalid");
                }
                else if (rfc[rfc.Length - 1] != CalculateChecksum(rfc.Substring(0, rfc.Length - 1)))
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
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
