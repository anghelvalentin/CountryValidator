using CountryValidator.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests
{
    public class SlovakiaValidatorTests
    {
        private readonly SlovakiaValidator _slovakiaValidator;

        public SlovakiaValidatorTests()
        {
            _slovakiaValidator = new SlovakiaValidator();
        }

        [Theory]
        [InlineData("7103192745", true)]
        [InlineData("991231123", false)]
        [InlineData("7103192746", false)]
        [InlineData("1103492745", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _slovakiaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("7103192745", true)]
        [InlineData("991231123", false)]
        [InlineData("7103192746", false)]
        [InlineData("1103492745", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _slovakiaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("2022749619", true)]
        [InlineData("SK 202 274 96 19", true)]
        [InlineData("2022749618", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _slovakiaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("2022749619", true)]
        [InlineData("SK 202 274 96 19", true)]
        [InlineData("2022749618", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _slovakiaValidator.ValidateVAT(code).IsValid);
        }

    }
}
