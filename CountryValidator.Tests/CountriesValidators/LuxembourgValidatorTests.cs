using CountryValidator.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests
{
    public class LuxembourgValidatorTests
    {
        private readonly LuxembourgValidator _luxembourgValidator;

        public LuxembourgValidatorTests()
        {
            _luxembourgValidator = new LuxembourgValidator();
        }

        [Theory]
        [InlineData("1893120105732", true)]
        [InlineData("1893120105733", false)]
        [InlineData("18931201057321", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("1893120105732", true)]
        [InlineData("1893120105733", false)]
        [InlineData("18931201057321", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("15027442", true)]
        [InlineData("15027443", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("15027442", true)]
        [InlineData("15027443", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _luxembourgValidator.ValidateVAT(code).IsValid);
        }

    }
}
