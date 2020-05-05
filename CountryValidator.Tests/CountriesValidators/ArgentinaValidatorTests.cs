using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class ArgentinaValidatorTests
    {
        private readonly ArgentinaValidator _argentinaValidator;
        public ArgentinaValidatorTests()
        {
            _argentinaValidator = new ArgentinaValidator();
        }

        [Theory]
        [InlineData("20.123.456 ", true)]
        [InlineData("20123456", true)]
        [InlineData("2012345699", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _argentinaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("20-05536168-2", true)]
        [InlineData("20267565393", true)]
        [InlineData("20267565392", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _argentinaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("20267565393", true)]
        [InlineData("20055361682", true)]
        [InlineData("2026756A393", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _argentinaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("20267565393", true)]
        [InlineData("20055361682", true)]
        [InlineData("2026756A393", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _argentinaValidator.ValidateVAT(code).IsValid);
        }

    }
}
