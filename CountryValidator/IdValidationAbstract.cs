
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryValidator
{
    public abstract class IdValidationAbstract
    {

        public static string CountryCode { get; protected set; }
        public virtual ValidationResult ValidateNationalIdentity(string ssn)
        {
            return ValidateIndividualTaxCode(ssn);
        }

        public abstract ValidationResult ValidateIndividualTaxCode(string id);
        public abstract ValidationResult ValidateEntity(string id);
        public abstract ValidationResult ValidateVAT(string vatId);
    }
}
