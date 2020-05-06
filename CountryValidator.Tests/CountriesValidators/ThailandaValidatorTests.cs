using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class ThailandaValidatorTests
    {
        private readonly ThailandValidator _thailandaValidator;

        public ThailandaValidatorTests()
        {
            _thailandaValidator = new ThailandValidator();
        }

        [Theory]
        [InlineData("0105-515-004-336", true)]
        [InlineData("0107537001510", true)]
        [InlineData("0107537001706", true)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _thailandaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("8112289874", true)]
        [InlineData("811228-9874", true)]
        [InlineData("811228+9874", true)]
        [InlineData("811228-9873", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _thailandaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("1234567897", true)]
        [InlineData("123456-7897", true)]
        [InlineData("1234567891", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _thailandaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("123456789701", true)]
        [InlineData("123456789101", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _thailandaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("11455", true)]
        [InlineData("21321", true)]
        [InlineData("321", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _thailandaValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
