using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class CzechValidatorTests
    {
        private readonly CzechValidator _czechValidator;

        public CzechValidatorTests()
        {
            _czechValidator = new CzechValidator();
        }

        [Theory]
        [InlineData("7103192745", true)]
        [InlineData("991231123", false)]
        [InlineData("1103492745", false)]
        [InlineData("590312123", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _czechValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("7103192745", true)]
        [InlineData("991231123", false)]
        [InlineData("1103492745", false)]
        [InlineData("590312123", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _czechValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("25123891", true)]
        [InlineData("7103192745", true)]
        [InlineData("CZ 25123891", true)]
        [InlineData("640903926", true)]
        [InlineData("590312123", true)]
        [InlineData("25123890", false)]
        [InlineData("1103492745", false)]
        [InlineData("991231123", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _czechValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("25123891", true)]
        [InlineData("7103192745", true)]
        [InlineData("CZ 25123891", true)]
        [InlineData("640903926", true)]
        [InlineData("590312123", true)]
        [InlineData("25123890", false)]
        [InlineData("1103492745", false)]
        [InlineData("991231123", false)]

        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _czechValidator.ValidateVAT(code).IsValid);
        }

    }
}
