using CountryValidation.Countries;
using System.Collections.Generic;
using System.Linq;

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

        public static List<string> SupportedCountries
        {
            get
            {
                return _supportedCountries.Keys.Select(c => c.ToString()).ToList();
            }
        }


        private static Dictionary<Country, IdValidationAbstract> Load()
        {
            Dictionary<Country, IdValidationAbstract> ssnCountries = new Dictionary<Country, IdValidationAbstract>
            {
                { Country.AL, new AlbaniaValidator() },
                { Country.AD, new AndorraValidator() },
                { Country.AR, new ArgentinaValidator() },
                { Country.AM, new ArmeniaValidator() },
                { Country.AU, new AustraliaValidator() },
                { Country.AT, new AustriaValidator() },
                { Country.AZ, new AzerbaijanValidator() },
                { Country.BA, new BosniaValidator() },
                { Country.BY, new BelarusValidator() },
                { Country.BE, new BelgiumValidator() },
                { Country.BH, new BahrainValidator() },
                { Country.BR, new BrazilValidator() },
                { Country.BG, new BulgariaValidator() },
                { Country.CA, new CanadaValidator() },
                { Country.CH, new SwitzerlandValidator() },
                { Country.CN, new ChinaValidator() },
                { Country.CL, new ChileValidator() },
                { Country.CO, new ColombiaValidator()},
                { Country.CR, new CostaRicaValidator()},
                { Country.CU, new CubaValidator()},
                { Country.CY, new CyprusValidator()},
                { Country.CZ, new CzechValidator()},
                { Country.DE, new GermanyValidator() },
                { Country.DK, new DenmarkValidator() },
                { Country.DO, new DominicanRepublicValidator() },
                { Country.EE, new EstoniaValidator() },
                { Country.EC, new EcuadorValidator() },
                { Country.SV, new ElSalvadorValidator() },
                { Country.ES, new SpainValidator() },
                { Country.FR, new FranceValidator() },
                { Country.FI, new FinlandValidator() },
                { Country.FO, new FaroeIslandsValidator() },
                { Country.GR, new GreeceValidator() },
                { Country.GT, new GuatemalaValidator() },
                { Country.GE, new GeorgiaValidator() },
                { Country.HK, new HongKongValidator() },
                { Country.HR, new CroatiaValidator() },
                { Country.HU, new HungaryValidator() },
                { Country.IS, new IcelandValidator() },
                { Country.IE, new IrelandValidator() },
                { Country.IN, new IndiaValidator() },
                { Country.ID, new IndonesiaValidator() },
                { Country.IT, new ItalyValidator()},
                { Country.IL, new IsraelValidator() },
                { Country.JP, new JapanValidator() },
                { Country.KR, new KoreaValidator() },
                { Country.LV, new LatviaValidator() },
                { Country.LT, new LithuaniaValidator() },
                { Country.LU, new LuxembourgValidator() },
                { Country.MK, new MacedoniaValidator() },
                { Country.ME, new MontenegroValidator() },
                { Country.MY, new MalaysiaValidator() },
                { Country.MT, new MaltaValidator() },
                { Country.MU, new MauritiusValidator() },
                { Country.MX, new MexicoValidator() },
                { Country.MD, new MoldovaValidator() },
                { Country.MC, new MonacoValidator() },
                { Country.NG, new NigeriaValidator() },
                { Country.NL, new NetherlandsValidator() },
                { Country.NZ, new NewZealandValidator() },
                { Country.NO, new NorwayValidator() },
                { Country.PK, new PakistanValidator() },
                { Country.PY, new ParaguayValidator() },
                { Country.PL, new PolandValidator() },
                { Country.PT, new PortugalValidator() },
                { Country.PE, new PeruValidator() },
                { Country.RO, new RomaniaValidator() },
                { Country.RU, new RussiaValidator() },
                { Country.RS, new SerbiaValidator() },
                { Country.SM, new SanMarinoValidator() },
                { Country.SK, new SlovakiaValidator() },
                { Country.SE, new SwedenValidator() },
                { Country.SI, new SloveniaValidator() },
                { Country.TW, new TaiwanValidator() },
                { Country.TH, new ThailandValidator() },
                { Country.TR, new TurkeyValidator() },
                { Country.GB, new UnitedKingdomValidator() },
                { Country.US, new UnitedStatesValidator() },
                { Country.UA, new UkraineValidator() },
                { Country.UY, new UruguayValidator() },
                { Country.VE, new VenezuelaValidator() },
                { Country.ZA, new SouthAfricaValidator() }
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
