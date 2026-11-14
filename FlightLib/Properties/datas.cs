using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////// BASE DE DATOS /////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class datas
    {
        /////////////////////////////////////////////////////// Diccionario ID ///////////////////////////////////////////////////////////////////////
        private static Dictionary<string, string> id_databases = new Dictionary<string, string>
        {
             // España
        { "IB", "Iberia" }, { "I2", "Iberia Express" }, { "VY", "Vueling" },
        { "UX", "Air Europa" }, { "V7", "Volotea" }, { "YW", "Air Nostrum" },
        { "NT", "Binter Canarias" },

        // Europa
        { "FR", "Ryanair" }, { "U2", "easyJet" }, { "W6", "Wizz Air" },
        { "LH", "Lufthansa" }, { "EW", "Eurowings" }, { "DE", "Condor" },
        { "AF", "Air France" }, { "TO", "Transavia France" }, { "HV", "Transavia" },
        { "KL", "KLM" }, { "BA", "British Airways" }, { "VS", "Virgin Atlantic" },
        { "LS", "Jet2" }, { "BY", "TUI Airways" }, { "X3", "TUIfly" },
        { "EI", "Aer Lingus" }, { "LX", "Swiss" }, { "OS", "Austrian" },
        { "SN", "Brussels Airlines" }, { "TP", "TAP Air Portugal" },
        { "AZ", "ITA Airways" }, { "AY", "Finnair" }, { "SK", "SAS" },
        { "DY", "Norwegian" }, { "D8", "Norwegian Air International" },
        { "FI", "Icelandair" }, { "WF", "Widerøe" }, { "LO", "LOT Polish" },
        { "OK", "Czech Airlines" }, { "A3", "Aegean" }, { "BT", "airBaltic" },
        { "JU", "Air Serbia" }, { "OU", "Croatia Airlines" }, { "RO", "TAROM" },
        { "FB", "Bulgaria Air" }, { "KM", "Air Malta" }, { "TK", "Turkish Airlines" },
        { "PC", "Pegasus" }, { "XQ", "SunExpress" }, { "SU", "Aeroflot" },
        { "S7", "S7 Airlines" },

        // Oriente Medio y África
        { "EK", "Emirates" }, { "QR", "Qatar Airways" }, { "EY", "Etihad" },
        { "FZ", "flydubai" }, { "G9", "Air Arabia" }, { "SV", "Saudia" },
        { "WY", "Oman Air" }, { "GF", "Gulf Air" }, { "KU", "Kuwait Airways" },
        { "RJ", "Royal Jordanian" }, { "ME", "Middle East Airlines" },
        { "LY", "El Al" }, { "MS", "EgyptAir" }, { "AT", "Royal Air Maroc" },
        { "TU", "Tunisair" }, { "ET", "Ethiopian Airlines" }, { "SA", "South African Airways" },

        // América
        { "AA", "American Airlines" }, { "DL", "Delta" }, { "UA", "United" },
        { "WN", "Southwest" }, { "B6", "JetBlue" }, { "AS", "Alaska Airlines" },
        { "NK", "Spirit" }, { "F9", "Frontier" }, { "HA", "Hawaiian Airlines" },
        { "AC", "Air Canada" }, { "WS", "WestJet" }, { "AM", "Aeroméxico" },
        { "CM", "Copa Airlines" }, { "AV", "Avianca" }, { "LA", "LATAM" },
        { "JJ", "LATAM Brasil" }, { "G3", "Gol" }, { "AD", "Azul" },
        { "AR", "Aerolíneas Argentinas" },

        // Asia y Oceanía
        { "AI", "Air India" }, { "6E", "IndiGo" }, { "SG", "SpiceJet" },
        { "SQ", "Singapore Airlines" }, { "TR", "Scoot" }, { "3K", "Jetstar Asia" },
        { "CX", "Cathay Pacific" }, { "CA", "Air China" }, { "CZ", "China Southern" },
        { "MU", "China Eastern" }, { "CI", "China Airlines" }, { "BR", "EVA Air" },
        { "JL", "Japan Airlines" }, { "NH", "ANA" }, { "KE", "Korean Air" },
        { "OZ", "Asiana" }, { "TG", "Thai Airways" }, { "MH", "Malaysia Airlines" },
        { "AK", "AirAsia" }, { "GA", "Garuda Indonesia" }, { "PR", "Philippine Airlines" },
        { "VN", "Vietnam Airlines" }, { "UL", "SriLankan" }, { "PK", "PIA" },
        { "QF", "Qantas" }, { "JQ", "Jetstar" }, { "VA", "Virgin Australia" },
        { "NZ", "Air New Zealand" }
        };

        ////////////////////////////////////////////////////////////    Metodos booleanos   /////////////////////////////////////////////////////////////////////
        public bool codecheck(string code)                                                  //  Revisar si el ID esta dentro de la base de datos
        {
            return id_databases.ContainsKey(code);                                          //  Devuelve en caso afirmativo un bool True, en caso negativo devuelve un False
        }
    }
}
