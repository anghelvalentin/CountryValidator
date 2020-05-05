using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class FinlandValidatorTests
    {
        private readonly FinlandValidator _finlandValidator;

        public FinlandValidatorTests()
        {
            _finlandValidator = new FinlandValidator();
        }

        [Theory]
        [InlineData("311280-888Y", true)]
        [InlineData("131052-308T", true)]
        [InlineData("131052-308U", false)]
        [InlineData("310252-308Y", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _finlandValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("311280-888Y", true)]
        [InlineData("131052-308T", true)]
        [InlineData("131052-308U", false)]
        [InlineData("310252-308Y", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _finlandValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("2077474-0", true)]
        [InlineData("2077474-1", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _finlandValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("20774740", true)]
        [InlineData("20774741", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _finlandValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("22150", true)]
        [InlineData("22430", true)]
        [InlineData("2133213", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _finlandValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
