﻿using System;
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
        public List<string> Rezervisanekarte { get; set; } //ako je KUPAC sifra karte , bilo koji status
        public List<string> Manifestacije { get; set; } //ako je Prodavac, naziv manifestacije
        public int Sakupljenibodovi { get; set; }       //za KUPAC
        // public TipKorisnika Tip { get; set; }
        public TipIme Tip { get; set; }
        public bool Obrisan { get; set; }

        public Korisnik()
        {
            //Rezervisanekarte = new List<string>();
            //Sakupljenibodovi = 0;
            //Obrisan = false;
            //// TipKorisnika tip = new TipKorisnika();
            //Tip = TipIme.Nepoznat;
            //Uloga = UlogaType.Kupac;
        }

        public Korisnik(string korisnickoime, string lozinka, string ime, string prezime, PolType pol, string datumrodjenja, int sakupljenibodovi, TipIme tip, UlogaType uloga, bool obrisan)
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
        }

        public override string ToString()
        {
            return Korisnickoime + ";" + Lozinka + ";" + Ime + ";" + Prezime + ";" + Pol.ToString() + ";" + Datumrodjenja + ";" + Sakupljenibodovi.ToString() + ";" + Tip + ";" + Uloga.ToString() + ";" + Obrisan.ToString();
        }
    }
}