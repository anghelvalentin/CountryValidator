using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class HungaryValidatorTests
    {
        private readonly HungaryValidator _hungaryValidator;

        public HungaryValidatorTests()
        {
            _hungaryValidator = new HungaryValidator();
        }

        [Theory]
        [InlineData("26136907-2-13", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _hungaryValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("26136907-2-13", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _hungaryValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("13-09-189347", true)]
        [InlineData("01-10-042595", true)]
        [InlineData("10949621-2-44", false)]
        [InlineData("13-9-189347", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _hungaryValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("HU-12892312", true)]
        [InlineData("HU-12892313", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _hungaryValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("1037", true)]
        [InlineData("2380", true)]
        [InlineData("12321", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _hungaryValidator.ValidatePostalCode(code).IsValid);
        }

    }
}
