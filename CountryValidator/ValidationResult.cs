using System;
using System.Collections.Generic;
using System.Text;

namespace CountryValidator
{
    public sealed class ValidationResult
    {

        public static ValidationResult Success()
        {
            return new ValidationResult() { IsValid = true };
        }

        public static ValidationResult Invalid(string errorMessage)
        {
            return new ValidationResult() { Error = errorMessage, IsValid = false };
        }

        public static ValidationResult InvalidChecksum()
        {
            return new ValidationResult() { Error = $"Invalid checksum.", IsValid = false };
        }
        public static ValidationResult InvalidFormat(string format)
        {
            return new ValidationResult() { Error = $"Invalid format. The code must have this format {format}", IsValid = false };
        }
        public static ValidationResult InvalidDate()
        {
            return new ValidationResult() { Error = $"Invalid date", IsValid = false };
        }

        public static ValidationResult InvalidLength()
        {
            return new ValidationResult() { Error = $"Invalid length", IsValid = false };
        }

        public bool IsValid { get; private set; }

        public string Error { get; private set; }
    }
}
