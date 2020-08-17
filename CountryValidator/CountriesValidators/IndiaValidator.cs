using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class IndiaValidator : IdValidationAbstract
    {

        public IndiaValidator()
        {
            CountryCode = nameof(Country.IN);
        }

        readonly int[,] d = new int[,]
        {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
            {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
            {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
            {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
            {5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
            {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
            {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
            {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}
        };

        readonly int[,] p = new int[,]
       {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 5, 7, 6, 2, 8, 3, 0, 9, 4},
            {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
            {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
            {9, 4, 5, 3, 1, 2, 6, 8, 7, 0},
            {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
            {2, 7, 9, 3, 8, 0, 6, 4, 1, 5},
            {7, 0, 4, 6, 9, 1, 3, 2, 5, 8}
       };

        int[] inv = { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

        /// <summary>
        /// Aadhaar
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string num)
        {
            num = num.RemoveSpecialCharacthers();
            int c = 0;
            int[] myArray = StringToReversedIntArray(num);

            for (int i = 0; i < myArray.Length; i++)
            {
                c = d[c, p[(i % 8), myArray[i]]];
            }

            bool isValid = c == 0;
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        private int[] StringToReversedIntArray(string num)
        {
            int[] myArray = new int[num.Length];

            for (int i = 0; i < num.Length; i++)
            {
                myArray[i] = int.Parse(num.Substring(i, 1));
            }

            Array.Reverse(myArray);

            return myArray;

        }

        /// <summary>
        /// PAN (Permanent Account Number, Indian income tax identifier)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            if (id.Length != 10)
            {
                return ValidationResult.InvalidLength();
            }
            else if (!Regex.IsMatch(id, "^[A-Z]{5}[0-9]{4}[A-Z]$"))
            {
                return ValidationResult.InvalidFormat("AAAAA1234A");
            }
            else if (!HasValidCardType(id))
            {
                return ValidationResult.Invalid("Invalid card type");
            }
            return ValidationResult.Success();
        }


        private bool HasValidCardType(string number)
        {

            string[] card_holder_types = new string[]{
                "A",// 'Association of Persons (AOP)'
                "B",//Body of Individuals (BOI)'
                "C",//Company'
                "F",//Firm'
                "G",//Government'
                "H",//HUF (Hindu Undivided Family)'
                "L",//Local Authority'
                "J",//Artificial Juridical Person'
                "P",//Individual
                "T",//Trust (AOP)
                "K",//Krish (Trust Krish)
            };


            return card_holder_types.Contains(number.Substring(3, 1));
        }


        /// <summary>
        /// VAT TIN / CST TIN  
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(vatId, @"^\d{11}[CcVv]$"))
            {
                return ValidationResult.InvalidFormat("12345678901C");
            }
            return ValidationResult.Success();
        }


        /// <summary>
        /// PAN (Permanent Account Number, Indian income tax identifier)
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string pan)
        {
            return ValidateEntity(pan);
        }

        public override ValidationResult ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.RemoveSpecialCharacthers();
            if (!Regex.IsMatch(postalCode, "^\\d{6}$"))
            {
                return ValidationResult.InvalidFormat("NNNNNN or NNN NNN");
            }
            return ValidationResult.Success();
        }
    }
}
