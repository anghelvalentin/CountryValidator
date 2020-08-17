using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class LuxembourgValidatorTests
    {
        private readonly LuxembourgValidator _luxembourgValidator;

        public LuxembourgValidatorTests()
        {
            _luxembourgValidator = new LuxembourgValidator();
        }

        [Theory]
        [InlineData("1893120105732", true)]
        [InlineData("1893120105733", false)]
        [InlineData("18931201057321", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("1893120105732", true)]
        [InlineData("1893120105733", false)]
        [InlineData("18931201057321", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("15027442", true)]
        [InlineData("15027443", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("15027442", true)]
        [InlineData("15027443", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("4750", true)]
        [InlineData("2998", true)]
        [InlineData("a1", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
