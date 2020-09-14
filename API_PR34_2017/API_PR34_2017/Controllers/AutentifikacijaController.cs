using API_PR34_2017.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace API_PR34_2017.Controllers
{
    public class AutentifikacijaController : ApiController
    {
        public int Post([FromBody]Korisnik korisnik)
        {
            
            if (ModelState.IsValid)
            {
                korisnik.Rezervisanekarte = new List<string>();
                korisnik.Sakupljenibodovi = 0;
                korisnik.Obrisan = false;
                korisnik.Tip = TipIme.Nepoznat;
                korisnik.Uloga = UlogaType.Kupac;

                DateTime timestamp;
                if(!DateTime.TryParseExact(korisnik.Datumrodjenja, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out timestamp))
                {
                    return 0;   //datum nije dobar
                   // return false;
                }
                Dictionary<string, Korisnik> korisnici = Data.ReadUser("~/App_Data/korisnici.txt");
                bool postoji = false;
                foreach (string kime in korisnici.Keys)
                {
                    if (kime.Equals(korisnik.Korisnickoime))
                    {
                        //return false;
                        postoji = true;
                        break;
                    }
                }
                if (!postoji)
                {
                    Data.SaveUser(korisnik);
                    //dodaje se ako postoji

                    //Korisnik user = new Korisnik();
                    //HttpContext.Current.Session["user"] = korisnik;         //SESIJA
                    //Session["user"] = user;

                    return 1;                   //USPESNO UPISAN 1
                }
                return 2;
            }
            return 2;   //POSTOJI VEC

        }

        public Korisnik Put([FromBody]Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                DateTime timestamp;
                if (!DateTime.TryParseExact(korisnik.Datumrodjenja, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out timestamp))
                {
                    return null;   //datum nije dobar
                                // return false;
                }
                Data.SaveUser(korisnik);

                List<Karta> svekarte = new List<Karta>();
                svekarte = Data.ReadKarte("~/App_Data/karte.txt");
                foreach (Karta karta in svekarte)
                {
                    if (karta.Korisnikid.Equals(korisnik.Korisnickoime))
                    {
                        karta.Kupac = korisnik.Ime + " " + korisnik.Prezime;
                        Data.SaveKartu(karta);
                    }
                }

                //dodaje se ako postoji
                return korisnik;
            }
            return null;//nije validan unos
        }
    }
}
