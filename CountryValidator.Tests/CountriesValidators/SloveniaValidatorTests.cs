using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class SloveniaValidatorTests
    {
        private readonly SloveniaValidator _sloveniaValidator;

        public SloveniaValidatorTests()
        {
            _sloveniaValidator = new SloveniaValidator();
        }

        [Theory]
        [InlineData("0101006500006", true)]
        [InlineData("0101006500007", false)]
        [InlineData("010100650000", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _sloveniaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("50223054", true)]
        [InlineData("50223055", false)]
        [InlineData("09999990", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _sloveniaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("50223054", true)]
        [InlineData("50223055", false)]
        [InlineData("09999990", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _sloveniaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("50223054", true)]
        [InlineData("50223055", false)]
        [InlineData("09999990", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _sloveniaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("4000", true)]
        [InlineData("2500", true)]
        [InlineData("211212", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _sloveniaValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
