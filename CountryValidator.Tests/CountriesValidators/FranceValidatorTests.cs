using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class FranceValidatorTests
    {
        private readonly FranceValidator _franceValidator;

        public FranceValidatorTests()
        {
            _franceValidator = new FranceValidator();
        }

        [Theory]
        [InlineData("295109912611193", true)]
        [InlineData("253072B07300470", true)]
        [InlineData("253072A07300443", true)]
        [InlineData("295109912611199", false)]
        [InlineData("253072C07300443", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _franceValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("0701987765432", true)]
        [InlineData("07 01 987 765 432", true)]
        [InlineData("070198776543", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _franceValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("552 008 443", true)]
        [InlineData("404833048", true)]
        [InlineData("404833047", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _franceValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("Fr 40 303 265 045", true)]
        [InlineData("23334175221", true)]
        [InlineData("K7399859412", true)]
        [InlineData("84 323 140 391", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _franceValidator.ValidateVAT(code).IsValid);
        }

    }
}
