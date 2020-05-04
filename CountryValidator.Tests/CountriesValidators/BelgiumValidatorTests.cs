using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class BelgiumValidatorTests
    {
        private readonly BelgiumValidator _belgiumValidator;

        public BelgiumValidatorTests()
        {
            _belgiumValidator = new BelgiumValidator();
        }

        [Theory]
        [InlineData("12060105317", true)]
        [InlineData("36574261890", false)]
        [InlineData("36554266806", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _belgiumValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("12060105317", true)]
        [InlineData("36574261890", false)]
        [InlineData("36554266806", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _belgiumValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("0428759497", true)]
        [InlineData("BE403019261", true)]
        [InlineData("431150351", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _belgiumValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("0428759497", true)]
        [InlineData("BE403019261", true)]
        [InlineData("BE 428759497", true)]
        [InlineData("431150351", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _belgiumValidator.ValidateVAT(code).IsValid);
        }

    }
}
