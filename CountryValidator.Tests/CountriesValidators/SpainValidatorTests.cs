using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidation.Tests
{
    public class SpainValidatorTests
    {
        private readonly SpainValidator _spainValidator;

        public SpainValidatorTests()
        {
            _spainValidator = new SpainValidator();
        }

        [Theory]
        [InlineData("54362315K", true)]
        [InlineData("54362315-K", true)]
        [InlineData("X2482300W", true)]
        [InlineData("X-2482300W", true)]
        [InlineData("X-2482300-W", true)]
        [InlineData("X-2482300A", false)]
        [InlineData("54362315Z", false)]
        public void TestNationalId(string code, bool isValid)
        {
            Assert.Equal(isValid, _spainValidator.ValidateNationalIdentity(code).IsValid);
        }

        [Theory]
        [InlineData("X-2482300W", true)]
        [InlineData("X-2482300-W", true)]
        [InlineData("X-2482300A", false)]
        public void TestIndividualCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _spainValidator.ValidateIndividualTaxCode(code).IsValid);
        }

        [Theory]
        [InlineData("J99216582", true)]
        [InlineData("J99216583", false)]
        [InlineData("J992165831", false)]
        [InlineData("M-1234567-L", false)]
        public void TestCorrectEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _spainValidator.ValidateEntity(code).IsValid);
        }

        [Theory]
        [InlineData("54362315K", true)]
        [InlineData("X2482300W", true)]
        [InlineData("X5253868R ", true)]
        [InlineData("M1234567L", true)]
        [InlineData("J99216582", true)]
        [InlineData("B58378431", true)]
        [InlineData("B64717838", true)]
        [InlineData("R5000274J", true)]
        [InlineData("Q5000274J", true)]
        [InlineData("J99216583", false)]
        [InlineData("54362315Z", false)]
        [InlineData("X2482300A", false)]
        public void TestCorrectVatCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _spainValidator.ValidateVAT(code).IsValid);
        }

    }
}
