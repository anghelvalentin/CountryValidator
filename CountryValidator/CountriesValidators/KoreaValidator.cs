using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class KoreaValidator : IdValidationAbstract
    {
        public KoreaValidator()
        {
            CountryCode = nameof(Country.KR);
        }
        public override ValidationResult ValidateEntity(string id)
        {
            throw new NotSupportedException();

        }

        /// <summary>
        /// Validate  Resident Registration Number (RRN)
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            ssn = ssn.RemoveSpecialCharacthers();

            if (!Regex.IsMatch(ssn, "^[0-9]{13}$"))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }

            string dateString; DateTime maxDate;
            string sDigit; // parse the date into 'YYYYMMDD' according to 'S' digit

            sDigit = ssn.Substring(6, 1);
            string yearPrefix;
            switch (sDigit)
            {
                case "1":
                case "2":
                case "5":
                case "6":
                    yearPrefix = "19";
                    break;

                case "3":
                case "4":
                case "7":
                case "8":
                    yearPrefix = "20";
                    break;

                default:
                    yearPrefix = "18";
                    break;
            }


            dateString = yearPrefix + ssn.Substring(0, 6);
            maxDate = new DateTime(DateTime.Now.Year - 17, DateTime.Now.Month, DateTime.Now.Day);
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datetime))
            {
                if (datetime > maxDate)
                {
                    return ValidationResult.InvalidDate();
                }
                else if (datetime < new DateTime(1860, 1, 1))
                {
                    return ValidationResult.InvalidDate();
                }
            }
            else
            {
                return ValidationResult.InvalidDate();
            }

            string char6; int index;
            int remainder; int weightedSum;

            int[] weight = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 0 }; // add 0 for check digit

            weightedSum = 0;
            index = 0;

            for (int i = 0, len = ssn.Length; i < len; i++)
            {
                char6 = ssn[i].ToString();
                weightedSum += int.Parse(char6) * weight[index];
                index++;
            }

            remainder = (11 - weightedSum % 11) % 10 - +int.Parse(ssn.Substring(12));
            var isValid = remainder == 0;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
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
