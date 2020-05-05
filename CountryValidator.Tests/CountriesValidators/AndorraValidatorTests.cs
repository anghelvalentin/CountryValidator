using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class AndorraValidatorTests
    {
        private readonly AndorraValidator _andorraValidator;
        public AndorraValidatorTests()
        {
            _andorraValidator = new AndorraValidator();
        }

        [Theory]
        [InlineData("U-132950-X", true)]
        [InlineData("A123B", false)]
        [InlineData("2012345699", false)]
        [InlineData("D059888N", true)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _andorraValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("U-132950-X", true)]
        [InlineData("A123B", false)]
        [InlineData("2012345699", false)]
        [InlineData("D059888N", true)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _andorraValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("U-132950-X", true)]
        [InlineData("A123B", false)]
        [InlineData("2012345699", false)]
        [InlineData("D059888N", true)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _andorraValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("U-132950-X", true)]
        [InlineData("A123B", false)]
        [InlineData("2012345699", false)]
        [InlineData("D059888N", true)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _andorraValidator.ValidateVAT(code).IsValid);
        }

    }
}
