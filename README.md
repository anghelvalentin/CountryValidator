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
|   Supported Country  | Alpha Code 2 |                        National Identification Number Name                        |                                   VAT Code                                  | Entity code                                            | Postal Code        |
|:--------------------:|:------------:|:---------------------------------------------------------------------------------:|:---------------------------------------------------------------------------:|--------------------------------------------------------|--------------------|
| Andorra              | AD           | NRT (Número de Registre Tributari, Andorra tax number)                            | NRT (Número de Registre Tributari, Andorra tax number)                      | NRT (Número de Registre Tributari, Andorra tax number) | :heavy_check_mark: |
| United Arab Emirates | AE           |                                                                                   | :x:                                                                         | :x:                                                    | :x:                |
| Albania              | AL           | Identity Number - Numri i Identitetit (NID)                                       | NIPT (Numri i Identifikimit për Personin e Tatueshëm, Albanian VAT number). | NIPT                                                   | :heavy_check_mark: |
| Armenia              | AM           | TIN Number                                                                        | TIN Number                                                                  | TIN Number                                             | :heavy_check_mark: |
| Argentina            | AR           | DNI number                                                                        | VAT/IVA/CUIT                                                                | CUIT                                                   | :heavy_check_mark: |
| Austria              | AT           | Versicherungsnummer (VNR, SVNR, VSNR)                                             | UID (Umsatzsteuer-Identifikationsnummer)                                    |                                                        | :heavy_check_mark: |
| Australia            | AU           | TFN                                                                               | ABN                                                                         | ABN/ACN/TFN                                            | :heavy_check_mark: |
| Azerbaijan           | AZ           | PIN - Personal Identification Number                                              | VÖEN/TIN Number                                                             | VÖEN/TIN Number                                        | :heavy_check_mark: |
| Bosnia               | BA           | Unique Master Citizen Number JMBG                                                 |                                                                             | :x:                                                    | :heavy_check_mark: |
| Belgium              | BE           | Rijksregisternummer                                                               | BTW, TVA, NWSt, ondernemingsnummer (Belgian enterprise number).             |                                                        | :heavy_check_mark: |
| Bulgaria             | BG           | Edinen grazhdanski nomer (EGN)                                                    | Идентификационен номер по ДДС                                               |                                                        | :heavy_check_mark: |
| Bahrain              | BH           | Social security number                                                            | :x:                                                                         | :x:                                                    | :heavy_check_mark: |
| Bolivia              | BO           | CI Number                                                                         | Número de Identificación Tributaria                                         | Número de Identificación Tributaria                    | :heavy_check_mark: |
| Brazil               | BR           | CPF                                                                               | Brazil Cadastro Nacional da Pessoa Juridica (CNPJ)                          | Brazil Cadastro Nacional da Pessoa Juridica (CNPJ)     | :heavy_check_mark: |
| Belarus              | BY           | Payer's account number (UNP)                                                      | Payer's account number (UNP)                                                | Payer's account number (UNP)                           | :heavy_check_mark: |
| Canada               | CA           | SIN Number                                                                        | Business Number                                                             | Business Number                                        | :heavy_check_mark: |
| Chile                | CL           | National Tax Number (RUN/RUT)                                                     | National Tax Number (RUN/RUT)                                               | National Tax Number (RUN/RUT)                          | :heavy_check_mark: |
| China                | CN           | Social Number (15 digits and 18 digits)                                           | :x:                                                                         | Business Number                                                    | :heavy_check_mark: |
| Colombia             | CO           | NIT (Número De Identificación Tributaria, Colombian identity code)                | VAT                                                                         | RUT (Registro Unico Tributario)                        | :heavy_check_mark: |
| Costa Rica           | CR           | CPF (Cédula de Persona Física,physical person ID number)/CR(Cédula de Residencia) | CPJ                                                                         | CPJ                                                    | :heavy_check_mark: |
| Cuba                 | CU           | NI (Número de identidad)                                                          | :x:                                                                         | :x:                                                    | :heavy_check_mark: |
| Croatia              | HR           | OIB (Osobni identifikacijski broj, Croatian identification number)                | OIB (Osobni identifikacijski broj, Croatian identification number)          | OIB (Osobni identifikacijski broj)                     | :heavy_check_mark: |
| Cyprus               | CY           | Identity Number                                                                   | ΦΠΑ                                                                         |                                                        | :heavy_check_mark: |
| Czechia              | CZ           | Rodné Císlo (RČ)                                                                  | Danove Identifikacni Cislo (DIC/VAT)                                        |                                                        | :heavy_check_mark: |
| Denmark              | DK           | Det Centrale Personregister (CPR)                                                 | Momsregistreringsnummer (CVR/VAT)                                           |                                                        | :heavy_check_mark: |
| Dominican Republic   | DO           | Cedula                                                                            | RNC (Registro Nacional del Contribuyente)                                   | RNC (Registro Nacional del Contribuyente)              | :heavy_check_mark: |
| Ecuador              | EC           | Registro Unico de Contribuyentes (RUC)                                            | Registro Unico de Contribuyentes (RUC)                                      | Registro Unico de Contribuyentes (RUC)                 | :heavy_check_mark: |
| Estonia              | EE           | Personal Id - Isikukood                                                           | Kaibemaksukohuslase (KMKR)                                                  | Registrikood (Estonian organisation registration code) | :heavy_check_mark: |
| Finland              | FI           | Henkilötunnus (HETU)                                                              | Arvonlisaveronumero (ALV)                                                   | Arvonlisaveronumero (ALV)                              | :heavy_check_mark: |
| Faroe Islands        | FO           | P-number                                                                          | V-number                                                                    | V-number                                               | :heavy_check_mark: |
| France               | FR           | INSEE/NIR (French personal identification number)                                 | Taxe sur la Valeur Ajoutee (TVA)                                            | SIREN                                                  | :heavy_check_mark: |
| Germany              | DE           | Steueridentifikationsnummer                                                       | Umsatzsteur Identifikationnummer (VAT)                                      | Steuernummer                                           | :heavy_check_mark: |
| Greece               | GR           | AMKA (Αριθμός Μητρώου Κοινωνικής Ασφάλισης, Greek social security number)         | VAT Number (FPA)                                                            | VAT Number (FPA)                                       | :heavy_check_mark: |
| Great Britain        | GB           | NINO or NHS                                                                       | VAT Reg No                                                                  | Value added tax registration number                    | :heavy_check_mark: |
| Guatemala            | GT           | :x:                                                                               | NIT                                                                         | NIT (Número de Identificación Tributaria)              | :heavy_check_mark: |
| Hong Kong            | HK           | Social number                                                                     | :x:                                                                         | :x:                                                    | :x:                |
| Hungary              | HU           | Szemelyi Szam Ellenorzese                                                         | Kozossegi Adoszam (ANUM)                                                    | Cegjegyzekszam Ellenorzese                             | :heavy_check_mark: |
| Indonesia            | ID           | NPWP                                                                              | NPWP - Nomor Pokok Wajib Pajak                                              | NPWP                                                   | :heavy_check_mark: |
| Ireland              | IE           | PPS No (Personal Public Service Number, Irish personal number).                   | Irish Tax Reference Number (VAT)                                            |                                                        | :heavy_check_mark: |
| Israel               | IL           |                                                                                   |                                                                             |                                                        | :heavy_check_mark: |
| India                | IN           | PAN (Permanent Account Number)                                                    | VAT TIN / CST TIN                                                           | PAN (Permanent Account Number)                         | :heavy_check_mark: |
| Iceland              | IS           | Kennitala                                                                         | Virdisaukaskattsnumer (VSK)                                                 | Kennitala                                              | :heavy_check_mark: |
| Italy                | IT           | Codice fiscale - Fiscal Code                                                      | Partita IVA                                                                 |                                                        | :heavy_check_mark: |
| Japan                | JP           | Japan My Number                                                                   | Japan My Number                                                             | CN hōjin bangō, Japanese Corporate Number              | :heavy_check_mark: |
| Korea                | KR           | Resident Registration Number (RRN)                                                | :x:                                                                         | :x:                                                    | :heavy_check_mark: |
| Kazakhstan           | KZ           | PIN                                                                               | BIN                                                                         | BIN БСН – бизнес-сәйкестендіру нөмірі                  | :heavy_check_mark: |
| Latvia               | LV           | Personal Code - Personas kods                                                     | PVN (Pievienotās vērtības nodokļa, Latvian VAT number)                      | PVN (Pievienotās vērtības nodokļa, Latvian VAT number) | :heavy_check_mark: |
| Lithuania            | LT           | Personal Code - Asmens kodas                                                      | PVM (Pridėtinės vertės mokestis mokėtojo kodas, Lithuanian VAT number)      |                                                        | :heavy_check_mark: |
| Luxembourg           | LU           | Personal identification code (PIC)                                                | TVA (taxe sur la valeur ajoutée, Luxembourgian VAT number)                  |                                                        | :heavy_check_mark: |
| Malta                | MT           | Identity Card Number                                                              | VAT Number                                                                  |                                                        | :heavy_check_mark: |
| Monaco               | MC           | :x:                                                                               | VAT Number                                                                  | VAT Number                                             | :heavy_check_mark: |
| Moldova              | MD           | IDNP (Identification Number of Person)                                            | Validate VAT code (Nr. de Inregistrare TVA)                                 |                                                        | :heavy_check_mark: |
| Montenegro           | ME           |                                                                                   |                                                                             |                                                        | :heavy_check_mark: |
| Macedonia            | MK           |                                                                                   | Vat Number                                                                  |                                                        | :heavy_check_mark: |
| Mauritius            | MU           | ID number (Mauritian national identifier)                                         |                                                                             |                                                        | :heavy_check_mark: |
| Netherlands          | NL           | Burgerservicenummer (BSN) - Citizen Service Number or Onderwijsnummer             | Omzetbelastingnummer (BTW)                                                  |                                                        | :heavy_check_mark: |
| Norway               | NO           |                                                                                   |                                                                             |                                                        | :heavy_check_mark: |
| New Zealand          | NZ           |                                                                                   |                                                                             |                                                        | :heavy_check_mark: |
| Peru                 | PE           | RUC                                                                               | RUC Peruvian company tax number                                             | RUC Peruvian company tax number                        | :heavy_check_mark: |
| Philippines          | PH           |                                                                                   |                                                                             |                                                        | :heavy_check_mark: |
| Pakistan             | PK           | CNIC (Computerized National Identity Card)                                        |                                                                             |                                                        | :heavy_check_mark: |
| Poland               | PL           | Polish National Identification Number (PESEL)                                     | Numer Identyfikacji Podatkowej (NIP)                                        |                                                        | :heavy_check_mark: |
| Portugal             | PT           | Número de identificação civil - NIC                                               | Numero de Identificacao Fiscal (NIF)                                        |                                                        | :heavy_check_mark: |
| Paraguay             | PY           | Registro Unico de Contribuyentes (RUC)                                            | Registro Unico de Contribuyentes (RUC)                                      | Registro Unico de Contribuyentes (RUC)                 | :heavy_check_mark: |
| Romania              | RO           | Cod Numeric Personal - Personal Numerical Code (CNP)                              | Cod fiscal TVA                                                              | Cod fiscal                                             | :heavy_check_mark: |
| Serbia               | RS           | Unique Master Citizen Number JMBG                                                 |                                                                             |                                                        | :heavy_check_mark: |
| Russia               | RU           | Taxpayer Personal Identification Number (INN)                                     | VAT Number                                                                  | VAT Number                                             | :heavy_check_mark: |
| Slovakia             | SK           | Rodné Císlo (RČ)                                                                  | Identifikačné číslo pre daň z pridanej hodnoty (IČ DPH)                     |                                                        | :heavy_check_mark: |
| Slovenia             | SI           | Unique Master Citizen Number JMBG                                                 | Identifikacijska številka za DDV                                            |                                                        | :heavy_check_mark: |
| San Marino           | SM           | COE (Codice operatore economico, San Marino national tax number)                  | COE (Codice operatore economico, San Marino national tax number)            | COE (Codice operatore economico)                       | :heavy_check_mark: |
| El Salvador          | SV           | NIT (Número de Identificación Tributaria, El Salvador tax number)                 |                                                                             |                                                        | :heavy_check_mark: |
| Thailand             | TH           | Thailand citizen number                                                           |                                                                             |                                                        | :heavy_check_mark: |
| Turkey               | TR           | T.C. Kimlik No. (Turkish personal identification number)                          | VKN (Vergi Kimlik Numarası, Turkish tax identification number)              | VKN (Vergi Kimlik Numarası)                            | :heavy_check_mark: |
| Taiwan               | TW           | SSN                                                                               | :x:                                                                         | :x:                                                    | :heavy_check_mark: |
| Spain                | ES           | DNI/NIF/NIE                                                                       | NIF / CIF                                                                   | NIF / CIF                                              | :heavy_check_mark: |
| Switzerland          | CH           | AHV (Sozialversicherungsnummer)                                                   | VAT, MWST, TVA, IVA, TPV (Mehrwertsteuernummer, the Swiss VAT number).      | UID Unternehmens-Identifikationsnummer                 | :heavy_check_mark: |
| Sweden               | SE           | Personnummer - Personal Identity Number                                           | VAT-nummer or momsnummer                                                    | Orgnr (Organisationsnummer, Swedish company number)    | :heavy_check_mark: |
| United States        | US           | Social Security Number                                                            | Not Supported                                                               | EIN                                                    | :heavy_check_mark: |
| Ukraine              | UA           | Social Number                                                                     | VAT                                                                         | VAT                                                    | :heavy_check_mark: |
| Uruguay              | UY           | RUT numbers                                                                       | RUT numbers                                                                 | RUT numbers                                            | :heavy_check_mark: |
| Venezuela            | VE           | Registro de Informacion Fiscal (RIF)                                              | Registro de Informacion Fiscal (RIF)                                        | Registro de Informacion Fiscal (RIF)                   | :heavy_check_mark: |
| South Africa         | ZA           | Social Number                                                                     | VAT Code                                                                    | VAT Code                                               | :heavy_check_mark: |


<a href="https://www.buymeacoffee.com/valentinanghel" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" style="height: 51px !important;width: 217px !important;" ></a>

### License
Copyright 2020 Anghel Valentin

Licensed under the Apache License, Version 2.0: http://www.apache.org/licenses/LICENSE-2.0

##### Special thanks
[Python Stdnum](https://github.com/arthurdejong/python-stdnum)