using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class LithuaniaValidatorTests
    {
        private readonly LithuaniaValidator _lithuaniaValidator;

        public LithuaniaValidatorTests()
        {
            _lithuaniaValidator = new LithuaniaValidator();
        }

        [Theory]
        [InlineData("38703181745", true)]
        [InlineData("38703181746", false)]
        [InlineData("78703181745", false)]
        [InlineData("38703421745", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _lithuaniaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("38703181745", true)]
        [InlineData("38703181746", false)]
        [InlineData("78703181745", false)]
        [InlineData("38703421745", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _lithuaniaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("119511515", true)]
        [InlineData("100001919017", true)]
        [InlineData("100001919018", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _lithuaniaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("119511515", true)]
        [InlineData("100001919017", true)]
        [InlineData("100001919018", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _lithuaniaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("03500", true)]
        [InlineData("04340", true)]
        [InlineData("2113", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _lithuaniaValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
