using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class SwedenValidatorTests
    {
        private readonly SwedenValidator _swedenValidator;

        public SwedenValidatorTests()
        {
            _swedenValidator = new SwedenValidator();
        }

        [Theory]
        [InlineData("8112289874", true)]
        [InlineData("811228-9874", true)]
        [InlineData("811228+9874", true)]
        [InlineData("811228-9873", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _swedenValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("8112289874", true)]
        [InlineData("811228-9874", true)]
        [InlineData("811228+9874", true)]
        [InlineData("811228-9873", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _swedenValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("1234567897", true)]
        [InlineData("123456-7897", true)]
        [InlineData("1234567891", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _swedenValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("123456789701", true)]
        [InlineData("123456789101", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _swedenValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("11455", true)]
        [InlineData("21321", true)]
        [InlineData("321", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _swedenValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
