using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountryValidator.Tests.CountriesValidators
{
    public class ChinaValidatorTests
    {
        private readonly ChinaValidator _chinaValidator;

        public ChinaValidatorTests()
        {
            _chinaValidator = new ChinaValidator();
        }

        [Theory]
        [InlineData("342122198211155230", true)]
        [InlineData("810000199408230021", true)]
        [InlineData("830000199201300022", true)]
        [InlineData("810000199408230023", false)]
        public void TestNationalId_18digits(string code, bool isValid)
        {
            Assert.Equal(isValid, _chinaValidator.ValidateNationalIdentity(code).IsValid);
        }
        [Theory]
        [InlineData("350424870506202", true)]
        [InlineData("350424875006202", false)]
        [InlineData("350424870540202", false)]
        public void TestNationalId_15digits(string code, bool isValid)
        {
            Assert.Equal(isValid, _chinaValidator.ValidateNationalIdentity(code).IsValid);
        }
    }
}
