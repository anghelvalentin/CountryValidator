using CountryValidator.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests
{
    public class GreeceValidatorTests
    {
        private readonly GreeceValidator _greeceValidator;
        public GreeceValidatorTests()
        {
            _greeceValidator = new GreeceValidator();
        }

        [Theory]
        [InlineData("01013099997", true)]
        [InlineData("01013099999", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("023456780", true)]
        [InlineData("094259216", true)]
        [InlineData("123456781", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]

        [InlineData("023456780", true)]
        [InlineData("094259216", true)]
        [InlineData("123456781", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("023456780", true)]
        [InlineData("094259216", true)]
        [InlineData("123456781", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateVAT(code).IsValid);
        }
    }
}
