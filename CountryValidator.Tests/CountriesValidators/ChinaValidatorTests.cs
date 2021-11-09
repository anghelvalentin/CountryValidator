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
        [InlineData("360402198611133850", true)]
        [InlineData("342122198211155230", true)]
        [InlineData("220211198701291224", true)]
        [InlineData("810000199408230021", true)]
        [InlineData("830000199201300022", true)]
        [InlineData("420400197512034215", true)]
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

        [Theory]
        [InlineData("9111010878398835XQ", true)]
        [InlineData("91310110MA1G8KLD1C", true)]
        [InlineData("91610000719782242D", true)]
        [InlineData("91420100MA49NUAH2W", true)]
        [InlineData("91420000706803542C", true)]
        [InlineData("91310116594798880J", true)]
        [InlineData("91110108600040399G", true)]
        [InlineData("51420000MJH2042495", true)]
        [InlineData("51420000MJH204142R", true)]
        [InlineData("51420000MJH2006517", true)]
        [InlineData("53420000MJH24497XN", true)]
        [InlineData("113100000024280004", true)]
        [InlineData("11310116002475827M", true)]        
        public void TestEntityCode(string code, bool isValid)
        {
            Assert.Equal(isValid, _chinaValidator.ValidateEntity(code).IsValid);
        }


    }
}
