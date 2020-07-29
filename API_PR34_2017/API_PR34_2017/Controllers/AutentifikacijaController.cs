using API_PR34_2017.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace API_PR34_2017.Controllers
{
    public class AutentifikacijaController : ApiController
    {
        public int Post([FromBody]Korisnik korisnik)
        {

            // if (String.IsNullOrEmpty(korisnik.Korisnickoime) || String.IsNullOrEmpty(korisnik.Lozinka) ||
            //   String.IsNullOrEmpty(korisnik.Ime) || String.IsNullOrEmpty(korisnik.Prezime) ||
            //    String.IsNullOrEmpty(korisnik.Datumrodjenja))
            // {
            //     return false;
            // }


            // Regex r1 = new Regex("[0-9a-zA-Z]{3,}"); //korisnicko ime 
            // Regex r2 = new Regex("[0-9a-zA-Z]{4,}");//lozinka
            //// Regex r3 = new Regex("[0-9]{13}");//datum rodj
            // Regex r3 = new Regex("[a-zA-Z]{3,}");//ime i prezime



            // if (!r1.IsMatch(korisnik.Korisnickoime) || !r2.IsMatch(korisnik.Lozinka) || !r3.IsMatch(korisnik.Ime) || !r3.Match(korisnik.Prezime))
            // {
            //     return false;
            // }
            if (ModelState.IsValid)
            {
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
                    return 1;                   //USPESNO UPISAN 1
                }
                return 2;
            }
            return 2;   //POSTOJI VEC

        }

        public int Put([FromBody]Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                DateTime timestamp;
                if (!DateTime.TryParseExact(korisnik.Datumrodjenja, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out timestamp))
                {
                    return 0;   //datum nije dobar
                                // return false;
                }
                Data.SaveUser(korisnik);
                //dodaje se ako postoji
                return 1;
            }
            return 0;//nije validan unos
        }
    }
}
