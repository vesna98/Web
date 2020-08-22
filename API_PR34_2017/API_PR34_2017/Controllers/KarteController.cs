using API_PR34_2017.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace API_PR34_2017.Controllers
{
    [RoutePrefix("api/Karte")]
    public class KarteController : ApiController
    {
        [Route("KartePrikazKupac")]
        [HttpPost]
        public HttpResponseMessage KartePrikazKupac(JObject jsonResult)
        {
            string filter = (string)jsonResult["filter"];
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (filter.Equals("Sve") && !k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (!k.Obrisana && k.Status.ToString().Equals(filter) && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                //***********
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana") && filter.Equals("Sve"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana") && k.Status.ToString().Equals(filter))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana && filter.Equals("Sve"))
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana && k.Status.ToString().Equals(filter))
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }
            }

            var output = JsonConvert.SerializeObject(odabrane);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("SveKarte")]
        [HttpPost]
        public HttpResponseMessage SveKarte(JObject jsonResult)
        {
            //string filter = (string)jsonResult["filter"];
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach(Manifestacija fest in festovi)
                    {
                        if(k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }
                    
                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var output = JsonConvert.SerializeObject(odabrane);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KomentarOcena")]
        [HttpPost]
        public HttpResponseMessage KomentarOcena(JObject jsonResult)
        {
            string ocena = (string)jsonResult["ocena"];
            string manifestacija = (string)jsonResult["mani"];
            string komentar = (string)jsonResult["comm"];
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            //manifestacija
            string mani = manifestacija.Split(';')[0];
            string dat = manifestacija.Split(';')[1];

            //List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }
                //if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                //{
                //    odabrane.Add(k);
                //}
            }
            var odgovor = "Uspesno zabelezeno.";
            if ((string.IsNullOrWhiteSpace(komentar.Trim()) || string.IsNullOrEmpty(komentar.Trim())) && ocena.Equals("0"))
            {
                odgovor = "Niste uneli komentar,ni ocenili";
            } else if (komentar.Contains(';'))
            {
                odgovor = "Komentar ne sme da sadrzi ';'";
            }
            else
            {
                Komentar komentarObj = new Komentar(manifestacija, korisnik, komentar, int.Parse(ocena), false,GetRandomString(5),false);
                Data.SaveKomentar(komentarObj);
            }

            var output = JsonConvert.SerializeObject(odgovor);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
        [Route("PrikaziKomentar")]
        [HttpPost]
        public HttpResponseMessage PrikaziKomenter(JObject jsonResult)
        {
                string naziv = (string)jsonResult["naziv"];
                string datum = (string)jsonResult["datum"];
            
            string nazivIdatum = naziv + ";" + datum;
            List<Komentar> komentari = Data.ReadKomentar("~/App_Data/komentari.txt");
            List<string> odabrani = new List<string>();
            foreach(Komentar commnent in komentari)
            {
                if (commnent.Manifestacija.Equals(nazivIdatum) && commnent.Odobren)//dodati da je odobren
                {
                    odabrani.Add(commnent.Tekst);
                }
            }

            var output = JsonConvert.SerializeObject(odabrani);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
        private static string GetRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }
        [Route("KomentariCekanje")]
        [HttpPost]
        public HttpResponseMessage KomentariCekanje(JObject jsonResult)
        {
            string naziv = (string)jsonResult["naziv"];
            string datum = (string)jsonResult["datum"];
            
            string nazivIdatum = naziv + ";" + datum;
            List<Komentar> komentari = Data.ReadKomentar("~/App_Data/komentari.txt");
            List<Komentar> odabrani = new List<Komentar>();
            foreach (Komentar commnent in komentari)
            {
                if (commnent.Manifestacija.Equals(nazivIdatum) && !commnent.Odobren)//dodati koji NIJE odobren
                {
                    if (!commnent.Obrisi)
                        odabrani.Add(commnent);
                }
            }

            var output = JsonConvert.SerializeObject(odabrani);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
        [Route("KomentariObrisan")]
        [HttpPost]
        public HttpResponseMessage KomentariObrisan(JObject jsonResult)
        {
            string naziv = (string)jsonResult["naziv"];
            string datum = (string)jsonResult["datum"];
            string komID = (string)jsonResult["comm"];
  
            string nazivIdatum = naziv + ";" + datum;
            List<Komentar> komentari = Data.ReadKomentar("~/App_Data/komentari.txt");
            List<Komentar> odabrani = new List<Komentar>();
            foreach (Komentar commnent in komentari)
            {
                if (commnent.Manifestacija.Equals(nazivIdatum) && !commnent.Odobren)//dodati koji NIJE odobren
                {
                    if (commnent.Id.Equals(komID))
                    {
                        commnent.Obrisi = true;         //BRISANJE
                        Data.SaveKomentar(commnent);
                    }

                    if (!commnent.Obrisi && !commnent.Odobren)            //KOJI NISUUU OBRISANI
                        odabrani.Add(commnent);
                    
                }
            }

            var output = JsonConvert.SerializeObject(odabrani);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KomentariPrihvati")]
        [HttpPost]
        public HttpResponseMessage KomentariPrihvati(JObject jsonResult)
        {
            string naziv = (string)jsonResult["naziv"];
            string datum = (string)jsonResult["datum"];
            string komID = (string)jsonResult["comm"];

            string nazivIdatum = naziv + ";" + datum;
            List<Komentar> komentari = Data.ReadKomentar("~/App_Data/komentari.txt");
            List<Komentar> odabrani = new List<Komentar>();
            foreach (Komentar commnent in komentari)
            {
                if (commnent.Manifestacija.Equals(nazivIdatum) && !commnent.Odobren)//dodati koji NIJE odobren
                {
                    //-----
                    if (commnent.Id.Equals(komID))
                    {
                        commnent.Odobren = true;         //ODOBRAVANJE
                        Data.SaveKomentar(commnent);
                        //continue;
                    }

                    if (!commnent.Obrisi && !commnent.Odobren)            //KOJI NISUUU OBRISANI
                        odabrani.Add(commnent);
                }
            }

            var output = JsonConvert.SerializeObject(odabrani);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("ObrisiKartu")]
        [HttpPost]
        public HttpResponseMessage ObrisiKartu(JObject jsonResult)
        {
            string ID = (string)jsonResult["idkarte"];
            string filter = (string)jsonResult["filter"];
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA

            double cenaKarte=0;

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");
            foreach(Karta k in karte)
            {
                if (k.Idkarte.Equals(ID))
                {
                    k.Obrisana = true;
                    cenaKarte = k.Cena;
                    k.MogucOdustanak = true;

                    //treba smanjiti broj kupljenih 
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach(Manifestacija fest in festovi)
                    {
                        if(fest.Naziv.Equals(k.Nazivmanifestacije) && fest.Datumivreme.Equals(k.Datummanifestacije))
                        {
                            fest.Kupljeno -= 1;
                            Data.SaveFest(fest);
                        }
                    }
                    //----------------------------
                    Data.SaveKartu(k);
                    break;
                }
            }
            //broj_izgubljenih_bodova = cena_jedne_karte/1000 * 133 * 4
            double brojIzgubljenihBodova = cenaKarte / 1000 * 133 * 4;
            Korisnik kupac = new Korisnik();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                if (u.Korisnickoime.Equals(korisnik))
                {
                    kupac = u;
                }
            }
            kupac.Sakupljenibodovi = kupac.Sakupljenibodovi - brojIzgubljenihBodova;
            Data.SaveUser(kupac);

            ///kopirano od gore
            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);

                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (filter.Equals("Sve") && !k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
                }
                else if (!k.Obrisana && k.Status.ToString().Equals(filter) && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
                }
            }

            var output = JsonConvert.SerializeObject(odabrane);

            return Request.CreateResponse(HttpStatusCode.OK, output);


        }

        [Route("KupacFilterTip")]
        [HttpPost]
        public HttpResponseMessage KupacFilterTip(JObject jsonResult)
        {
            string filter = (string)jsonResult["filter"];
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }

                //sve regular vip fanpit
                if (filter.Equals("Sve") && !k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (!k.Obrisana && k.Tipkarte.ToString().Equals(filter.ToUpper()) && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana") && k.Tipkarte.ToString().Equals(filter.ToUpper()))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana") && filter.Equals("Sve"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana && filter.Equals("Sve"))
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana && k.Tipkarte.ToString().Equals(filter.ToUpper()))
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var output = JsonConvert.SerializeObject(odabrane);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KupacNazivAZ")]
        [HttpPost]
        public HttpResponseMessage KupacNazivAZ(JObject jsonResult)
        {
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var sortirano = odabrane.OrderBy(i=>i.Nazivmanifestacije);

            var output = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KupacNazivZA")]
        [HttpPost]
        public HttpResponseMessage KupacNazivZA(JObject jsonResult)
        {
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var sortirano = odabrane.OrderByDescending(i => i.Nazivmanifestacije);

            var output = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KupacCenaRastuce")]
        [HttpPost]
        public HttpResponseMessage KupacCenaRastuce(JObject jsonResult)
        {
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var sortirano = odabrane.OrderBy(i => i.Cena);

            var output = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KupacCenaOpadajuce")]
        [HttpPost]
        public HttpResponseMessage KupacCenaOpadajuce(JObject jsonResult)
        {
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var sortirano = odabrane.OrderByDescending(i => i.Cena);

            var output = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KupacDatumKasnije")]
        [HttpPost]
        public HttpResponseMessage KupacDatumKasnije(JObject jsonResult)
        {
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var sortirano = odabrane.OrderByDescending(i => DateTime.ParseExact(i.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None));

            var output = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KupacDatumRanije")]
        [HttpPost]
        public HttpResponseMessage KupacDatumRanije(JObject jsonResult)
        {
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);
                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

            var sortirano = odabrane.OrderBy(i => DateTime.ParseExact(i.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None));

            var output = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KupacPretraga")]
        [HttpPost]
        public HttpResponseMessage KupacPretraga(JObject jsonResult)
        {
            string naziv = (string)jsonResult["naziv"];
            string datumOD = (string)jsonResult["datumOD"];
            string datumDO = (string)jsonResult["datumDO"];
            int cenaOD;
            bool cena1 = Int32.TryParse((string)jsonResult["cenaOD"], out cenaOD);
            int cenaDO;
            bool cena2 = Int32.TryParse((string)jsonResult["cenaDO"], out cenaDO);

            string korisnik = (string)jsonResult["korisnik"];       //SESIJA
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");

            DateTime sad = DateTime.Now;
            int result;
            foreach (Karta k in karte)
            {
                DateTime datum = DateTime.ParseExact(k.Datummanifestacije, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);

                result = DateTime.Compare(datum, sad);

                if (result < 0)
                {
                    //prosla
                    if (!k.Status.ToString().Equals("Rezervisana"))
                    {
                        k.Status = StatusKarte.Rezervisana;
                        Data.SaveKartu(k);
                    }

                }
                else if (result > 0)
                {
                    //slede tek, moguce je odustati
                    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                    if (preostaliDani >= 7)
                    {
                        if (!k.MogucOdustanak && !k.Obrisana)
                        {
                            k.MogucOdustanak = true;
                            Data.SaveKartu(k);
                        }
                    }
                }


                if (!k.Obrisana && k.Korisnikid.Equals(user.Korisnickoime))
                {
                    odabrane.Add(k);
                }
                else if (user.Uloga.ToString().Equals("Prodavac") && !k.Obrisana && k.Status.ToString().Equals("Rezervisana"))//proveriti da li je njihova manifestacija ta koja je rezervisana
                {
                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (k.Nazivmanifestacije.Equals(fest.Naziv) && k.Datummanifestacije.Equals(fest.Datumivreme))
                        {
                            odabrane.Add(k);
                        }
                    }

                }
                else if (user.Uloga.ToString().Equals("Administrator") && !k.Obrisana)
                {
                    odabrane.Add(k);    //dodaju se sve/svi statusi
                }

            }

                DateTime datOD;
            DateTime datDO;
            bool dat1 = false;
            bool dat2 = false;
            if (DateTime.TryParseExact(datumOD, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datOD))
            {
                dat1 = true;
            }
            if (DateTime.TryParseExact(datumDO, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datDO))
            {
                dat2 = true;
            }

            bool oba = false;
            if (dat1 && dat2)
                oba = true;

            //--------------
            int treba = 0;
            if (cena1 && !cena2)
            {
                odabrane = odabrane.Where(u => u.Cena >= cenaOD).ToList();
            }
            else if (!cena1 && cena2)
            {
                odabrane = odabrane.Where(u => u.Cena <= cenaDO).ToList();
            }
            else if (cena1 && cena2)
            {
                odabrane = odabrane.Where(u => u.Cena >= cenaOD).ToList();
                odabrane = odabrane.Where(u => u.Cena <= cenaDO).ToList();
            }

            //datum
            if (oba)
            {
                int result2 = DateTime.Compare(datOD, datDO);

                if (result2 < 0) //NE MOZE,NIJE DOBRO UNETO relationship = "is earlier than";
                {
                    odabrane = odabrane.FindAll(u => treba <= DateTime.Compare(DateTime.ParseExact(u.Datummanifestacije.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD));
                    odabrane = odabrane.FindAll(u => treba >= DateTime.Compare(DateTime.ParseExact(u.Datummanifestacije.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datDO));

                }

                else if (result2 == 0)  //OKEJ,JEDNAKO SU DATUMI,ONDA SAMO TOG DANA STO JE relationship = "is the same time as";
                {
                    // int treba = 0;
                    odabrane = odabrane.FindAll(u => treba == DateTime.Compare(DateTime.ParseExact(u.Datummanifestacije.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD));
                }
                else//RADIMO ONDA POREDJENJA DA BUDU POSLE PRVOG I PRE DRUGOG relationship = "is later than";
                {

                }

            }

            // int treba = 0;
            if (dat1)
            {
                odabrane = odabrane.FindAll(u => treba <= DateTime.Compare(DateTime.ParseExact(u.Datummanifestacije.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD));
            }

            if (dat2)
            {
                odabrane = odabrane.FindAll(u => treba >= DateTime.Compare(DateTime.ParseExact(u.Datummanifestacije.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datDO));
            }
           
            if (!string.IsNullOrWhiteSpace(naziv.Trim()))
                odabrane = odabrane.Where(u => u.Nazivmanifestacije.ToUpper().Contains(naziv.ToUpper())).ToList();


            var output = JsonConvert.SerializeObject(odabrane);
            
            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
    }
}
