using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class ChinaValidator : IdValidationAbstract
    {
        static readonly Dictionary<char, int> charToNumDict = new Dictionary<char, int>();
        static ChinaValidator()
        {
            charToNumDict.Add('0', 0);
            charToNumDict.Add('1', 1);
            charToNumDict.Add('2', 2);
            charToNumDict.Add('3', 3);
            charToNumDict.Add('4', 4);
            charToNumDict.Add('5', 5);
            charToNumDict.Add('6', 6);
            charToNumDict.Add('7', 7);
            charToNumDict.Add('8', 8);
            charToNumDict.Add('9', 9);
            charToNumDict.Add('A', 10);
            charToNumDict.Add('B', 11);
            charToNumDict.Add('C', 12);
            charToNumDict.Add('D', 13);
            charToNumDict.Add('E', 14);
            charToNumDict.Add('F', 15);
            charToNumDict.Add('G', 16);
            charToNumDict.Add('H', 17);
            charToNumDict.Add('J', 18);
            charToNumDict.Add('K', 19);
            charToNumDict.Add('L', 20);
            charToNumDict.Add('M', 21);
            charToNumDict.Add('N', 22);
            charToNumDict.Add('P', 23);
            charToNumDict.Add('Q', 24);
            charToNumDict.Add('R', 25);
            charToNumDict.Add('T', 26);
            charToNumDict.Add('U', 27);
            charToNumDict.Add('W', 28);
            charToNumDict.Add('X', 29);
            charToNumDict.Add('Y', 30);
        }

        private int GetNationalIDWeight(int n)
        {
            return (int)Math.Pow(2, n - 1) % 11;
        }

        public override ValidationResult ValidateIndividualTaxCode(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (id.Length != 18&&id.Length!=15)
            {
                return ValidationResult.Invalid("Invalid length! The code must have a length of 18 or 15");
            }


            if (id.Length == 18)
            {
                if (!Regex.IsMatch(id, "^[0-9]{17}[0-9X]$"))
                {
                    return ValidationResult.InvalidFormat("123456YYYYMMDD123X where YYYYMMDD - date of birth, X - checksum");
                }

                string dateString = id.Substring(6, 8);
                try
                {
                    DateTime data = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (!(data < DateTime.Now && data.Year > 1886))
                    {
                        return ValidationResult.InvalidDate();
                    }
                }
                catch
                {
                    return ValidationResult.InvalidDate();
                }

                string _char;
                int checkDigit;
                string identifier;
                int remainder;

                identifier = id.Substring(0, 17);
                checkDigit = int.Parse(id.Substring(17) == "X" ? "10" : id.Substring(17));


                int weightedSum = 0;
                int index = id.Length;

                for (int i = 0, len = identifier.Length; i < len; i++)
                {
                    _char = identifier[i].ToString();
                    weightedSum += int.Parse(_char) * GetNationalIDWeight(index);
                    index--;
                }

                remainder = (12 - weightedSum % 11) % 11 - checkDigit;
                bool isValid = remainder == 0;

                return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
            }
            else
            {
                //15 digits
                if (!Regex.IsMatch(id, "^[0-9]{15}"))
                {
                    return ValidationResult.InvalidFormat("123456YYMMDD123 where YYMMDD - date of birth");
                }
                string dateString = "19"+id.Substring(6, 6);    //people born after 2000 doesn't have 15 digits id
                try
                {
                    DateTime data = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (data > DateTime.Now)
                    {
                        return ValidationResult.InvalidDate();
                    }
                }
                catch
                {
                    return ValidationResult.InvalidDate();
                }
                return ValidationResult.Success();
            }
        }

        static readonly int[] EntityCodeWeightingFactors = { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };
        /// <summary>
        /// This method is used for Entity validation only
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private int CharToNum(char c)
        {
            if (charToNumDict.ContainsKey(c))
                return charToNumDict[c];
            return -1;
        }
        /// <summary>
        /// Validate Uniform Social Credit Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (id.Length != 18)
            {
                return ValidationResult.Invalid("Invalid length! The code must have a length of 18");
            }
            if (!Regex.IsMatch(id,"^[159Y]{1}[1239]{1}[0-9]{6}[^_IOZSVa-z\\W]{10}$"))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            int tempSum = 0;

            for (var i = 0; i <17; i++)
            {
                var t = CharToNum(id[i]);
                if (t == -1)
                    return ValidationResult.InvalidFormat(string.Format("There is a invalid character '{0}' at position {1}",id[i],i+1));

                tempSum += EntityCodeWeightingFactors[i] * t;
            }
            var checksum = 31 - tempSum % 31;
            if (checksum == 31)
                checksum = 0;
            if (checksum == this.CharToNum(id[17]))
                return ValidationResult.Success();
            else
                return ValidationResult.InvalidChecksum();
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            throw new NotSupportedException();
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
