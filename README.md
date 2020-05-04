# Country Validator 

Country Validator is a .NET library that can validate **VAT codes, national identification numbers and tax identification numbers for individuals and companies**

## Features
- Validate Social Security Numbers/Personal Identity Numbers
- Validate VAT Codes
- Validate Tax Indentification Numbers for Individuals
- Validate Tax Identification Numbers For Companies

## Install
**Nuget Package [CountryValidator](https://www.nuget.org/packages/CountryValidator/)**
**Nuget Package [CountryValidator.DataAnnotations](https://www.nuget.org/packages/CountryValidator.DataAnnotations/)**


```powershell
Install-Package CountryValidator
Install-Package CountryValidator.DataAnnotations
```


## How to use Country Validator
### Using Validator Class
```csharp
CountryValidator validator = new CountryValidator();
ValidationResult validationResult = validator.ValidateNationalIdentityCode(ssn, Country.US);
if (validationResult.IsValid)
{
    Console.WriteLine("Valid");
}
else
{
    Console.WriteLine(validationResult.ErrorMessage);
}
```

### Using Data Annotations

```csharp
[HttpPost]
public IActionResult ValidateSSN([Required, SSNAttribute(Country.US)]string ssn)
{
    if (!ModelState.IsValid)
    {
        //log exception
    }
    return Ok();
}
```

### Live Demo
[Social Security Number Validation](https://randommer.io/SocialNumber/SsnValidator)

[VAT Code Validation](https://randommer.io/SocialNumber/VatValidator)


### Supported Countries
| Supported Country | Alpha Code 2 | National Identification Number Name                                       | VAT Code                                                                    |
|-------------------|--------------|---------------------------------------------------------------------------|------------------------------------------------------------------------|
| Austria           | AT           | Versicherungsnummer (VNR, SVNR, VSNR)                                     | UID (Umsatzsteuer-Identifikationsnummer)                               |
| Belgium           | BE           | Rijksregisternummer                                                       | BTW, TVA, NWSt, ondernemingsnummer (Belgian enterprise number).        |
| Bulgaria          | BG           | Edinen grazhdanski nomer (EGN)                                            | Идентификационен номер по ДДС                                          |
| Croatia           | HR           | OIB (Osobni identifikacijski broj, Croatian identification number)        | OIB (Osobni identifikacijski broj, Croatian identification number)     |
| Cyprus            | CY           | Identity Number                                                           | ΦΠΑ                                                                    |
| Czechia           | CZ           | Rodné Císlo (RČ)                                                          | Danove Identifikacni Cislo (DIC/VAT)                                   |
| Denmark           | DK           | Det Centrale Personregister (CPR)                                         | Momsregistreringsnummer (CVR/VAT)                                      |
| Estonia           | EE           | Personal Id - Isikukood                                                   | Kaibemaksukohuslase (KMKR)                                             |
| Finland           | FI           | Henkilötunnus (HETU)                                                      | Arvonlisaveronumero (ALV)                                              |
| France            | FR           | INSEE/NIR (French personal identification number)                         | Taxe sur la Valeur Ajoutee (TVA)                                       |
| Germany           | DE           | Steueridentifikationsnummer                                               | Umsatzsteur Identifikationnummer (VAT)                                 |
| Greece            | GR           | AMKA (Αριθμός Μητρώου Κοινωνικής Ασφάλισης, Greek social security number) | VAT Number (FPA)                                                       |
| Great Britain     | GB           | NINO or NHS                                                               | VAT Reg No                                                             |
| Hungary           | HU           | Szemelyi Szam Ellenorzese                                                 | Kozossegi Adoszam (ANUM)                                               |
| Ireland           | IE           | PPS No (Personal Public Service Number, Irish personal number).           | Irish Tax Reference Number (VAT)                                       |
| Italy             | IT           | Codice fiscale - Fiscal Code                                              | Partita IVA                                                            |
| Latvia            | LV           | Personal Code - Personas kods                                             | PVN (Pievienotās vērtības nodokļa, Latvian VAT number)                 |
| Lithuania         | LT           | Personal Code - Asmens kodas                                              | PVM (Pridėtinės vertės mokestis mokėtojo kodas, Lithuanian VAT number) |
| Luxembourg        | LU           | Personal identification code (PIC)                                        | TVA (taxe sur la valeur ajoutée, Luxembourgian VAT number)             |
| Malta             | MT           | Identity Card Number                                                      | VAT Number                                                             |
| Netherlands       | NL           | Burgerservicenummer (BSN) - Citizen Service Number or Onderwijsnummer     | Omzetbelastingnummer (BTW)                                             |
| Poland            | PL           | Polish National Identification Number (PESEL)                             | Numer Identyfikacji Podatkowej (NIP)                                   |
| Portugal          | PT           | Número de identificação civil - NIC                                       | Numero de Identificacao Fiscal (NIF)                                   |
| Romania           | RO           | Cod Numeric Personal - Personal Numerical Code (CNP)                      | Cod fiscal TVA                                                         |
| Slovakia          | SK           | Rodné Císlo (RČ)                                                          | Identifikačné číslo pre daň z pridanej hodnoty (IČ DPH)                |
| Slovenia          | SI           | Unique Master Citizen Number JMBG                                         | Identifikacijska številka za DDV                                       |
| Spain             | ES           | DNI/NIF/NIE                                                               | NIF / CIF                                                              |
| Switzerland       | CH           | AHV (Sozialversicherungsnummer)                                           | VAT, MWST, TVA, IVA, TPV (Mehrwertsteuernummer, the Swiss VAT number). |
| Sweden            | SE           | Personnummer - Personal Identity Number                                   | VAT-nummer or momsnummer                                               |
| United States     | US           | Social Security Number                                                    | Not Supported                                                          |

### License
Copyright 2020 Anghel Valentin

Licensed under the Apache License, Version 2.0: http://www.apache.org/licenses/LICENSE-2.0

##### Special thanks
[Python Stdnum](https://github.com/arthurdejong/python-stdnum)