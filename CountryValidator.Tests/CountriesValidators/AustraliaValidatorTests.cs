using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class AustraliaValidatorTests
    {
        private readonly AustraliaValidator _australiaValidator;
        public AustraliaValidatorTests()
        {
            _australiaValidator = new AustraliaValidator();
        }

        [Theory]
        [InlineData("123 456 782", true)]
        [InlineData("123456782", true)]
        [InlineData("999 999 999", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _australiaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("123 456 782", true)]
        [InlineData("123456782", true)]
        [InlineData("999 999 999", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _australiaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        //Abn
        [InlineData("83 914 571 673", true)]
        [InlineData("51824753556", true)]
        [InlineData("99 999 999 999", false)]
        //Acn
        [InlineData("004 085 616", true)]
        [InlineData("010 499 966", true)]
        [InlineData("004085616", true)]
        [InlineData("999 999 999", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _australiaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("83 914 571 673", true)]
        [InlineData("51824753556", true)]
        [InlineData("99 999 999 999", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _australiaValidator.ValidateVAT(code).IsValid);
        }

    }
}
