using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class PolandValidator : IdValidationAbstract
    {

        public PolandValidator()
        {
            CountryCode = nameof(Country.PL);
        }

        /// <summary>
        /// Polish National Identification Number (PESEL)
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string pesel)
        {
            List<int> peselList;
            int peselMonth, peselDay, peselYear, peselChecksum;

            if (pesel.Length != 11 || !pesel.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("12345678901");
            }

            peselList = pesel.ToCharArray().Select(n => (int)char.GetNumericValue(n)).ToList();
            peselMonth = int.Parse(pesel.Substring(2, 2));
            peselDay = int.Parse(pesel.Substring(4, 2));
            peselYear = ComputePESELYear(Int32.Parse(pesel.Substring(0, 2)), peselMonth);
            peselChecksum = int.Parse(pesel.Substring(10, 1));

            if (!CheckIfMonthIsCorrect(peselMonth))
            {
                return ValidationResult.InvalidDate();
            }
            else if (!CheckIfDayIsCorrect(peselDay, peselMonth, peselYear))
            {
                return ValidationResult.InvalidDate();
            }
            else if (!peselChecksum.Equals(ComputePESELChecksum(peselList)))
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        private int CalculateChecksum(string number)
        {
            int[] weights = null;
            if (number.Length == 8)
            {
                weights = new int[] { 8, 9, 2, 3, 4, 5, 6, 7 };
            }
            else
            {
                weights = new int[] { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };
            }
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum += weights[i] * (int)char.GetNumericValue(number[i]);
            }

            return sum.Mod(11).Mod(10);
        }

        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers();
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("Invalid code. Only numbers are allowed");
            }
            else if (!(number.Length == 9 || number.Length == 14))
            {
                return ValidationResult.InvalidLength();
            }
            else if ((int)char.GetNumericValue(number[number.Length - 1]) != CalculateChecksum(number.Substring(0, number.Length - 1)))
            {
                return ValidationResult.InvalidChecksum();
            }
            else if (number.Length == 14 && (int)char.GetNumericValue(number[8]) != CalculateChecksum(number.Substring(0, 8)))
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Numer Identyfikacji Podatkowej (NIP)  
        /// </summary>
        /// <param name="vat"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vat)
        {
            vat = vat.RemoveSpecialCharacthers();
            vat = vat.Replace("PL", string.Empty).Replace("pl", string.Empty);

            if (!Regex.IsMatch(vat, @"^\d{10}$"))
            {
                return ValidationResult.InvalidFormat("1234567890");
            }

            int[] multipliers = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };

            var sum = vat.Sum(multipliers);

            var checkDigit = sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == vat[9].ToInt();
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

        }

        private int ComputePESELYear(int PESEL2DigitsYear, int PESELMonth)
        {
            if (PESELMonth > 0 && PESELMonth < 13)
            {
                return 1900 + PESEL2DigitsYear;
            }
            else if (PESELMonth > 20 && PESELMonth < 33)
            {
                return 2000 + PESEL2DigitsYear;
            }
            else if (PESELMonth > 40 && PESELMonth < 53)
            {
                return 2100 + PESEL2DigitsYear;
            }
            else if (PESELMonth > 60 && PESELMonth < 73)
            {
                return 2200 + PESEL2DigitsYear;
            }

            return 1800 + PESEL2DigitsYear;
        }

        private bool CheckIfMonthIsCorrect(int PESELMonth)
        {
            if (PESELMonth % 20 == 0)
            {
                return false;
            }

            for (int i = 0; i <= 80; i += 20)
            {
                for (int j = 13 + i; j <= 19 + i; j++)
                {
                    if (PESELMonth == j)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckIfDayIsCorrect(int PESELDay, int PESELMonth, int PESELYear)
        {
            if (PESELDay == 0 || PESELDay > 31)
            {
                return false;
            }

            for (int i = 0; i <= 80; i += 20)
            {
                for (int j = 1 + i; j <= 7 + i ? j <= 7 + i : j <= 12 + i; j += 2)
                {
                    if (PESELMonth == j && PESELDay > 31)
                    {
                        return false;
                    }
                }

                for (int j = 4 + i; j <= 6 + i; j += 2)
                {
                    if (PESELMonth == j && PESELDay > 30)
                    {
                        return false;
                    }
                }

                for (int j = 9 + i; j <= 11 + i; j += 2)
                {
                    if (PESELMonth == j && PESELDay > 30)
                    {
                        return false;
                    }
                }
            }

            if (PESELYear == 2000 || (PESELYear % 4 == 0 && PESELYear % 100 != 0))
            {
                if (PESELMonth % 20 == 2 && PESELDay > 29)
                {
                    return false;
                }
            }
            else
            {
                if (PESELMonth % 20 == 2 && PESELDay > 28)
                {
                    return false;
                }
            }

            return true;
        }

        private int ComputePESELChecksum(List<int> PESELList)
        {
            int sum = PESELList[0] * 9 + PESELList[1] * 7 + PESELList[2] * 3 + PESELList[3] * 1 + PESELList[4] * 9 +
                      PESELList[5] * 7 + PESELList[6] * 3 + PESELList[7] * 1 + PESELList[8] * 9 + PESELList[9] * 7;

            return sum % 10;
        }
    }
}
