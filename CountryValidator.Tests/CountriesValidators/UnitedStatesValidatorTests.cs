using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class UnitedStatesValidatorTests
    {
        private readonly UnitedStatesValidator _unitedStatesValidator;
        public UnitedStatesValidatorTests()
        {
            _unitedStatesValidator = new UnitedStatesValidator();
        }

        [Theory]
        [InlineData("181-26-4874", true)]
        [InlineData("136-23-9624", true)]
        [InlineData("181-26-48741", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _unitedStatesValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("181-26-4874", true)]
        [InlineData("912903456", true)]
        [InlineData("20267565392", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _unitedStatesValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("91-1144442", true)]
        [InlineData("07-1144442", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _unitedStatesValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("20267565393")]
        public void TestCorrectVatCode(string code)
        {
            Assert.Throws<NotSupportedException>(() => _unitedStatesValidator.ValidateVAT(code));

        }

        [Theory]
        [InlineData("95014", true)]
        [InlineData("99999-9999", true)]
        [InlineData("950142", false)]
        public void TestPostalCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _unitedStatesValidator.ValidatePostalCode(code).IsValid);
        }
    }
}
