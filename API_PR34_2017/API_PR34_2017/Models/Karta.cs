using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Karta
    {
        public string Idkarte { get; set; }
        public string Nazivmanifestacije { get; set; }  //manifestacija za koju je rez
        public DateTime Datummanifestacije { get; set; } //vrme i datum manif
        public double Cena { get; set; }
        public string Kupac { get; set; }   //ime i prezime
        public StatusKarte Status { get; set; }
        public TypeKarte Tipkarte { get; set; }
    }
    public enum StatusKarte
    {
        Rezervisana,
        Odustanak
    }

    public enum TypeKarte
    {
        VIP,
        REGULAR,
        FANPIT
    }
}