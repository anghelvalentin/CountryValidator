using CountryValidator.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests
{
    public class CyprusValidatorTests
    {
        private readonly CyprusValidator _cyprusValidator;

        public CyprusValidatorTests()
        {
            _cyprusValidator = new CyprusValidator();
        }

        [Theory]
        [InlineData("7103192745", true)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _cyprusValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("00123123T", true)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _cyprusValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("10259033P", true)]
        [InlineData("10259033Z", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _cyprusValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("10259033P", true)]
        [InlineData("CY-10259033P", true)]
        [InlineData("CY-10259033P ", true)]
        [InlineData("10259033Z", false)]
        [InlineData("CY-10259033Z", false)]

        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _cyprusValidator.ValidateVAT(code).IsValid);
        }

    }
}
