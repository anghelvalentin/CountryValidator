using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class RomaniaValidatorTests
    {
        private readonly RomaniaValidator _romaniaValidator;

        public RomaniaValidatorTests()
        {
            _romaniaValidator = new RomaniaValidator();
        }

        [Theory]
        [InlineData("1630615123457", true)]
        [InlineData("1800101221144", true)]
        [InlineData("8800101221144", false)]
        [InlineData("1632215123457", false)]
        [InlineData("1630615123458", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _romaniaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("1630615123457", true)]
        [InlineData("1800101221144", true)]
        [InlineData("8800101221144", false)]
        [InlineData("1632215123457", false)]
        [InlineData("1630615123458", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _romaniaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("18547290", true)]
        [InlineData("RO18547290", true)]
        [InlineData("18547291", false)]
        [InlineData("RO18547291", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _romaniaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("18547290", true)]
        [InlineData("RO18547290", true)]
        [InlineData("18547291", false)]
        [InlineData("RO18547291", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _romaniaValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("021602", true)]
        [InlineData("061357", true)]
        [InlineData("32123", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _romaniaValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
