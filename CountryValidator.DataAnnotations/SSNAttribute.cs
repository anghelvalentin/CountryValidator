using CountryValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CountryValidator.DataAnnotations
{
    /// <summary>
    /// When applied to a <see cref="string" /> property or parameter, validates that a valid National Identification Number is provided.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class SSNAttribute : ValidationAttribute
    {
        public SSNAttribute(Country countryCode)
        {
            if (Enum.IsDefined(typeof(Country), countryCode))
            {
                throw new ArgumentNullException(nameof(countryCode));
            }

            CountryCode = countryCode;
        }

        public Country CountryCode { get; set; }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }

            if (!(value is string ssn))
            {
                return base.IsValid(value, validationContext);
            }

            TaxValidator taxValidator = new TaxValidator();
            ValidationResult result = taxValidator.ValidateNationalIdentityCode(ssn, CountryCode);
            if (result.IsValid)
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }

            validationContext.Items.Add("Error", result.Error);

            IEnumerable<string> memberNames = null;
            if (validationContext.MemberName != null)
            {
                memberNames = new[] { validationContext.MemberName };
            }

            return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
        }

    }
}
