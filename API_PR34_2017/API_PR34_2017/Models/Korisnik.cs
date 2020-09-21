using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Korisnik
    {
        [Required, MinLength(3)]
        [RegularExpression("^[a-zA-Z0-9]*$")]//, ErrorMessage = "Mogu samo slova i brojevi.")]
        public string Korisnickoime { get; set; }
        [Required ,MinLength(4)]
        [RegularExpression("^[a-zA-Z0-9]*$")]// ErrorMessage = "Mogu samo slova i brojevi.")]
        public string Lozinka { get; set; }
        [Required, MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$")]//ErrorMessage = "Mogu samo slova.")]
        public string Ime { get; set; }
        [Required, MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$")]// ErrorMessage = "Mogu samo slova.")]
        public string Prezime { get; set; }
        [Required]
        public PolType Pol { get; set; }
        [Required]
        public string Datumrodjenja { get; set; }
        public UlogaType Uloga { get; set; }
        public List<Karta> Rezervisanekarte { get; set; } = new List<Karta>();//ako je KUPAC sifra karte , bilo koji status
        public List<Manifestacija> Manifestacije { get; set; } = new List<Manifestacija>();// = Data.ReadFest("~/App_Data/manifestacije.txt").FindAll(x=>x.Prodavac.Equals(ko)); //ako je Prodavac, id manifestacije
        public double Sakupljenibodovi { get; set; }       //za KUPAC
        // public TipKorisnika Tip { get; set; }
        public TipIme Tip { get; set; }
        public bool Obrisan { get; set; }
        public bool Loggedin { get; set; }
        public bool Blokiran { get; set; }

        public Korisnik()
        {
            //Rezervisanekarte = new List<string>();
            //Sakupljenibodovi = 0;
            //Obrisan = false;
            //// TipKorisnika tip = new TipKorisnika();
            //Tip = TipIme.Nepoznat;
            //Uloga = UlogaType.Kupac;
            Loggedin = false;

        }

        public Korisnik(string korisnickoime, string lozinka, string ime, string prezime, PolType pol, string datumrodjenja, double sakupljenibodovi, TipIme tip, UlogaType uloga, bool obrisan,bool blokiran)
        {
            Korisnickoime = korisnickoime;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Datumrodjenja = datumrodjenja;
            Uloga = uloga;
            Sakupljenibodovi = sakupljenibodovi;
            Tip = tip;
            Obrisan = obrisan;
            Loggedin = false;
            Blokiran =blokiran;
            Manifestacije = Data.ReadFest("~/App_Data/manifestacije.txt").FindAll(x => x.Prodavac.Equals(korisnickoime) && !x.Obrisan);
            Rezervisanekarte= Data.ReadKarte("~/App_Data/karte.txt").FindAll(x=>x.Korisnikid.Equals(korisnickoime) && !x.Obrisana);
        }

        public override string ToString()
        {
            return Korisnickoime + ";" + Lozinka + ";" + Ime + ";" + Prezime + ";" + Pol.ToString() + ";" + Datumrodjenja + ";" + Sakupljenibodovi.ToString() + ";" + Tip + ";" + Uloga.ToString() + ";" + Obrisan.ToString() + ";" +Blokiran.ToString();
        }
    }
}