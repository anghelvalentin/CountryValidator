using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class RussiaValidator : IdValidationAbstract
    {

        public RussiaValidator()
        {
            CountryCode = nameof(Country.RU);
        }

        /// <summary>
        /// Validate Taxpayer Personal Identification Number (INN) 
        /// </summary>
        /// <param name="inn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string inn)
        {
            inn = inn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(inn, @"^\d{10}$") && !Regex.IsMatch(inn, @"^\d{12}$"))
            {
                return ValidationResult.InvalidFormat("123456789");
            }
            else if ((inn.Length == 10)
            && (int.Parse(inn[9].ToString()) ==
            ((2 * int.Parse(inn[0].ToString()) + 4 * int.Parse(inn[1].ToString()) + 10 * int.Parse(inn[2].ToString()) + 3 * int.Parse(inn[3].ToString()) + 5 * int.Parse(inn[4].ToString())
            + 9 * int.Parse(inn[5].ToString()) + 4 * int.Parse(inn[6].ToString()) + 6 * int.Parse(inn[7].ToString()) + 8 * int.Parse(inn[8].ToString())) % 11) % 10))
            {
                return ValidationResult.Success();
            }
            else if (inn.Length != 12)
            {
                return ValidationResult.Invalid("Invalid length");
            }

            int checkDigit10 = int.Parse(inn[10].ToString());
            int calculatedDigit10 = ((7 * int.Parse(inn[0].ToString())
                + 2 * int.Parse(inn[1].ToString())
                + 4 * int.Parse(inn[2].ToString())
                + 10 * int.Parse(inn[3].ToString())
                + 3 * int.Parse(inn[4].ToString())
                + 5 * int.Parse(inn[5].ToString())
                + 9 * int.Parse(inn[6].ToString())
                + 4 * int.Parse(inn[7].ToString())
                + 6 * int.Parse(inn[8].ToString())
                + 8 * int.Parse(inn[9].ToString())) % 11) % 10;

            int checkDigit11 = int.Parse(inn[11].ToString());
            int calculatedDigit11 = (
                (
                  3 * int.Parse(inn[0].ToString())
                + 7 * int.Parse(inn[1].ToString())
                + 2 * int.Parse(inn[2].ToString())
                + 4 * int.Parse(inn[3].ToString())
               + 10 * int.Parse(inn[4].ToString())
                + 3 * int.Parse(inn[5].ToString())
                + 5 * int.Parse(inn[6].ToString())
                + 9 * int.Parse(inn[7].ToString())
                + 4 * int.Parse(inn[8].ToString())
                + 6 * int.Parse(inn[9].ToString())
                + 8 * int.Parse(inn[10].ToString())) % 11) % 10;

            bool isValid = checkDigit10 == calculatedDigit10 && checkDigit11 == calculatedDigit11;

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Validate Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            if (id?.Length != 10)
            {
                return ValidationResult.Invalid("Invalid length");

            }
            return ValidateIndividualTaxCode(id);

        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId?.Replace("RU", string.Empty).Replace("ru", string.Empty);
            return ValidateIndividualTaxCode(vatId);
        }


        /// <summary>
        /// Validate SNILS
        /// </summary>
        /// <param name="snils"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string snils)
        {
            snils = snils.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(snils, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }
            var checkSum = int.Parse(snils.Substring(9));
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(snils[i].ToString()) * (9 - i);
            }

            bool isValid = (sum < 100 && sum == checkSum)
                || ((sum == 100 || sum == 101) && checkSum == 0)
                || (sum > 101 && (sum % 101 == checkSum || (sum % 101 == 100 && checkSum == 0)));

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }



        /// <summary>
        /// Valiudate Beneficiary’s Bank BIK code
        /// </summary>
        /// <param name="bik"></param>
        /// <returns></returns>
        public ValidationResult ValidateBIK(string bik)
        {
            bik = bik.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(bik, @"^\d{9}$"))
            {
                return ValidationResult.InvalidFormat("123456789");
            }

            var thirdPart = int.Parse(bik.Substring(6));
            if (thirdPart == 0 || thirdPart == 1 || thirdPart == 2)
            {
                return ValidationResult.Success();
            }
            bool isValid = thirdPart >= 50 && thirdPart < 1000;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Validate OGRN (Principle State Registration Number)
        /// </summary>
        /// <param name="ogrn"></param>
        /// <returns></returns>
        public ValidationResult ValidateOGRN(string ogrn)
        {
            ogrn = ogrn.RemoveSpecialCharacthers();
            if (!(Regex.IsMatch(ogrn, @"^\d{13}$")))
            {
                return ValidationResult.InvalidFormat("123456789");
            }
            long checkSUm = long.Parse(ogrn.Substring(0, ogrn.Length - 1)) % 11;

            bool isValid = checkSUm % 10 == Char.GetNumericValue(ogrn[12]);
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Validate OGRNIP - Primary State Registration Number of an Individual Entrepreneur
        /// </summary>
        /// <param name="ogrnip"></param>
        /// <returns></returns>
        public ValidationResult ValidateOGRNIP(string ogrnip)
        {
            ogrnip = ogrnip.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ogrnip, @"^\d{15}$"))
            {
                return ValidationResult.InvalidFormat("123456789012345");
            }
            ulong checksum = ulong.Parse(ogrnip.Substring(0, ogrnip.Length - 1)) % 13;
            bool isValid = checksum % 10 == ulong.Parse(ogrnip[14].ToString());
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{6}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
