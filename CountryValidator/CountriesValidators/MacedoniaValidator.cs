using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MacedoniaValidator : IdValidationAbstract
    {
        public MacedoniaValidator()
        {
            CountryCode = nameof(Country.MK);
        }

        public override ValidationResult ValidateNationalIdentity(string value)
        {
            value = value.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(value, @"^\d{13}$"))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }


            try
            {
                int day = int.Parse(value.Substring(0, 2));
                int month = int.Parse(value.Substring(2, 2));
                int year = int.Parse(value.Substring(4, 3));


                if (year >= 800)
                {
                    year = 1000 + year;
                }
                else
                {
                    year = 2000 + year;
                }
                DateTime date = new DateTime(year, month, day);
            }
            catch
            {
                return ValidationResult.InvalidDate();
            }


            int rr = int.Parse(value.Substring(7, 2));
            int k = int.Parse(value.Substring(12, 1));

            // Validate checksum
            var sum = 0;
            for (var i = 0; i < 6; i++)
            {
                sum += (7 - i) * ((int)char.GetNumericValue(value[i]) + (int)char.GetNumericValue(value[i + 6]));
            }
            sum = 11 - sum % 11;
            if (sum == 10 || sum == 11)
            {
                sum = 0;
            }
            if (sum != k)
            {
                return ValidationResult.InvalidChecksum();
            }

            // Validate political region
            // rr is the political region of birth, which can be in ranges:
            // 10-19: Bosnia and Herzegovina
            // 20-29: Montenegro
            // 30-39: Croatia (not used anymore)
            // 41-49: Macedonia
            // 50-59: Slovenia (only 50 is used)
            // 70-79: Central Serbia
            // 80-89: Serbian province of Vojvodina
            // 90-99: Kosovo

            return 41 <= rr && rr <= 49 ? ValidationResult.Success() : ValidationResult.Invalid("Invalid Region. Macedonia region is between 41-49");
        }


        public override ValidationResult ValidateEntity(string id)
        {
            return ValidateVAT(id);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            throw new NotImplementedException();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            vatId = vatId.Replace("MK", string.Empty).Replace("mk", string.Empty);
            if (!Regex.IsMatch(vatId, @"^\d{13}$"))
            {
                return ValidationResult.InvalidFormat("MK1234567890123");
            }
            return ValidationResult.Success();
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
