using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Manifestacija
    {
        public string Naziv { get; set; }
        public TypeManifestacije Tipmanifestacije { get; set; }
        public int Brojmesta { get; set; }
        // public DateTime Datumivreme { get; set; }
        public string Datumivreme { get; set; }
        public double Cenaregular { get; set; }
        public double Cenafanpit { get; set; }
        public double Cenavip { get; set; }
        public StatusType Status { get; set; }
        public Mesto Mestoodrzavanja { get; set; }
        public string Poster { get; set; }     //SLIKA TIP
        public string Prodavac { get; set; }
        public int Kupljeno { get; set; }
        public int Ocena { get; set; }
        public bool Obrisan { get; set; }

        public Manifestacija()
        {
            Status = StatusType.Neaktivno;
            Kupljeno = 0;
            Ocena = 0;
            Obrisan = false;
        }

        public Manifestacija(string prodavac, string naziv, TypeManifestacije tipmanifestacije, int brojmesta, string datumivreme, double cenaregular, Mesto mestoodrzavanja, string poster,int ocena,double cenafanpit,double cenavip,int kupljeno,bool obrisan)
        {
            Naziv = naziv;
            Tipmanifestacije = tipmanifestacije;
            Brojmesta = brojmesta;
            Datumivreme = datumivreme;
            Cenaregular = cenaregular;
            Mestoodrzavanja = mestoodrzavanja;
            Poster = poster;
            Prodavac = prodavac;
            Status = StatusType.Neaktivno;
            Kupljeno = kupljeno;
            Ocena = ocena;
            Cenafanpit = cenafanpit;
            Cenavip = cenavip;
            Obrisan = obrisan;
        }

        public override string ToString()
        {
            return Prodavac + ";" + Naziv + ";" + Tipmanifestacije.ToString() + ";" + Brojmesta.ToString() + ";" + Datumivreme + ";" + Cenaregular.ToString() + ";" + Mestoodrzavanja.Ulicabroj + ";" + Mestoodrzavanja.Grad + ";" + Mestoodrzavanja.Postanskibroj + ";" + Poster + ";" + Status.ToString() + ";" + Kupljeno + ";" +Ocena.ToString()+";" + Cenafanpit.ToString() + ";" + Cenavip.ToString() + ";" +Obrisan.ToString();
        }
    }
    public enum TypeManifestacije
    {
        Koncert,
        Festival,
        Pozoriste
    }
    public enum StatusType
    {
        Aktivno,
        Neaktivno
    }
}