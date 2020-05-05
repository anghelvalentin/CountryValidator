using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class PolandValidatorTests
    {
        private readonly PolandValidator _polandValidator;

        public PolandValidatorTests()
        {
            _polandValidator = new PolandValidator();
        }

        [Theory]
        [InlineData("83010411457", true)]
        [InlineData("87123116221", true)]
        [InlineData("39100413824", false)]
        [InlineData("36032806768", false)]
        [InlineData("04271113861", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _polandValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("83010411457", true)]
        [InlineData("87123116221", true)]
        [InlineData("39100413824", false)]
        [InlineData("36032806768", false)]
        [InlineData("04271113861", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _polandValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("192598184", true)]
        [InlineData("123456785", true)]
        [InlineData("12345678512347", true)]
        [InlineData("12345678512348", false)]
        [InlineData("192598183", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _polandValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("8567346215", true)]
        [InlineData("8567346216", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _polandValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("00-950", true)]
        [InlineData("05470", true)]
        [InlineData("213213", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _polandValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
