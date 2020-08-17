using CountryValidation.Countries;
using Xunit;

namespace CountryValidation.Tests
{
    public class SwitzerlandValidatorTests
    {
        private readonly SwitzerlandValidator _switzerlandValidator;

        public SwitzerlandValidatorTests()
        {
            _switzerlandValidator = new SwitzerlandValidator();
        }

        [Theory]
        [InlineData("7569217076985", true)]
        [InlineData("756.9217.0769.85", true)]
        [InlineData("756.9217.0769.84", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _switzerlandValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("7569217076985", true)]
        [InlineData("756.9217.0769.85", true)]
        [InlineData("756.9217.0769.84", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _switzerlandValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("CHE-100.155.212", true)]
        [InlineData("CHE100155212", true)]
        [InlineData("CHE-100.155.213", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _switzerlandValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("CHE-107.787.577 IVA", true)]
        [InlineData("CHE107787577IVA", true)]
        [InlineData("CHE-107.787.578 IVA", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _switzerlandValidator.ValidateVAT(code).IsValid);
        }

        [Theory]
        [InlineData("2544", true)]
        [InlineData("1211", true)]
        [InlineData("321", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _switzerlandValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
