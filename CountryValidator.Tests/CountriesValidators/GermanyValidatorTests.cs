using CountryValidator.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests
{
    public class GermanyValidatorTests
    {
        private readonly GermanyValidator _germanyValidator;
        public GermanyValidatorTests()
        {
            _germanyValidator = new GermanyValidator();
        }

        [Theory]
        [InlineData("36 574 261 809 ", true)]
        [InlineData("36574261890", false)]
        [InlineData("36554266806", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _germanyValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("36 574 261 809 ", true)]
        [InlineData("36574261890", false)]
        [InlineData("36554266806", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _germanyValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("93815/08152", true)]
        [InlineData("2893081508152", true)]
        [InlineData("151/815/08156", true)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _germanyValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("DE 136,695 976", true)]
        [InlineData("DE136695976", true)]
        [InlineData("136695978", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _germanyValidator.ValidateVAT(code).IsValid);
        }

    }
}
