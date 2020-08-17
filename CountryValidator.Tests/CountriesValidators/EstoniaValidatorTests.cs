using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class EstoniaValidatorTests
    {
        private readonly EstoniaValidator _estoniaValidator;

        public EstoniaValidatorTests()
        {
            _estoniaValidator = new EstoniaValidator();
        }

        [Theory]
        [InlineData("37605030299", true)]
        [InlineData("37605030291", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _estoniaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("37605030299", true)]
        [InlineData("37605030291", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _estoniaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("12345678", true)]
        [InlineData("12345679", false)]
        [InlineData("32345674", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _estoniaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("100931558", true)]
        [InlineData("100594102", true)]
        [InlineData("100594103", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _estoniaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("69501-", true)]
        [InlineData("12132", true)]
        [InlineData("232131", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _estoniaValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
