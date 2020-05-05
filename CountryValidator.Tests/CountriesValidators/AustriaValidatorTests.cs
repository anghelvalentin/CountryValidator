using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class AustriaValidatorTests
    {
        private readonly AustriaValidator _austriaValidator;
        public AustriaValidatorTests()
        {
            _austriaValidator = new AustriaValidator();
        }

        [Theory]
        [InlineData("1237 010180", true)]
        [InlineData("2237 010180", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _austriaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("59-119/9013", true)]
        [InlineData("a1-119/9013", false)]
        [InlineData("111-119/9013", false)]
        [InlineData("591199013", true)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _austriaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("FN 122119m", true)]
        [InlineData("122119m", true)]
        [InlineData("m123123", false)]
        [InlineData("abc1", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _austriaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("AT U13585627", true)]
        [InlineData("U13585626", false)]
        [InlineData("U13585627 ", true)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _austriaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("1010", true)]
        [InlineData("3741", true)]
        [InlineData("3741 ", true)]
        [InlineData("10101", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _austriaValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
