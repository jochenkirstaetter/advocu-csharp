using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Advocu;

/// <summary>
/// Specifies the country for an activity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AdvocuCountry>))]
public enum AdvocuCountry
{
    Afghanistan,
    [JsonStringEnumMemberName("Åland Islands")]
    AlandIslands,
    Albania,
    Algeria,
    [JsonStringEnumMemberName("American Samoa")]
    AmericanSamoa,
    Andorra,
    Angola,
    Anguilla,
    Antarctica,
    [JsonStringEnumMemberName("Antigua and Barbuda")]
    AntiguaAndBarbuda,
    Argentina,
    Armenia,
    Aruba,
    Australia,
    Austria,
    Azerbaijan,
    Bahamas,
    Bahrain,
    Bangladesh,
    Barbados,
    Belarus,
    Belgium,
    Belize,
    Benin,
    Bermuda,
    Bhutan,
    [JsonStringEnumMemberName("Bolivia (Plurinational State of)")]
    BoliviaPlurinationalStateOf,
    [JsonStringEnumMemberName("Bonaire (Sint Eustatius and Saba)")]
    BonaireSaintEustatiusAndSaba,
    [JsonStringEnumMemberName("Bosnia and Herzegovina")]
    BosniaAndHerzegovina,
    Botswana,
    [JsonStringEnumMemberName("Bouvet Island")]
    BouvetIsland,
    Brazil,
    [JsonStringEnumMemberName("British Indian Ocean Territory")]
    BritishIndianOceanTerritory,
    [JsonStringEnumMemberName("Brunei Darussalam")]
    BruneiDarussalam,
    Bulgaria,
    [JsonStringEnumMemberName("Burkina Faso")]
    BurkinaFaso,
    Burundi,
    [JsonStringEnumMemberName("Cabo Verde")]
    CaboVerde,
    Cambodia,
    Cameroon,
    Canada,
    [JsonStringEnumMemberName("Cayman Islands")]
    CaymanIslands,
    [JsonStringEnumMemberName("Central African Republic")]
    CentralAfricanRepublic,
    Chad,
    Chile,
    China,
    [JsonStringEnumMemberName("Christmas Island")]
    ChristmasIsland,
    [JsonStringEnumMemberName("Cocos (Keeling) Islands")]
    CocosKeelingIslands,
    Colombia,
    Comoros,
    Congo,
    [JsonStringEnumMemberName("Congo (Democratic Republic of the)")]
    CongoDemocraticRepublicOfThe,
    [JsonStringEnumMemberName("Cook Islands")]
    CookIslands,
    [JsonStringEnumMemberName("Costa Rica")]
    CostaRica,
    [JsonStringEnumMemberName("Côte d'Ivoire")]
    CoteDIvoire,
    Croatia,
    Cuba,
    Curacao,
    Cyprus,
    Czechia,
    Denmark,
    Djibouti,
    Dominica,
    [JsonStringEnumMemberName("Dominican Republic")]
    DominicanRepublic,
    Ecuador,
    Egypt,
    [JsonStringEnumMemberName("El Salvador")]
    ElSalvador,
    [JsonStringEnumMemberName("Equatorial Guinea")]
    EquatorialGuinea,
    Eritrea,
    Estonia,
    Eswatini,
    Ethiopia,
    [JsonStringEnumMemberName("Falkland Islands (Malvinas)")]
    FalklandIslandsMalvinas,
    [JsonStringEnumMemberName("Faroe Islands")]
    FaroeIslands,
    Fiji,
    Finland,
    France,
    [JsonStringEnumMemberName("French Guiana")]
    FrenchGuiana,
    [JsonStringEnumMemberName("French Polynesia")]
    FrenchPolynesia,
    [JsonStringEnumMemberName("French Southern Territories")]
    FrenchSouthernTerritories,
    Gabon,
    Gambia,
    Georgia,
    Germany,
    Ghana,
    Gibraltar,
    Greece,
    Greenland,
    Grenada,
    Guadeloupe,
    Guam,
    Guatemala,
    Guernsey,
    Guinea,
    [JsonStringEnumMemberName("Guinea-Bissau")]
    GuineaBissau,
    Guyana,
    Haiti,
    [JsonStringEnumMemberName("Heard Island and McDonald Islands")]
    HeardIslandAndMcDonaldIslands,
    [JsonStringEnumMemberName("Holy See")]
    HolySee,
    Honduras,
    [JsonStringEnumMemberName("Hong Kong")]
    HongKong,
    Hungary,
    Iceland,
    India,
    Indonesia,
    [JsonStringEnumMemberName("Iran (Islamic Republic of)")]
    IranIslamicRepublicOf,
    Iraq,
    Ireland,
    [JsonStringEnumMemberName("Isle of Man")]
    IsleOfMan,
    Israel,
    Italy,
    Jamaica,
    Japan,
    Jersey,
    Jordan,
    Kazakhstan,
    Kenya,
    Kiribati,
    [JsonStringEnumMemberName("Korea (Democratic People's Republic of)")]
    KoreaDemocraticPeopleSRepublicOf,
    [JsonStringEnumMemberName("Korea (Republic of)")]
    KoreaRepublicOf,
    Kosovo,
    Kuwait,
    Kyrgyzstan,
    [JsonStringEnumMemberName("Lao People's Democratic Republic")]
    LaoPeopleSDemocraticRepublic,
    Latvia,
    Lebanon,
    Lesotho,
    Liberia,
    Libya,
    Liechtenstein,
    Lithuania,
    Luxembourg,
    Macao,
    Madagascar,
    Malawi,
    Malaysia,
    Maldives,
    Mali,
    Malta,
    [JsonStringEnumMemberName("Marshall Islands")]
    MarshallIslands,
    Martinique,
    Mauritania,
    Mauritius,
    Mayotte,
    Mexico,
    [JsonStringEnumMemberName("Micronesia (Federated States of)")]
    MicronesiaFederatedStatesOf,
    [JsonStringEnumMemberName("Moldova (Republic of)")]
    MoldovaRepublicOf,
    Monaco,
    Mongolia,
    Montenegro,
    Montserrat,
    Morocco,
    Mozambique,
    Myanmar,
    Namibia,
    Nauru,
    Nepal,
    Netherlands,
    [JsonStringEnumMemberName("New Caledonia")]
    NewCaledonia,
    [JsonStringEnumMemberName("New Zealand")]
    NewZealand,
    Nicaragua,
    Niger,
    Nigeria,
    Niue,
    [JsonStringEnumMemberName("Norfolk Island")]
    NorfolkIsland,
    [JsonStringEnumMemberName("North Cyprus")]
    NorthMacedonia,
    [JsonStringEnumMemberName("Northern Mariana Islands")]
    NorthernMarianaIslands,
    Norway,
    Oman,
    Pakistan,
    Palau,
    [JsonStringEnumMemberName("Palestine (State of)")]
    PalestineStateOf,
    Panama,
    [JsonStringEnumMemberName("Papua New Guinea")]
    PapuaNewGuinea,
    Paraguay,
    Peru,
    Philippines,
    Pitcairn,
    Poland,
    Portugal,
    [JsonStringEnumMemberName("Puerto Rico")]
    PuertoRico,
    Qatar,
    Reunion,
    Romania,
    [JsonStringEnumMemberName("Russian Federation")]
    RussianFederation,
    Rwanda,
    [JsonStringEnumMemberName("Saint Barthélemy")]
    SaintBarthelemy,
    [JsonStringEnumMemberName("Saint Helena (Ascension and Tristan da Cunha)")]
    SaintHelenaAscensionAndTristanDaCunha,
    [JsonStringEnumMemberName("Saint Kitts and Nevis")]
    SaintKittsAndNevis,
    [JsonStringEnumMemberName("Saint Lucia")]
    SaintLucia,
    [JsonStringEnumMemberName("Saint Martin (French part)")]
    SaintMartinFrenchPart,
    [JsonStringEnumMemberName("Saint Pierre and Miquelon")]
    SaintPierreAndMiquelon,
    [JsonStringEnumMemberName("Saint Vincent and the Grenadines")]
    SaintVincentAndTheGrenadines,
    Samoa,
    [JsonStringEnumMemberName("San Marino")]
    SanMarino,
    [JsonStringEnumMemberName("Sao Tome and Principe")]
    SaoTomeAndPrincipe,
    [JsonStringEnumMemberName("Saudi Arabia")]
    SaudiArabia,
    Senegal,
    Serbia,
    Seychelles,
    [JsonStringEnumMemberName("Sierra Leone")]
    SierraLeone,
    Singapore,
    [JsonStringEnumMemberName("Sint Maarten (Dutch part)")]
    SintMaartenDutchPart,
    Slovakia,
    Slovenia,
    [JsonStringEnumMemberName("Solomon Islands")]
    SolomonIslands,
    Somalia,
    [JsonStringEnumMemberName("South Africa")]
    SouthAfrica,
    [JsonStringEnumMemberName("South Georgia and the South Sandwich Islands")]
    SouthGeorgiaAndTheSouthSandwichIslands,
    [JsonStringEnumMemberName("South Sudan")]
    SouthSudan,
    Spain,
    [JsonStringEnumMemberName("Sri Lanka")]
    SriLanka,
    Sudan,
    Suriname,
    [JsonStringEnumMemberName("Svalbard and Jan Mayen")]
    SvalbardAndJanMayen,
    Sweden,
    Switzerland,
    [JsonStringEnumMemberName("Syrian Arab Republic")]
    SyrianArabRepublic,
    Taiwan,
    Tajikistan,
    [JsonStringEnumMemberName("Tanzania (United Republic of)")]
    TanzaniaUnitedRepublicOf,
    Thailand,
    [JsonStringEnumMemberName("Timor-Leste")]
    TimorLeste,
    Togo,
    Tokelau,
    Tonga,
    [JsonStringEnumMemberName("Trinidad and Tobago")]
    TrinidadAndTobago,
    Tunisia,
    Turkey,
    Turkmenistan,
    [JsonStringEnumMemberName("Turks and Caicos Islands")]
    TurksAndCaicosIslands,
    Tuvalu,
    Uganda,
    Ukraine,
    [JsonStringEnumMemberName("United Arab Emirates")]
    UnitedArabEmirates,
    [JsonStringEnumMemberName("United Kingdom of Great Britain and Northern Ireland")]
    UnitedKingdomOfGreatBritainAndNorthernIreland,
    [JsonStringEnumMemberName("United States Minor Outlying Islands")]
    UnitedStatesMinorOutlyingIslands,
    [JsonStringEnumMemberName("United States of America")]
    UnitedStatesOfAmerica,
    Uruguay,
    Uzbekistan,
    Vanuatu,
    [JsonStringEnumMemberName("Venezuela (Bolivarian Republic of)")]
    VenezuelaBolivarianRepublicOf,
    [JsonStringEnumMemberName("Viet Nam")]
    VietNam,
    [JsonStringEnumMemberName("Virgin Islands (British)")]
    VirginIslandsBritish,
    [JsonStringEnumMemberName("Virgin Islands (U.S.)")]
    VirginIslandsUS,
    [JsonStringEnumMemberName("Wallis and Futuna")]
    WallisAndFutuna,
    [JsonStringEnumMemberName("Western Sahara")]
    WesternSahara,
    Yemen,
    Zambia,
    Zimbabwe
}