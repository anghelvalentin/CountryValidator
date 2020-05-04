using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class FranceValidator : IdValidationAbstract
    {
        string _alphabet = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        public FranceValidator()
        {
            CountryCode = nameof(Country.FR);
        }


        /// <summary>
        /// SIREN (a French company identification number)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string number)
        {
            number = number.RemoveSpecialCharacthers().ToUpper().Replace("FR", string.Empty);
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("123456789");
            }
            else if (number.Length != 9)
            {
                return ValidationResult.InvalidLength();
            }
            return number.CheckLuhnDigit() ? ValidationResult.Success() : ValidationResult.InvalidChecksum();
        }

        /// <summary>
        /// NIF (Numéro d'Immatriculation Fiscale, French tax identification number).
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string number)
        {
            number = number.RemoveSpecialCharacthers().ToUpper().Replace("FR", string.Empty);
            if (!number.All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("1234567890123");
            }
            else if (number.Length != 13)
            {
                return ValidationResult.InvalidLength();
            }
            return ValidationResult.Success();
        }

        /// <summary>
        /// NIR (French personal identification number).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ValidationResult ValidateNationalIdentity(string value)
        {
            value = value.RemoveSpecialCharacthers();
            if (value.Length != 15)
            {
                return ValidationResult.Invalid("Invalid length");
            }
            var pattern = @"^([1278])(\d{2})(0[1-9]|1[0-2]|20)(\d{2}|2[AB])(\d{3})(\d{3})(\d{2})$";
            var match = Regex.Match(value, pattern);
            if (!match.Success)
            {
                return ValidationResult.Invalid("Invalid format");
            }

            string gender = match.Groups[1].Value;
            string year = match.Groups[2].Value;
            string month = match.Groups[3].Value;
            string department = match.Groups[4].Value;
            string city = match.Groups[5].Value;
            int cityNumber = int.Parse(city);

            var certificate = match.Groups[6].Value;
            var key = match.Groups[7].Value;
            int keyNumber = int.Parse(key);

            if (certificate == "000" || int.Parse(key) * 1 > 97)
            {
                return ValidationResult.Invalid("Invalid certificate");
            }

            if (department == "2A")
            {
                department = "19";
            }
            else if (department == "2B")
            {
                department = "18";
            }
            else if (department == "97")
            {
                department += city[0];
                if (int.Parse(department) < 970 || int.Parse(department) >= 989)
                {
                    return ValidationResult.Invalid("Invalid department");
                }

                city = city.Substring(1);
                cityNumber = int.Parse(city);

                if (cityNumber < 1 || cityNumber > 90)
                {
                    return ValidationResult.Invalid("Invalid city");
                }
            }
            else if (cityNumber < 1 || cityNumber > 990)
            {
                return ValidationResult.Invalid("Invalid city");
            }

            string insee = $"{gender}{year}{month}{department.Replace("A", "0").Replace("B", "0")}{city}{certificate}";
            long inseeNumber = long.Parse(insee);

            if ((97 - (inseeNumber) % 97) != keyNumber)
            {
                return ValidationResult.InvalidChecksum();
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Taxe sur la Valeur Ajoutee (TVA)  
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string number)
        {
            number = number.RemoveSpecialCharacthers().ToUpper().Replace("FR", string.Empty);
            if (!(_alphabet.IndexOf(number[0]) != -1 || _alphabet.IndexOf(number[0]) != -1))
            {
                return ValidationResult.Invalid("Invalid format");
            }
            else if (!number.Substring(2).All(char.IsDigit))
            {
                return ValidationResult.InvalidFormat("A1234567890");
            }
            else if (number.Length != 11)
            {
                return ValidationResult.InvalidLength();
            }
            else if (number.Substring(2, 3) != "000")
            {
                return ValidateEntity(number.Substring(2));
            }
            else if (!number.All(char.IsDigit))
            {

                if (int.Parse(number.Substring(0, 2)) != (long.Parse(number.Substring(2) + "12") % 97))
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            else
            {
                int check = 0;
                if (char.IsDigit(number[0]))
                {
                    check = (
                        _alphabet.IndexOf(number[0]) * 24 +
                        _alphabet.IndexOf(number[1]) - 10);
                }
                else
                {
                    check = (
                        _alphabet.IndexOf(number[0]) * 34 +
                        _alphabet.IndexOf(number[1]) - 100);
                    if ((long.Parse(number.Substring(2)) + 1 + check / 11) % 11 != (check % 11))
                    {
                        return ValidationResult.InvalidChecksum();
                    }
                }
            }
            return ValidationResult.Success();



        }
    }
}
