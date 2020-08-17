using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class GreeceValidatorTests
    {
        private readonly GreeceValidator _greeceValidator;
        public GreeceValidatorTests()
        {
            _greeceValidator = new GreeceValidator();
        }

        [Theory]
        [InlineData("01013099997", true)]
        [InlineData("01013099999", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("023456780", true)]
        [InlineData("094259216", true)]
        [InlineData("123456781", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]

        [InlineData("023456780", true)]
        [InlineData("094259216", true)]
        [InlineData("123456781", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("023456780", true)]
        [InlineData("094259216", true)]
        [InlineData("123456781", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("151 24", true)]
        [InlineData("151-10", true)]
        [InlineData("151", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _greeceValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
