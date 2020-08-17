using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class LithuaniaValidator : IdValidationAbstract
    {
        public LithuaniaValidator()
        {
            CountryCode = nameof(Country.LT);
        }

        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();


            if (!Regex.IsMatch(ssn, @"^\d{11}$"))
            {
                return ValidationResult.InvalidFormat("012345678901");
            }

            int[] multiplier_1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int[] multiplier_2 = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };

            int control = int.Parse(ssn[10].ToString());
            int total = 0;

            for (int i = 0; i < 10; i++)
            {
                total += int.Parse(ssn[i].ToString()) * multiplier_1[i];
            }
            int mod = total % 11;

            total = 0;
            if (10 == mod)
            {
                for (int i = 0; i < 10; i++)
                {
                    total += int.Parse(ssn[i].ToString()) * multiplier_2[i];
                }
                mod = total % 11;

                if (10 == mod)
                {
                    mod = 0;
                }
            }

            return control == mod ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        /// <summary>
        /// PVM (Pridėtinės vertės mokestis mokėtojo kodas, Lithuanian VAT number).
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            if (!Regex.IsMatch(vat, @"^(\d{9}|\d{12})$"))
            {
                return ValidationResult.InvalidFormat("123456789 or 123456789012");
            }

            int[] Multipliers = { 3, 4, 5, 6, 7, 8, 9, 1 };

            if (vat.Length == 9)
            {
                if (vat[7] != '1')
                {
                    return ValidationResult.Invalid("Invalid format. The eight digit must be 1");
                }

                var sum = 0;
                for (var index = 0; index < 8; index++)
                {
                    sum += vat[index].ToInt() * (index + 1);
                }

                var checkDigit = sum % 11;
                if (checkDigit == 10)
                {
                    checkDigit = vat.Sum(Multipliers);
                }

                if (checkDigit == 10)
                {
                    checkDigit = 0;
                }

                var isValid = checkDigit == vat[8].ToInt();
                return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
            }

            return TemporarilyRegisteredTaxPayers(vat);

        }

        private ValidationResult TemporarilyRegisteredTaxPayers(string vat)
        {
            if (vat[10] != '1')
            {
                return ValidationResult.Invalid("Invalid format. The eleven digit must be 1");
            }

            int[] multipliersTemporarily = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            int[] multipliersDoubleCheck = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4 };


            var total = vat.Sum(multipliersTemporarily);

            if (total % 11 == 10)
            {
                total = vat.Sum(multipliersDoubleCheck);
            }

            total %= 11;
            if (total == 10)
            {
                total = 0;
            }

            var isValid = total == vat[11].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers().ToUpper().Replace("LT", string.Empty);
            if (!Regex.IsMatch(postalCode, "^\\d{5}$"))
            {
                return ValidationResult.InvalidFormat("LT-NNNNN");
            }
            return ValidationResult.Success();
        }
    }
}
