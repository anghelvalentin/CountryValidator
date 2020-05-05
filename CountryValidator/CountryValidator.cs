using CountryValidation.Countries;
using System.Collections.Generic;

namespace CountryValidation
{
    public class CountryValidator : ICountryValidator
    {
        static Dictionary<Country, IdValidationAbstract> _supportedCountries;

        static CountryValidator()
        {
            _supportedCountries = Load();
        }

        public static bool IsCountrySupported(Country country)
        {
            return _supportedCountries.ContainsKey(country);
        }

        private static Dictionary<Country, IdValidationAbstract> Load()
        {
            Dictionary<Country, IdValidationAbstract> ssnCountries = new Dictionary<Country, IdValidationAbstract>
            {

                { Country.AT, new AustriaValidator() },
                { Country.BE, new BelgiumValidator() },
                { Country.BG, new BulgariaValidator() },
                { Country.CY, new CyprusValidator()},
                { Country.CZ, new CzechValidator()},
                { Country.DE, new GermanyValidator() },
                { Country.DK, new DenmarkValidator() },
                { Country.EE, new EstoniaValidator() },
                { Country.ES, new SpainValidator() },
                { Country.FR, new FranceValidator() },
                { Country.FI, new FinlandValidator() },
                { Country.GR, new GreeceValidator() },
                { Country.HR, new CroatiaValidator() },
                { Country.HU, new HungaryValidator() },
                { Country.IE, new IrelandValidator() },
                { Country.IT, new ItalyValidator()},
                { Country.LV, new LatviaValidator() },
                { Country.LT, new LithuaniaValidator() },
                { Country.LU, new LuxembourgValidator() },
                { Country.MT, new MaltaValidator() },
                { Country.NL, new NetherlandsValidator() },
                { Country.PL, new PolandValidator() },
                { Country.PT, new PortugalValidator() },
                { Country.RO, new RomaniaValidator() },
                { Country.SK, new SlovakiaValidator() },
                { Country.SE, new SwedenValidator() },
                { Country.CH, new SwitzerlandValidator() },
                { Country.SI, new SloveniaValidator() },
                { Country.GB, new UnitedKingdomValidator() },
                { Country.US, new UnitedStatesValidator() }
            };

            return ssnCountries;
        }

        public ValidationResult ValidateIndividualTaxCode(string ssn, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateIndividualTaxCode(ssn);
            }
            return ValidationResult.Invalid("Not supported");

        }

        public ValidationResult ValidateVAT(string vat, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateVAT(vat);
            }
            return ValidationResult.Invalid("Not supported");

        }

        public ValidationResult ValidateEntity(string vat, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateEntity(vat);
            }
            return ValidationResult.Invalid("Not supported");

        }

        public ValidationResult ValidateNationalIdentityCode(string ssn, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateNationalIdentity(ssn);
            }
            return ValidationResult.Invalid("Not supported");

        }

        public ValidationResult ValidateZIPCode(string zip, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidatePostalCode(zip);
            }
            return ValidationResult.Invalid("Not supported");
        }
    }
}
