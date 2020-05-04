using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class IrelandValidatorTests
    {
        private readonly IrelandValidator _irelandValidator;

        public IrelandValidatorTests()
        {
            _irelandValidator = new IrelandValidator();
        }

        [Theory]
        [InlineData("6433435F", true)]
        [InlineData("6433435FW", true)]
        [InlineData("6433435OA", true)]
        [InlineData("6433435IH", false)]
        [InlineData("6433435VH", false)]
        [InlineData("6433435E", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _irelandValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("6433435F", true)]
        [InlineData("6433435FW", true)]
        [InlineData("6433435OA", true)]
        [InlineData("6433435IH", false)]
        [InlineData("6433435VH", false)]
        [InlineData("6433435E", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _irelandValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("6433435IH", true)]
        [InlineData("6433435VH", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _irelandValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("6433435F", true)]
        [InlineData("6433435OA", true)]
        [InlineData("8D79739I", true)]
        [InlineData("8D79738J", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _irelandValidator.ValidateVAT(code).IsValid);
        }

    }
}
