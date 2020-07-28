using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class TipKorisnika
    {
        public TipIme Imetipa { get; set; }
        public double Popust { get; set; }
        public int Trazenibodovi { get; set; }


        public TipKorisnika()
        {
            Imetipa = TipIme.Nepoznat;
            Popust = 0;
            Trazenibodovi = 0;
        }
    }
    public enum TipIme
    {
        Zlatni, Srebrni, Bronzani, Nepoznat
    }
}