using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class NetherlandsValidatorTests
    {
        private readonly NetherlandsValidator _netherlandsValidator;

        public NetherlandsValidatorTests()
        {
            _netherlandsValidator = new NetherlandsValidator();
        }

        [Theory]
        [InlineData("111222333", true)]
        [InlineData("941331490", true)]
        [InlineData("101222331", true)]
        [InlineData("9413.31.490", true)]
        [InlineData("941331491", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _netherlandsValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("111222333", true)]
        [InlineData("941331490", true)]
        [InlineData("101222331", false)]
        [InlineData("notanumber", false)]
        [InlineData("9413.31.490", true)]
        [InlineData("941331491", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _netherlandsValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("004495445B01", true)]
        [InlineData("123456789B90", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _netherlandsValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("004495445B01", true)]
        [InlineData("123456789B90", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _netherlandsValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("1234 AB", true)]
        [InlineData("2490 AA", true)]
        [InlineData("1321", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _netherlandsValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
