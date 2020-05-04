using CountryValidator.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests
{
    public class ItalyValidatorTests
    {
        private readonly ItalyValidator _italyValidator;
        public ItalyValidatorTests()
        {
            _italyValidator = new ItalyValidator();
        }

        [Theory]
        [InlineData("RCCMNL83S18D969H", true)]
        [InlineData("RCCMNL83S18D969", false)]
        [InlineData("00743110157", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _italyValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("RCCMNL83S18D969H", true)]
        [InlineData("RCCMNL83S18D969", false)]
        [InlineData("00743110157", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _italyValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("00743110157", true)]
        [InlineData("00743110158", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _italyValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("00743110157", true)]
        [InlineData("00743110158", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _italyValidator.ValidateVAT(code).IsValid);
        }

    }
}
