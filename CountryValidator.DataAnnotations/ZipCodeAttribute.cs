using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CountryValidation.DataAnnotations
{
    /// <summary>
    /// When applied to a <see cref="string" /> property or parameter, validates that a valid Zip Code is provided.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public sealed class ZipCodeAttribute : ValidationAttribute
    {
        public ZipCodeAttribute(Country countryCode)
        {
            if (!Enum.IsDefined(typeof(Country), countryCode))
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

            if (!(value is string zip))
            {
                return base.IsValid(value, validationContext);
            }

            CountryValidator taxValidator = new CountryValidator();
            ValidationResult result = taxValidator.ValidateZIPCode(zip, CountryCode);
            if (result.IsValid)
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }

            validationContext.Items.Add("Error", result.ErrorMessage);

            IEnumerable<string> memberNames = null;
            if (validationContext.MemberName != null)
            {
                memberNames = new[] { validationContext.MemberName };
            }

            return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
        }

    }
}
