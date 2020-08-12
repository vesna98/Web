using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Karta
    {
        public Karta(string nazivmanifestacije, string datummanifestacije, double cena, string kupac, string korisnikid, StatusKarte status, TypeKarte tipkarte)
        {
            Nazivmanifestacije = nazivmanifestacije;
            Datummanifestacije = datummanifestacije;
            Cena = cena;
            Kupac = kupac;
            Korisnikid = korisnikid;
            Status = status;
            Tipkarte = tipkarte;
            Obrisana = false;
            Idkarte = GetRandomString(10);
        }

        internal static string GetRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }

        public string Idkarte { get; set; }
        public string Nazivmanifestacije { get; set; }  //manifestacija za koju je rez
        public string Datummanifestacije { get; set; } //vrme i datum manif
        public double Cena { get; set; }
        public string Kupac { get; set; }   //ime i prezime
        public string Korisnikid { get; set; }   //username korisnika
        public StatusKarte Status { get; set; }
        public TypeKarte Tipkarte { get; set; }
        public bool Obrisana { get; set; }


        public override string ToString()
        {
            return Idkarte+";"+Korisnikid+";"+Tipkarte.ToString()+";"+Status.ToString()+";"+Cena.ToString()+";"+Nazivmanifestacije+";"+Datummanifestacije+";"+Obrisana.ToString();
        }
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