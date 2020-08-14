﻿using API_PR34_2017.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
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


                if (filter.Equals("Sve") && !k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
                }
                else if (!k.Obrisana && k.Status.ToString().Equals(filter) && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
                }
                //else if (filter.Equals("Odustanak") && !k.Obrisana)
                //{
                //    odabrane.Add(k);
                //}


                //if (filter.Equals("Sve"))
                //{
                //    if (result < 0)
                //    {
                //        //prosla manifestacija
                //        if (!k.Status.ToString().Equals("Rezervisana") && !k.Obrisana)
                //        {
                //            k.Status = StatusKarte.Rezervisana;
                //            Data.SaveKartu(k);
                //        }
                //    }
                //    else if(result>0)
                //    {
                //        double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                //        if (preostaliDani >= 7)
                //        {
                //            if (k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //            {
                //                k.MogucOdustanak = true;
                //                Data.SaveKartu(k);
                //            }
                //        }
                //        else
                //        {
                //            if (!k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //            {
                //                k.MogucOdustanak = false;
                //                Data.SaveKartu(k);
                //            }
                //        }
                //    }
                //    //Data.SaveKartu(k);
                //    odabrane.Add(k);
                //}
                //if (result < 0 && !filter.Equals("Sve"))
                //{
                //    if (!k.Status.ToString().Equals("Rezervisana") && !k.Obrisana)//stavlja na rezervisano ako nije
                //    {
                //        k.Status = StatusKarte.Rezervisana;
                //        Data.SaveKartu(k);
                //    }

                //    if (filter.Equals(k.Status.ToString()) && !k.Obrisana )  //tog statusa i da nije obrisana
                //    {
                //       // Data.SaveKartu(k);
                //        odabrane.Add(k);
                //    }

                //}else if(result>0 && !filter.Equals("Sve"))
                //{
                //    //treba da bude

                //    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                //    if (preostaliDani>= 7) {
                //        if (k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //        {
                //            k.MogucOdustanak = true;
                //            Data.SaveKartu(k);
                //        }
                //    }
                //    else
                //    {
                //        if (!k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //        {
                //            k.MogucOdustanak = false;
                //            Data.SaveKartu(k);
                //        }
                //    }

                //    if (filter.Equals(k.Status.ToString()) && !k.Obrisana)  //tog statusa i da nije obrisana
                //    {

                //        odabrane.Add(k);
                //    }
                //}
            }

            var output = JsonConvert.SerializeObject(odabrane);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("ObrisiKartu")]
        [HttpPost]
        public HttpResponseMessage ObrisiKartu(JObject jsonResult)
        {
            string ID = (string)jsonResult["idkarte"];
            string filter = (string)jsonResult["filter"];
            string korisnik = (string)jsonResult["korisnik"];       //SESIJA

            List<Karta> odabrane = new List<Karta>();
            List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");
            foreach(Karta k in karte)
            {
                if (k.Idkarte.Equals(ID))
                {
                    k.Obrisana = true;
                    k.MogucOdustanak = true;
                    Data.SaveKartu(k);
                    break;
                }
            }

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

                //if (filter.Equals("Sve"))
                //{
                //    if (result < 0)
                //    {
                //        //prosla manifestacija
                //        if (!k.Status.ToString().Equals("Rezervisana") && !k.Obrisana)
                //        {
                //            k.Status = StatusKarte.Rezervisana;
                //            Data.SaveKartu(k);
                //        }
                //    }
                //    else if (result > 0)
                //    {
                //        double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                //        if (preostaliDani >= 7)
                //        {
                //            if (k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //            {
                //                k.MogucOdustanak = true;
                //                Data.SaveKartu(k);
                //            }
                //        }
                //        else
                //        {
                //            if (!k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //            {
                //                k.MogucOdustanak = false;
                //                Data.SaveKartu(k);
                //            }
                //        }
                //    }
                //    //Data.SaveKartu(k);
                //    odabrane.Add(k);
                //}
                //if (result < 0 && !filter.Equals("Sve"))
                //{
                //    if (!k.Status.ToString().Equals("Rezervisana") && !k.Obrisana)//stavlja na rezervisano ako nije
                //    {
                //        k.Status = StatusKarte.Rezervisana;
                //        Data.SaveKartu(k);
                //    }

                //    if (filter.Equals(k.Status.ToString()) && !k.Obrisana)  //tog statusa i da nije obrisana
                //    {
                //        // Data.SaveKartu(k);
                //        odabrane.Add(k);
                //    }

                //}
                //else if (result > 0 && !filter.Equals("Sve"))
                //{
                //    //treba da bude

                //    double preostaliDani = datum.Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
                //    if (preostaliDani >= 7)
                //    {
                //        if (k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //        {
                //            k.MogucOdustanak = true;
                //            Data.SaveKartu(k);
                //        }
                //    }
                //    else
                //    {
                //        if (!k.MogucOdustanak.ToString().Equals("False") && !k.Obrisana)
                //        {
                //            k.MogucOdustanak = false;
                //            Data.SaveKartu(k);
                //        }
                //    }

                //    if (filter.Equals(k.Status.ToString()) && !k.Obrisana)  //tog statusa i da nije obrisana
                //    {

                //        odabrane.Add(k);
                //    }
                //}
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


                if (filter.Equals("Sve") && !k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
                }
                else if (!k.Obrisana && k.Tipkarte.ToString().Equals(filter.ToUpper()) && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
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


                if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
                }
                //else if (!k.Obrisana && k.Tipkarte.ToString().Equals(filter.ToUpper()) && k.Korisnikid.Equals(korisnik))
                //{
                //    odabrane.Add(k);
                //}

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


                if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
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


                if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
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


                if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
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


                if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
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


                if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
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


                if (!k.Obrisana && k.Korisnikid.Equals(korisnik))
                {
                    odabrane.Add(k);
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
