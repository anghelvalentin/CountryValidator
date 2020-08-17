using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class MaltaValidatorTests
    {
        private readonly MaltaValidator _maltaValidator;

        public MaltaValidatorTests()
        {
            _maltaValidator = new MaltaValidator();
        }

        [Theory]
        [InlineData("1234567M", true)]
        [InlineData("1893120105733", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _maltaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("1234567M", true)]
        [InlineData("12345678", true)]
        [InlineData("1893120105733", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _maltaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("12345678", true)]
        [InlineData("1234567M", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _maltaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("11679112", true)]
        [InlineData("11679113", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _maltaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("NXR 01", true)]
        [InlineData("ZTN 05", true)]
        [InlineData("VCT 17531", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _maltaValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
