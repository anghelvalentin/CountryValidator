using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class LatviaValidatorTests
    {
        private readonly LatviaValidator _latviaValidator;

        public LatviaValidatorTests()
        {
            _latviaValidator = new LatviaValidator();
        }

        [Theory]
        [InlineData("161175-19997", true)]
        [InlineData("16117519997", true)]
        [InlineData("161375-19997", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _latviaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("161175-19997", true)]
        [InlineData("16117519997", true)]
        [InlineData("161375-19997", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _latviaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("40003521600", true)]
        [InlineData("16117519997", true)]
        [InlineData("40003521601", false)]
        [InlineData("161375197", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _latviaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("40003521600", true)]
        [InlineData("16117519997", true)]
        [InlineData("40003521601", false)]
        [InlineData("167519997", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _latviaValidator.ValidateVAT(code).IsValid);
        }

    }
}
