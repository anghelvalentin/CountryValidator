using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CountryValidation.Countries
{
    public class MalaysiaValidator : IdValidationAbstract
    {
        public MalaysiaValidator()
        {
            CountryCode = nameof(Country.MY);
        }

        /// <summary>
        ///   Nombor Cukai Pendapatan (ITN)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ValidationResult ValidateEntity(string id)
        {
            id = id.RemoveSpecialCharacthers();

            if (Regex.IsMatch(id, @"^(CS|D|E|F|FA|PT|TA|TC|TN|TR|TP|TJ|LE)\d{10}$"))
            {
                return ValidationResult.Invalid("Invalid code!");
            }
            return ValidationResult.Success();

        }


        /// <summary>
        /// Nombor Cukai Pendapatan (ITN)  
        /// </summary>
        /// <param name="itn"></param>
        /// <returns></returns>
        public override ValidationResult ValidateIndividualTaxCode(string itn)
        {
            itn = itn.RemoveSpecialCharacthers();

            if (Regex.IsMatch(itn, @"^(SG|OG)\d{10}[01]$"))
            {
                return ValidationResult.Invalid("Invalid code!");
            }
            return ValidationResult.Success();

        }

        /// <summary>
        ///  Nombor Cukai Pendapatan (ITN)
        /// </summary>
        /// <param name="vatId"></param>
        /// <returns></returns>
        public override ValidationResult ValidateVAT(string vatId)
        {
            return ValidateEntity(vatId);
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
