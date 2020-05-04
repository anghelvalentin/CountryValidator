using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class DenmarkValidatorTests
    {
        private readonly DenmarkValidator _denmarkValidator;

        public DenmarkValidatorTests()
        {
            _denmarkValidator = new DenmarkValidator();
        }

        [Theory]
        [InlineData("2110625629", true)]
        [InlineData("211062-5629", true)]
        [InlineData("511062-5629", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _denmarkValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("2110625629", true)]
        [InlineData("211062-5629", true)]
        [InlineData("511062-5629", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _denmarkValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("13585628", true)]
        [InlineData("13585627", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _denmarkValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("13585628", true)]
        [InlineData("13585627", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _denmarkValidator.ValidateVAT(code).IsValid);
        }

    }
}
