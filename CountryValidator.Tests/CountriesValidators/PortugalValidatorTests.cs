using CountryValidator.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests
{
    public class PortugalValidatorTests
    {
        private readonly PortugalValidator _portugalValidator;

        public PortugalValidatorTests()
        {
            _portugalValidator = new PortugalValidator();
        }

        [Theory]
        [InlineData("900000007", true)]
        [InlineData("900000006", false)]
        [InlineData("000000000ZZ4", true)]
        [InlineData("000000000ZZ3", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _portugalValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("100000002", true)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _portugalValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("501964843", true)]
        [InlineData("401964843", false)]
        [InlineData("900000006", false)]
        [InlineData("501964842", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _portugalValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("501964843", true)]
        [InlineData("501964842", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _portugalValidator.ValidateVAT(code).IsValid);
        }

    }
}
