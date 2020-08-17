using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class IrelandValidator : IdValidationAbstract
    {

        public IrelandValidator()
        {
            CountryCode = nameof(Country.IE);
        }

        /// <summary>
        /// PPS No (Personal Public Service Number, Irish personal number).
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers().ToUpper();

            if (!Regex.IsMatch(id, @"^(\d{7})([A-Za-z]{1,2})$"))
            {
                return ValidationResult.InvalidFormat("1234567AA or 1234567A");
            }
            else if (id[id.Length - 1] == 'A')
            {
                return ValidationResult.Invalid("Invalid code. This is a personal code");
            }

            var numericPart = id.Substring(0, 7);
            string checksumCharacter = id.Substring(7);

            var multiplyingFactor = 8;
            var sum = 0;

            for (var i = 0; i < numericPart.Length; i++)
            {
                sum += int.Parse(numericPart[i].ToString()) * multiplyingFactor--;
            }

            if (checksumCharacter.Length > 1)
            {
                sum += (checksumCharacter[1].ToString().ToUpper()[0] - 64) * 9;
            }

            var checksum = sum % 23;
            return checksum + 64 == checksumCharacter[0] ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// PPS No (Personal Public Service Number, Irish personal number).
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers().ToUpper();

            if (!Regex.IsMatch(ssn, @"^(\d{7})([A-Za-z]{1,2})$"))
            {
                return ValidationResult.InvalidFormat("1234567AA or 1234567A");
            }
            else if (ssn[ssn.Length - 1] == 'H')
            {
                return ValidationResult.Invalid("Invalid code. This is a company code");
            }

            var numericPart = ssn.Substring(0, 7);
            string checksumCharacter = ssn.Substring(7);

            var multiplyingFactor = 8;
            var sum = 0;

            for (var i = 0; i < numericPart.Length; i++)
            {
                sum += int.Parse(numericPart[i].ToString()) * multiplyingFactor--;
            }

            if (checksumCharacter.Length > 1)
            {
                sum += (checksumCharacter[1].ToString().ToUpper()[0] - 64) * 9;
            }

            var checksum = sum % 23;
            return checksum + 64 == checksumCharacter[0] ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }


        public override ValidationResult ValidateVAT(string vatId)
        {
            int[] multipliers = { 8, 7, 6, 5, 4, 3, 2 };
            if (!Regex.IsMatch(vatId, @"^(\d{7}[A-W])|([7-9][A-Z\*\+)]\d{5}[A-W])|(\d{7}[A-W][AH])$"))
            {
                return ValidationResult.InvalidFormat("Invalid format");
            }

            if (Regex.IsMatch(vatId, @"^\d[A-Z\*\+]"))
            {
                vatId = "0" + vatId.Substring(2, 5)
                          + vatId.Substring(0, 1)
                          + vatId.Substring(7, 1);
            }

            var sum = vatId.Sum(multipliers);

            if (Regex.IsMatch(vatId, @"^\d{7}[A-Z][AH]$"))
            {
                if (vatId[8] == 'H')
                {
                    sum += 72;
                }
                else
                {
                    sum += 9;
                }
            }

            var checkDigit = sum % 23;

            bool isValid = vatId[7] == (checkDigit == 0 ? 'W' : (char)(checkDigit + 64));
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers().ToUpper();
            if (!Regex.IsMatch(postalCode, "^[\\dA-Z]{3}[\\dA-Z]{4}$"))
            {
                return ValidationResult.InvalidFormat("WDD WDWD");
            }
            return ValidationResult.Success();
        }
    }
}
