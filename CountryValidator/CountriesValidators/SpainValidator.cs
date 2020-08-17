using System;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class SpainValidator : IdValidationAbstract
    {
        readonly string DNI_REGEX = @"^(([KLM]\d{7})|(\d{8}))([A-Z])$";
        readonly string CIF_REGEX = @"^([ABCDEFGHJNPQRSUVW])(\d{7})([0-9A-J])$";
        readonly string NIE_REGEX = @"^[XYZ]\d{7,8}[A-Z]$";

        public SpainValidator()
        {
            CountryCode = nameof(Country.ES);
        }

        public override ValidationResult ValidateIndividualTaxCode(string ssn)
        {
            return ValidateSpanishID(ssn);
        }

        public ValidationResult ValidateSpanishID(string str)
        {
            str = str.RemoveSpecialCharacthers();
            ValidationResult validationResult = ValidationResult.Invalid("Invalid");
            var type = SpainIdType(str);

            switch (type)
            {
                case "dni":
                    validationResult = ValidDNI(str);
                    break;
                case "nie":
                    validationResult = ValidNIE(str);
                    break;
                case "cif":
                    validationResult = ValidCIF(str);
                    break;
            }
            return validationResult;
        }

        private string SpainIdType(string str)
        {
            if (Regex.IsMatch(str, DNI_REGEX))
            {
                return "dni";
            }
            if (Regex.IsMatch(str, CIF_REGEX))
            {
                return "cif";
            }
            if (Regex.IsMatch(str, NIE_REGEX))
            {
                return "nie";
            }
            return "";
        }

        private ValidationResult ValidDNI(string dni)
        {
            var dni_letters = "TRWAGMYFPDXBNJZSQVHLCKE";
            if (Regex.IsMatch(dni[0].ToString(), "[KLM]"))
            {
                dni = dni.Substring(1);
            }
            string dniNumber = Regex.Match(dni, @"\d+").Value;
            var letter = dni_letters[(int.Parse(dniNumber) % 23)];

            bool isValid = letter == dni[dni.Length - 1];
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        private ValidationResult ValidNIE(string nie)
        {

            var nie_prefix = nie[0];

            int nie_prefix_number;

            switch (nie_prefix)
            {
                case 'X': nie_prefix_number = 0; break;
                case 'Y': nie_prefix_number = 1; break;
                case 'Z': nie_prefix_number = 2; break;
                default:
                    return ValidationResult.Invalid("Invalid");
            }

            return ValidDNI(nie_prefix_number + nie.Substring(1));

        }

        private ValidationResult ValidCIF(string cif)
        {
            string letter = cif.Substring(0, 1);
            string number = cif.Substring(1, 7);
            string control = cif.Substring(8);
            var even_sum = 0;
            var odd_sum = 0;
            int n;

            for (var i = 0; i < number.Length; i++)
            {
                n = int.Parse(number[i].ToString());

                if (i % 2 == 0)
                {
                    n *= 2;

                    odd_sum += n < 10 ? n : n - 9;

                }
                else
                {
                    even_sum += n;
                }

            }

            string text = (even_sum + odd_sum).ToString();
            int last_digit = int.Parse(text.Substring(text.Length - 1));

            var control_digit = last_digit != 0 ? (10 - last_digit) : last_digit;
            var control_letter = "JABCDEFGHI".Substring(control_digit, 1);

            bool isValid = false;

            if (Regex.IsMatch(letter, "[ABEH]"))
            {
                isValid = control == control_digit.ToString();

            }
            else if (Regex.IsMatch(letter, "[PQSW]"))
            {
                isValid = control == control_letter;

            }
            else
            {
                isValid = control == control_digit.ToString() || control == control_letter;
            }
            return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }



        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();
            var total = 0;
            var multipliers = new int[] { 2, 1, 2, 1, 2, 1, 2 };

            int temp;
            if (Regex.IsMatch(id, @"^[A-H|J|U|V]\d{8}$"))
            {

                for (int i = 0; i < 7; i++)
                {
                    temp = int.Parse(id[i + 1].ToString()) * multipliers[i];
                    if (temp > 9)
                    {
                        total += (int)Math.Floor((decimal)temp / 10) + temp % 10;
                    }
                    else
                    {
                        total += temp;
                    }
                }
                total = 10 - total % 10;
                if (total == 10) { total = 0; }

                if (total == int.Parse(id.Substring(8, 1)))
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            else if (Regex.IsMatch(id, @"^[A-H|N-S|W]\d{7}[A-J]$"))
            {
                for (int i = 0; i < 7; i++)
                {
                    temp = int.Parse(id[i + 1].ToString()) * multipliers[i];
                    if (temp > 9)
                    {
                        total += (int)Math.Floor((decimal)temp / 10) + temp % 10;
                    }
                    else
                    {
                        total += temp;
                    }
                }

                total = 10 - total % 10;
                char totalChar = (char)(total + 64);

                if (totalChar == id[8])
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            return ValidationResult.Invalid("Invalid");
        }

        public override ValidationResult ValidateVAT(string vatId)
        {
            vatId = vatId.RemoveSpecialCharacthers();

            ValidationResult result = ValidateEntity(vatId);
            if (result.IsValid)
            {
                return ValidationResult.Success();
            }
            else if (Regex.IsMatch(vatId, @"^[0-9|Y|Z]\d{7}[A-Z]$"))
            {
                var tempnumber = vatId;
                if (tempnumber[0] == 'Y')
                {
                    tempnumber = tempnumber.Replace("Y", "1");
                }
                else if (tempnumber[0] == 'Z')
                {
                    tempnumber = tempnumber.Replace("Z", "2");
                }

                bool isValid = tempnumber[8] == "TRWAGMYFPDXBNJZSQVHLCKE"[int.Parse(tempnumber.Substring(0, 8)) % 23];
                return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
            }
            else if (Regex.IsMatch(vatId, @"^[K|L|M|X]\d{7}[A-Z]$"))
            {
                bool isValid = vatId[8] == "TRWAGMYFPDXBNJZSQVHLCKE"[int.Parse(vatId.Substring(1, 7)) % 23];
                return isValid ? ValidationResult.Success() : ValidationResult.InvalidChecksum();

            }

            return ValidationResult.Invalid("Invalid");
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
