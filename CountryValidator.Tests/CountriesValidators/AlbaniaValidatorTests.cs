using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class AlbaniaValidatorTests
    {
        private readonly AlbaniaValidator _albaniaValidator;
        public AlbaniaValidatorTests()
        {
            _albaniaValidator = new AlbaniaValidator();
        }

        [Theory]
        [InlineData("I05101999Q", true)]
        [InlineData("2012345699", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _albaniaValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("AL J 91402501 L", true)]
        [InlineData("K22218003V", true)]
        [InlineData("Z 22218003 V", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _albaniaValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("AL J 91402501 L", true)]
        [InlineData("K22218003V", true)]
        [InlineData("Z 22218003 V", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _albaniaValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("AL J 91402501 L", true)]
        [InlineData("K22218003V", true)]
        [InlineData("Z 22218003 V", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _albaniaValidator.ValidateVAT(code).IsValid);
        }

    }
}
