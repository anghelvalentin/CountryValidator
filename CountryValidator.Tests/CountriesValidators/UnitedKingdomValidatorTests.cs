using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class UnitedKingdomValidatorTests
    {
        private readonly UnitedKingdomValidator _ukValidator;
        public UnitedKingdomValidatorTests()
        {
            _ukValidator = new UnitedKingdomValidator();
        }

        [Theory]
        [InlineData("9434765870", true)]
        [InlineData("9434765871", false)]
        [InlineData("943 476 5870", true)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _ukValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("AB123456C", true)]
        [InlineData("TN50X", false)]
        [InlineData("20267565392", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _ukValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("980780684", true)]
        [InlineData("802311781", false)]
        [InlineData("6640211", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _ukValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("980780684", true)]
        [InlineData("802311781", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _ukValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("EC1Y 8SY", true)]
        [InlineData("GIR0AA", true)]
        [InlineData("321311", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _ukValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
