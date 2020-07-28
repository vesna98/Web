using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Komentar
    {
        public string Kupacid { get; set; } //kupac koji je osatavio komenatr
        public string Manifestacija { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
    }
}