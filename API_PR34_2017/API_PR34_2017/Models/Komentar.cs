using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Komentar
    {
        public string Kupacid { get; set; } //kupac koji je osatavio komenatr
        public string Id { get; set; } //id komentara da bi se mogao izmeniti
        public string Manifestacija { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
        public bool Odobren { get; set; }
        public Komentar()
        {

        }

        public Komentar( string manifestacija, string kupacid, string tekst, int ocena, bool odobren,string id)
        {
            Kupacid = kupacid;
            Manifestacija = manifestacija;
            Tekst = tekst;
            Ocena = ocena;
            Odobren = odobren;
            Id = id;
        }

        public override string ToString()
        {
            return Id+";"+Manifestacija+";"+Kupacid+";"+Tekst+";"+Ocena+";"+Odobren.ToString();
        }
    }
}