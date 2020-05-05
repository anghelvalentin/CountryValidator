using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class BulgariaValidatorTests
    {
        private readonly BulgariaValidator _bulgariaValidator;

        public BulgariaValidatorTests()
        {
            _bulgariaValidator = new BulgariaValidator();
        }

        [Theory]
        [InlineData("7523169263", true)]
        [InlineData("8032056031", true)]
        [InlineData("803205 603 1", true)]
        [InlineData("8001010008", true)]
        [InlineData("8019010008", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _bulgariaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("7523169263", true)]
        [InlineData("8032056031", true)]
        [InlineData("803205 603 1", true)]
        [InlineData("8001010008", true)]
        [InlineData("8019010008", true)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _bulgariaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("8001010008", true)]

        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _bulgariaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("BG 175 074 752", true)]
        [InlineData("175074752", true)]
        [InlineData("175074751", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _bulgariaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData(" 1000", true)]
        [InlineData("1700", true)]
        [InlineData("17001", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _bulgariaValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
