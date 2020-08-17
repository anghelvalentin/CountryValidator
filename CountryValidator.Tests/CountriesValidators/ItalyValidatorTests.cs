using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
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

        [Theory]
        [InlineData("00144", true)]
        [InlineData("39049", true)]
        [InlineData("123", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _italyValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
