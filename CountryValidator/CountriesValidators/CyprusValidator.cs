using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class CyprusValidator : IdValidationAbstract
    {
        public CyprusValidator()
        {
            CountryCode = nameof(Country.CY);
        }

        public override ValidationResult ValidateNationalIdentity(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(ssn, @"^\d{10}$"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }
            return ValidationResult.Success();
        }

        public override ValidationResult ValidateEntity(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat?.Replace("cy", string.Empty)?.Replace("CY", string.Empty);

            if (!Regex.IsMatch(vat, @"^([0-59]\d{7}[A-Z])$"))
            {
                return ValidationResult.InvalidFormat("12345678X");
            }

            var result = 0;
            for (var index = 0; index < 8; index++)
            {
                var temp = vat[index].ToInt();

                if (index % 2 == 0)
                {
                    switch (temp)
                    {
                        case 0:
                            temp = 1;
                            break;
                        case 1:
                            temp = 0;
                            break;
                        case 2:
                            temp = 5;
                            break;
                        case 3:
                            temp = 7;
                            break;
                        case 4:
                            temp = 9;
                            break;
                        default:
                            temp = temp * 2 + 3;
                            break;
                    }
                }
                result += temp;
            }

            var checkDigit = result % 26;
            bool isValid = vat[8] == (char)(checkDigit + 65);
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidateIndividualTaxCode(string vat)
        {
            return ValidateEntity(vat);
        }

        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("CY", string.Empty).Replace("cy", string.Empty);

            if (!Regex.IsMatch(vat, @"^([0-59]\d{7}[A-Z])$"))
            {
                return ValidationResult.InvalidFormat("12345678X");
            }

            return ValidateEntity(vat);
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
