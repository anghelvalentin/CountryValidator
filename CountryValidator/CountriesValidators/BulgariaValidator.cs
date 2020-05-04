using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class BulgariaValidator : IdValidationAbstract
    {
        private static readonly Regex RegexPhysicalPerson = new Regex(@"^\d\d[0-5]\d[0-3]\d\d{4}$");
        private static readonly int[] _multipliersPhysicalPerson = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
        private static readonly int[] MultipliersForeignPhysicalPerson = { 21, 19, 17, 13, 11, 9, 7, 3, 1 };
        private static readonly int[] MultipliersMiscellaneous = { 4, 3, 2, 7, 6, 5, 4, 3, 2 };


        public BulgariaValidator()
        {
            CountryCode = nameof(Country.BG);
        }
        public override ValidationResult ValidateEntity(string vat)
        {
            bool isValid;
            if (vat.Length == 9)
            {
                return Bg9DigitsVat(vat) ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
            }

            if (BgPhysicalPerson(vat))
            {
                return ValidationResult.Success();
            }

            isValid = BgForeignerPhysicalPerson(vat) || BgMiscellaneousVatNumber(vat);

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        private bool HasValidDate(string number)
        {
            var year = int.Parse(number.Substring(0, 2)) + 1900;
            var month = int.Parse(number.Substring(2, 2));
            var day = int.Parse(number.Substring(4, 2));
            if (month > 40)
            {
                year += 100;
                month -= 40;
            }
            else if (month > 20)
            {
                year -= 100;
                month -= 20;
            }

            try
            {
                DateTime date = new DateTime(year, month, day);

                return date < DateTime.Now;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Edinen grazhdanski nomer (EGN)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            return BgPhysicalPerson(number) ? ValidationResult.Success() : ValidationResult.Invalid("Invalid code");
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (BgPhysicalPerson(ssn))
            {
                return ValidationResult.Success();
            }

            var isValid = BgForeignerPhysicalPerson(ssn) || BgMiscellaneousVatNumber(ssn);

            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// Bulgarian VAT Number (VAT) (Идентификационен номер по ДДС)
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("BG", string.Empty).Replace("bg", string.Empty);
            if (!Regex.IsMatch(vat, @"^\d{9,10}$"))
            {
                return ValidationResult.InvalidFormat("123456789, 1234567890");
            }
            return ValidateEntity(vat);
        }

        private static bool Bg9DigitsVat(string vat)
        {
            var total = 0;
            var temp = 0;
            for (var index = 0; index < 8; index++)
            {
                temp += vat[index].ToInt() * (index + 1);
            }

            total = temp % 11;

            if (total != 10)
            {
                return total == vat[8].ToInt();
            }

            temp = 0;
            for (var index = 0; index < 8; index++)
            {
                temp += vat[index].ToInt() * (index + 3);
            }

            total = temp % 11;

            if (total == 10)
            {
                total = 0;
            }

            return total == vat[8].ToInt();
        }

        private static bool BgPhysicalPerson(string vat)
        {
            if (!RegexPhysicalPerson.IsMatch(vat))
            {
                return false;
            }

            var month = int.Parse(vat.Substring(2, 2));

            if ((month <= 0 || month >= 13) && (month <= 20 || month >= 33) && (month <= 40 || month >= 53))
            {
                return false;
            }

            var total = vat.Sum(_multipliersPhysicalPerson);

            total %= 11;

            if (total == 10)
            {
                total = 0;
            }

            return total == vat[9].ToInt();
        }

        public static bool BgForeignerPhysicalPerson(string vat)
        {
            var total = vat.Sum(MultipliersForeignPhysicalPerson);

            return total % 10 == vat[9].ToInt();
        }

        public static bool BgMiscellaneousVatNumber(string vat)
        {
            var total = vat.Sum(MultipliersMiscellaneous);

            total = 11 - total % 11;

            if (total == 10)
            {
                return false;
            }

            if (total == 11)
            {
                total = 0;
            }

            return total == vat[9].ToInt();
        }
    }
}
