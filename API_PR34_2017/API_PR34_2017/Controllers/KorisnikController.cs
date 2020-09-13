using API_PR34_2017.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace API_PR34_2017.Controllers
{
    [RoutePrefix("api/Korisnik")]
    public class KorisnikController : ApiController
    {
        [Route("SviKorisnici")]
        [HttpPost]
        public HttpResponseMessage SviKorisnici()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                //if (!u.Uloga.ToString().Equals("Administrator"))        //svi korisnici sem administratora
                    korisnici.Add(u);
            }
            var json = JsonConvert.SerializeObject(korisnici);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("KorisniciImeAZ")]
        [HttpPost]
        public HttpResponseMessage KorisniciImeAZ()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderBy(i => i.Ime);
            
            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("KorisniciImeZA")]
        [HttpPost]
        public HttpResponseMessage KorisniciImeZA()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderByDescending(i => i.Ime);

            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("KorisniciPrzAZ")]
        [HttpPost]
        public HttpResponseMessage KorisniciPrzAZ()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderBy(i => i.Prezime);

            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("KorisniciPrzZA")]
        [HttpPost]
        public HttpResponseMessage KorisniciPrzZA()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderByDescending(i => i.Prezime);

            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [Route("KorisniciKorImeAZ")]
        [HttpPost]
        public HttpResponseMessage KorisniciKorImeAZ()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderBy(i => i.Korisnickoime);

            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("KorisniciKorImeZA")]
        [HttpPost]
        public HttpResponseMessage KorisniciKorImeZA()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderByDescending(i => i.Korisnickoime);

            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("KorisniciBodoviRastuce")]
        [HttpPost]
        public HttpResponseMessage KorisniciBodoviRastuce()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderBy(i => i.Sakupljenibodovi);

            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("KorisniciBodoviOpadajuce")]
        [HttpPost]
        public HttpResponseMessage KorisniciBodoviOpadajuce()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var sortirano = korisnici.OrderByDescending(i => i.Sakupljenibodovi);

            var json = JsonConvert.SerializeObject(sortirano);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [Route("TipoviKorisnikaFilter")]
        [HttpPost]
        public HttpResponseMessage TipoviKorisnikaFilter(JObject jsonResult)
        {
            string filter = (string)jsonResult["filter1"];

            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                if(u.Tip.ToString().Equals(filter) && !filter.Equals("Sve"))
                    korisnici.Add(u);
                if (filter.Equals("Sve"))
                {
                    korisnici.Add(u);
                }
            }
            
            var json = JsonConvert.SerializeObject(korisnici);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [Route("UlogaKorisnikaFilter")]
        [HttpPost]
        public HttpResponseMessage UlogaKorisnikaFilter(JObject jsonResult)
        {
            string filter = (string)jsonResult["filter1"];

            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                if (u.Uloga.ToString().Equals(filter) && !filter.Equals("Sve"))
                    korisnici.Add(u);
                if (filter.Equals("Sve"))
                {
                    korisnici.Add(u);
                }
            }

            var json = JsonConvert.SerializeObject(korisnici);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [Route("PretragaKorisnika")]
        [HttpPost]
        public HttpResponseMessage PretragaKorisnika(JObject jsonResult)
        {
            string korime = (string)jsonResult["korime"];
            string ime = (string)jsonResult["ime"];
            string prz = (string)jsonResult["prezime"];

            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                //if (u.Uloga.ToString().Equals(filter) && !filter.Equals("Sve"))
                //    korisnici.Add(u);
                //if (filter.Equals("Sve"))
                //{
                    korisnici.Add(u);
               // }
            }

            if (!string.IsNullOrWhiteSpace(korime))
                korisnici = korisnici.FindAll(u => u.Korisnickoime.ToUpper().Contains(korime.ToUpper()));
            if (!string.IsNullOrWhiteSpace(ime))
                korisnici = korisnici.FindAll(u => u.Ime.ToUpper().Contains(ime.ToUpper()));
            if (!string.IsNullOrWhiteSpace(prz))
                korisnici = korisnici.FindAll(u => u.Prezime.ToUpper().Contains(prz.ToUpper()));

            var json = JsonConvert.SerializeObject(korisnici);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [Route("ObrisiKorisnika")]
        [HttpPost]
        public HttpResponseMessage ObrisiKorisnika(JObject jsonResult)
        {
            string filter = (string)jsonResult["korime"];

            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                if (u.Korisnickoime.Equals(filter) && u.Uloga.ToString().Equals("Kupac") && !u.Obrisan)   //kad je kupac brisu se karte i dodaju kod manifestacije slob mesta
                {
                    u.Obrisan = true;
                    Data.SaveUser(u);
                    //brisanje i karata
                    //int brojNovihSlobodnihKarata = 0;
                    
                    List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");
                    foreach (Karta karta in karte)
                    {
                        if (karta.Korisnikid.Equals(filter))
                        {
                            if (!karta.Obrisana)//ako nije vec predhodno obrisan karta obrisi
                            {
                                karta.Obrisana = true;
                                Data.SaveKartu(karta);
                                //brojNovihSlobodnihKarata++;
                                //smanjiti broj kupljenih karata
                                List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                                foreach (Manifestacija fest in festovi)
                                {
                                    if (fest.Naziv.Equals(karta.Nazivmanifestacije) && fest.Datumivreme.Equals(karta.Datummanifestacije) && !fest.Obrisan)
                                    {
                                        fest.Kupljeno -= 1;
                                        Data.SaveFest(fest);
                                    }
                                }
                            }

                        }
                    }

                }else if (u.Korisnickoime.Equals(filter) && u.Uloga.ToString().Equals("Prodavac") && !u.Obrisan)
                {
                    //ako je prodavac onda se brisu manifestacije i karte za prodavce
                    u.Obrisan = true;
                    Data.SaveUser(u);

                    List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
                    foreach (Manifestacija fest in festovi)
                    {
                        if (fest.Prodavac.Equals(u.Korisnickoime))
                        {
                            if (!fest.Obrisan)//ako vec nije obrisan
                            {
                                fest.Obrisan = true;//brise fest pa treba i karte za taj
                                Data.SaveFest(fest);

                                List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");
                                foreach (Karta kart in karte)
                                {
                                    if(!kart.Obrisana && kart.Nazivmanifestacije.Equals(fest.Naziv) && kart.Datummanifestacije.Equals(fest.Datumivreme))
                                    {
                                        //ako nije obrisana brise se
                                        kart.Obrisana = true;
                                        Data.SaveKartu(kart);
                                    }
                                }
                            }
                        }
                    }
                }
                    korisnici.Add(u);
               
            }

            var json = JsonConvert.SerializeObject(korisnici);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [Route("ListaManifestacije")]
        [HttpPost]
        public HttpResponseMessage ListaManifestacije()//([FromBody]JToken jtoken)
        {
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            
            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    { 
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                       // if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }

                    
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            Dictionary<Manifestacija, DateTime> recnik = new Dictionary<Manifestacija, DateTime>();
            for (int i = 0; i < svi.Count(); i++)
            {
                DateTime myDate;
                Manifestacija temp = svi[i];
                if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                // //if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                {
                    recnik.Add(temp, myDate);
                }
            }
            //recnik.OrderBy(b => b.Value.Date).ThenBy(b => b.Value.TimeOfDay);//ne radi

            var dateTimesAscending = recnik.Values.OrderBy(d => d);
            List<Manifestacija> konacna = new List<Manifestacija>();


            foreach (var ii in dateTimesAscending)
            {

                foreach (Manifestacija m in recnik.Keys)
                {
                    DateTime myDate;
                    Manifestacija temp = m;
                    //if (DateTime.TryParseExact(temp.Datumivreme, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    {

                        if (myDate == ii)
                        {
                            konacna.Add(m);
                        }
                    }
                }
            }

            var output = JsonConvert.SerializeObject(konacna);
            //var output = JsonConvert.SerializeObject(recnik.Keys.ToList());//ne radi

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("DodajFest")]
        [HttpPost]
        public HttpResponseMessage DodajFest()//([FromBody] Object manifestacija)
        {
            // if (ModelState.IsValid)
            // {
            List<Manifestacija> sveManifestacije = new List<Manifestacija>();
            sveManifestacije = Data.ReadFest("~/App_Data/manifestacije.txt");

            Manifestacija mani = new Manifestacija();

            mani.Naziv = HttpContext.Current.Request.Form["naziv"];
            mani.Prodavac = HttpContext.Current.Request.Form["prodavac"];
            mani.Tipmanifestacije = (TypeManifestacije)Enum.Parse(typeof(TypeManifestacije), HttpContext.Current.Request.Form["tipmanifestacije"]);

            mani.IDmanifestacije = GetRandomString(5);  //DODAT ID MANIFESTACIJE
            mani.Brojmesta = int.Parse(HttpContext.Current.Request.Form["brojmesta"]);
            mani.Cenaregular = Double.Parse(HttpContext.Current.Request.Form["cenaregular"]);
            mani.Cenavip = 4 * mani.Cenaregular; /*Double.Parse(HttpContext.Current.Request.Form["cenavip"]); */      //dodato za vip i fan pit cena
            mani.Cenafanpit = 2 * mani.Cenaregular;/* Double.Parse(HttpContext.Current.Request.Form["cenafanpit"]);*/
            mani.Datumivreme = HttpContext.Current.Request.Form["datumivreme"];
            Mesto mjesto = new Mesto(HttpContext.Current.Request.Form["ulicabroj"], HttpContext.Current.Request.Form["grad"], HttpContext.Current.Request.Form["postanskibroj"]);

            mani.Mestoodrzavanja = mjesto;

            string datumVreme = mani.Datumivreme;
            string vreme = datumVreme.Split(' ')[1];
            string sati = vreme.Split(':')[0];

            if (sati.Length == 1)
            {
                datumVreme = datumVreme.Split(' ')[0] + " 0" + sati + ":" + vreme.Split(':')[1]+" "+ datumVreme.Split(' ')[2];
                mani.Datumivreme = datumVreme;
            }

            //provera da li je validan datum sa tim vremenom
            DateTime myDate;
            
            if (!DateTime.TryParseExact(mani.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

                string fileSavePath = string.Empty;
            //string virtualDirectoryImg = "Files";
            string fileName = string.Empty;

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["poster"];
                fileName = httpPostedFile.FileName;
                mani.Poster = fileName;         //dodaje naziv slike
                if (httpPostedFile != null)
                {
                    // OBtient le path du fichier 
                    fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files"), httpPostedFile.FileName);

                    // Sauvegarde du fichier dans UploadedFiles sur le serveur
                    httpPostedFile.SaveAs(fileSavePath);
                }

                // return Request.CreateResponse(HttpStatusCode.OK);
                // return MessageOutCoreVm.SendSucces(virtualDirectoryImg + '/' + fileName);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
                // return MessageOutCoreVm.SendValidationFailed("");
            }

            bool postoji = false;
            foreach (Manifestacija fest in sveManifestacije)
            {
                if (fest.Naziv.Equals(mani.Naziv) && fest.Datumivreme.Equals(mani.Datumivreme))     //DA LI TREBA POPRAVITI
                {
                    postoji = true;
                }
            }

            if (!postoji)
            {
                Data.SaveFest(mani);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                //var output = JsonConvert.SerializeObject(svi);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            //}//

            // return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("NadjiManifestaciju")]
        [HttpPost]
        public HttpResponseMessage NadjiManifestaciju(JObject jsonResult)
        {
            /*
            string naziv = (string)jsonResult["naziv"];
            string datum = (string)jsonResult["datum"];*/
            string ID = (string)jsonResult["id"];

            //da se pronadju manifestacije i koje nisu aktivne zbog admina,da imaju detaljan prikaz
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];
            //-----------------------------------------------------------------
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            Manifestacija man =null;
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))
                {
                    //if (user!=null && user.Uloga.ToString().Equals("Prodavac") && k.Naziv.Equals(naziv) && k.Datumivreme.Equals(datum) && !k.Obrisan)
                    if (user!=null && user.Uloga.ToString().Equals("Prodavac") && k.IDmanifestacije.Equals(ID) && !k.Obrisan)
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            man = k;    //moze i aktivna i neaktivna
                        }
                    //}else if (k.Naziv.Equals(naziv) && k.Datumivreme.Equals(datum) && !k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                    }else if (k.IDmanifestacije.Equals(ID) && !k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                    {
                        man = k;
                    }
                }
                else
                {//kad je admin,posto on vidi i neaktivno i aktivne         //da li moze?????????????
                    //if (k.Naziv.Equals(naziv) && k.Datumivreme.Equals(datum) && !k.Obrisan)
                    if (k.IDmanifestacije.Equals(ID)  && !k.Obrisan)
                    {
                        man = k;
                    }

                }
            }
                var json = JsonConvert.SerializeObject(man);

            if (man == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,json);
            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
        }

        [Route("DobaviJednuManifestaciju")]
        [HttpPost]
        public HttpResponseMessage DobaviJednuManifestaciju(JObject jsonResult)
        {
            //var obj = JsonConvert.DeserializeObject<dynamic>(jsonResult.ToString());
            //string naziv = (string)jsonResult["naziv"];
            //string datum = (string)jsonResult["datum"];
            string idman = (string)jsonResult["idman"];

            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            Manifestacija man = null;
            foreach (Manifestacija k in festovi)
            {
               // if (k.Naziv.Equals(naziv) && k.Datumivreme.Equals(datum) && !k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                if (k.IDmanifestacije.Equals(idman) && !k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                {
                    if (k.Brojmesta - k.Kupljeno > 0) //samo ako ima slobosnih mesta prosledi
                        man = k;
                }
            }
            var json = JsonConvert.SerializeObject(man);
            //if (man != null)
            //{
            //    int result = DateTime.Compare(DateTime.ParseExact(man.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None) , DateTime.Now);
            //    if (result <= 0)
            //    {
            //        //prosla je manifestacija
            //        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject("Manifestacija je prosla."));
            //    }
            //}
            return Request.CreateResponse(HttpStatusCode.OK, json);
          
        }

        [Route("ObrisiManifestaciju")]
        [HttpPost]
        public HttpResponseMessage ObrisiManifestaciju(JObject jsonResult)
        {
            
            //string naziv = (string)jsonResult["naziv"];
            //string datum = (string)jsonResult["datum"];
            string ID = (string)jsonResult["id"];

            List<Manifestacija> festovi1 = Data.ReadFest("~/App_Data/manifestacije.txt");
           // Manifestacija man = null;
            foreach (Manifestacija k in festovi1)
            {
                //if (k.Naziv.Equals(naziv) && k.Datumivreme.Equals(datum) && !k.Obrisan)     //da li mora biti aktivna da bi se obrisala???????
                if (k.IDmanifestacije.Equals(ID) && !k.Obrisan)     //da li mora biti aktivna da bi se obrisala???????
                {
                    k.Obrisan = true;
                    Data.SaveFest(k);

                    //kad se brise manifestacija brisu se i karte za tu manifestaciju sto su rezervisane
                    List<Karta> karte = Data.ReadKarte("~/App_Data/karte.txt");
                    foreach (Karta karta in karte)
                    {
                        //if(karta.Nazivmanifestacije.Equals(k.Naziv) && karta.Datummanifestacije.Equals(k.Datumivreme))
                        if(karta.Nazivmanifestacije.Equals(k.Naziv) && karta.Datummanifestacije.Equals(k.Datumivreme))      //POPRAVITI ZA KARTU
                        {
                            if (!karta.Obrisana)
                            {
                                //ako vec prethodno nije obrisana
                                karta.Obrisana = true;
                                Data.SaveKartu(karta);      //brise karta
                            }
                        }
                    }
                }
            }
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                        svi.Add(k);
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            Dictionary<Manifestacija, DateTime> recnik = new Dictionary<Manifestacija, DateTime>();
            for (int i = 0; i < svi.Count(); i++)
            {
                DateTime myDate;
                Manifestacija temp = svi[i];
                if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                // //if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                {
                    recnik.Add(temp, myDate);
                }
            }
            //recnik.OrderBy(b => b.Value.Date).ThenBy(b => b.Value.TimeOfDay);//ne radi

            var dateTimesAscending = recnik.Values.OrderBy(d => d);
            List<Manifestacija> konacna = new List<Manifestacija>();


            foreach (var ii in dateTimesAscending)
            {

                foreach (Manifestacija m in recnik.Keys)
                {
                    DateTime myDate;
                    Manifestacija temp = m;
                    //if (DateTime.TryParseExact(temp.Datumivreme, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    {

                        if (myDate == ii)
                        {
                            konacna.Add(m);
                        }
                    }
                }
            }

            var output = JsonConvert.SerializeObject(konacna);
            //var output = JsonConvert.SerializeObject(recnik.Keys.ToList());//ne radi

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("AktivirajManifestaciju")]
        [HttpPost]
        public HttpResponseMessage AktivirajManifestaciju(JObject jsonResult)
        {
            
            //string naziv = (string)jsonResult["naziv"];
            //string datum = (string)jsonResult["datum"];
            string ID = (string)jsonResult["id"];

            List<Manifestacija> festovi1 = Data.ReadFest("~/App_Data/manifestacije.txt");
            // Manifestacija man = null;
            foreach (Manifestacija k in festovi1)
            {
                //if (k.Naziv.Equals(naziv) && k.Datumivreme.Equals(datum) && !k.Obrisan && k.Status.ToString().Equals("Neaktivno"))     //neaktivna i da nije obrisana
                if (k.IDmanifestacije.Equals(ID) && !k.Obrisan && k.Status.ToString().Equals("Neaktivno"))     //neaktivna i da nije obrisana
                {
                    //k.Obrisan = true;
                    k.Status = StatusType.Aktivno;
                    Data.SaveFest(k);
                }
            }
            
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            Dictionary<Manifestacija, DateTime> recnik = new Dictionary<Manifestacija, DateTime>();
            for (int i = 0; i < svi.Count(); i++)
            {
                DateTime myDate;
                Manifestacija temp = svi[i];
                if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                // //if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                {
                    recnik.Add(temp, myDate);
                }
            }
            //recnik.OrderBy(b => b.Value.Date).ThenBy(b => b.Value.TimeOfDay);//ne radi

            var dateTimesAscending = recnik.Values.OrderBy(d => d);
            List<Manifestacija> konacna = new List<Manifestacija>();


            foreach (var ii in dateTimesAscending)
            {

                foreach (Manifestacija m in recnik.Keys)
                {
                    DateTime myDate;
                    Manifestacija temp = m;
                    //if (DateTime.TryParseExact(temp.Datumivreme, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    {

                        if (myDate == ii)
                        {
                            konacna.Add(m);
                        }
                    }
                }
            }

            var output = JsonConvert.SerializeObject(konacna);
            //var output = JsonConvert.SerializeObject(recnik.Keys.ToList());//ne radi

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("Pretraga")]
        [HttpPost]
        public HttpResponseMessage Pretraga(JObject jsonResult)
        {
            string naziv = (string)jsonResult["naziv"];
            string mesto = (string)jsonResult["mesto"];
            string datumOD = (string)jsonResult["datumOD"];
            string datumDO = (string)jsonResult["datumDO"];
            int cenaOD;
                bool cena1=Int32.TryParse((string)jsonResult["cenaOD"],out cenaOD);
            int cenaDO;
            bool cena2=Int32.TryParse((string)jsonResult["cenaDO"],out cenaDO);

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

            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            int treba = 0;
            if(cena1 && !cena2)
            {
                svi = svi.Where(u => u.Cenaregular>=cenaOD).ToList();
            }
            else if(!cena1 && cena2)
            {
                svi = svi.Where(u => u.Cenaregular<=cenaDO).ToList();
            }
            else if(cena1 && cena2)
            {
                svi = svi.Where(u => u.Cenaregular >= cenaOD).ToList();
                svi = svi.Where(u => u.Cenaregular <= cenaDO).ToList();
            }

            //datum
            if (oba)
            {
                int result = DateTime.Compare(datOD, datDO);

                if (result < 0) //NE MOZE,NIJE DOBRO UNETO relationship = "is earlier than";
                {
                    svi = svi.FindAll(u => treba <= DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD));
                    svi = svi.FindAll(u => treba >= DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datDO));

                }

                else if (result == 0)  //OKEJ,JEDNAKO SU DATUMI,ONDA SAMO TOG DANA STO JE relationship = "is the same time as";
                {
                   // int treba = 0;
                    svi = svi.FindAll(u => treba==DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD));
                }
                else//RADIMO ONDA POREDJENJA DA BUDU POSLE PRVOG I PRE DRUGOG relationship = "is later than";
                {
                    //int treba = 0;
                    //svi = svi.FindAll(u => treba <= DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD));
                    //svi = svi.FindAll(u => treba >= DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datDO));
                    
                   /* (0 < DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD)))*/
                }
               
            }

               // int treba = 0;
            if (dat1)
            {
                svi = svi.FindAll(u => treba <= DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datOD));
            }

            if (dat2)
            {
                svi = svi.FindAll(u => treba >= DateTime.Compare(DateTime.ParseExact(u.Datumivreme.Split(' ')[0], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None), datDO));
            }
            // var filteredSvi = new List<Manifestacija>();
            if (!string.IsNullOrWhiteSpace(naziv.Trim()))
                svi = svi.Where(u => u.Naziv.ToUpper().Contains(naziv.ToUpper())).ToList();
            if (!string.IsNullOrWhiteSpace(mesto.Trim())) //&& !string.IsNullOrWhiteSpace(naziv.Trim()))//ima mesto i naziv
            {
                svi = svi.FindAll(u => u.Mestoodrzavanja.Grad.ToUpper().Contains(mesto.ToUpper()));
            }
            //else if(!string.IsNullOrWhiteSpace(mesto.Trim()) && string.IsNullOrWhiteSpace(naziv.Trim()))//ima mesto nema naziv
            //{
            //    filteredSvi =svi.FindAll(u => u.Mestoodrzavanja.Grad.ToUpper().Contains(mesto.ToUpper()));
            //}

            Dictionary<Manifestacija, DateTime> recnik = new Dictionary<Manifestacija, DateTime>();
            for (int i = 0; i < svi.Count(); i++)
            {
                DateTime myDate;
                Manifestacija temp = svi[i];
                if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                {
                    recnik.Add(temp, myDate);
                }
            }
            
            var dateTimesAscending = recnik.Values.OrderBy(d => d);
            List<Manifestacija> konacna = new List<Manifestacija>();


            foreach (var ii in dateTimesAscending)
            {

                foreach (Manifestacija m in recnik.Keys)
                {
                    DateTime myDate;
                    Manifestacija temp = m;
                    
                    if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    {

                        if (myDate == ii)
                        {
                            konacna.Add(m);
                        }
                    }
                }
            }

            var output = JsonConvert.SerializeObject(konacna);
            //var output = JsonConvert.SerializeObject(recnik.Keys.ToList());//ne radi

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
        [Route("TipoviFilter")]
        [HttpPost]
        public HttpResponseMessage TipoviFilter(JObject jsonResult)
        {
            string filter = (string)jsonResult["filter1"];

            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            Dictionary<Manifestacija, DateTime> recnik = new Dictionary<Manifestacija, DateTime>();
            for (int i = 0; i < svi.Count(); i++)
            {
                DateTime myDate;
                Manifestacija temp = svi[i];
                if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))

                {
                    if (filter.Equals("Sve"))               //dopunjeno za odgovarajuci filter i sve
                    {
                        recnik.Add(temp, myDate);
                    }
                    if (!filter.Equals("Sve") && temp.Tipmanifestacije.ToString().Equals(filter))
                    {
                        recnik.Add(temp, myDate);
                    }
                }
            }

            var dateTimesAscending = recnik.Values.OrderBy(d => d);
            List<Manifestacija> konacna = new List<Manifestacija>();


            foreach (var ii in dateTimesAscending)
            {

                foreach (Manifestacija m in recnik.Keys)
                {
                    DateTime myDate;
                    Manifestacija temp = m;

                    if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    {

                        if (myDate == ii)
                        {
                            konacna.Add(m);
                        }
                    }
                }
            }

            var output = JsonConvert.SerializeObject(konacna);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("KarteFilter")]
        [HttpPost]
        public HttpResponseMessage KarteFilter(JObject jsonResult)
        {
            string filter = (string)jsonResult["filter2"];

            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            Dictionary<Manifestacija, DateTime> recnik = new Dictionary<Manifestacija, DateTime>();
            for (int i = 0; i < svi.Count(); i++)
            {
                DateTime myDate;
                Manifestacija temp = svi[i];
                if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))

                {
                    if (filter.Equals("Sve"))               //dopunjeno za odgovarajuci filter i sve
                    {
                        recnik.Add(temp, myDate);
                    }
                    else if(temp.Brojmesta-temp.Kupljeno>0) { 
                    
                        recnik.Add(temp, myDate);
                    }
                }
            }

            var dateTimesAscending = recnik.Values.OrderBy(d => d);
            List<Manifestacija> konacna = new List<Manifestacija>();


            foreach (var ii in dateTimesAscending)
            {

                foreach (Manifestacija m in recnik.Keys)
                {
                    DateTime myDate;
                    Manifestacija temp = m;

                    if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    {

                        if (myDate == ii)
                        {
                            konacna.Add(m);
                        }
                    }
                }
            }

            var output = JsonConvert.SerializeObject(konacna);

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("SortNazivAZ")]
        [HttpPost]
        public HttpResponseMessage SortNazivAZ()
        {
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            var sortirano = svi.OrderBy(i => i.Naziv);

            var output = JsonConvert.SerializeObject(sortirano);
            

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
        [Route("SortNazivZA")]
        [HttpPost]
        public HttpResponseMessage SortNazivZA()
        {
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            var sortirano = svi.OrderByDescending(i => i.Naziv);

            var output = JsonConvert.SerializeObject(sortirano);


            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("DatumKasnije")]
        [HttpPost]
        public HttpResponseMessage DatumKasnije()//([FromBody]JToken jtoken)
        {
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            Dictionary<Manifestacija, DateTime> recnik = new Dictionary<Manifestacija, DateTime>();
            for (int i = 0; i < svi.Count(); i++)
            {
                DateTime myDate;
                Manifestacija temp = svi[i];
                if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                // //if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                {
                    recnik.Add(temp, myDate);
                }
            }
            //recnik.OrderBy(b => b.Value.Date).ThenBy(b => b.Value.TimeOfDay);//ne radi

            var dateTimesAscending = recnik.Values.OrderByDescending(d => d);
            List<Manifestacija> konacna = new List<Manifestacija>();


            foreach (var ii in dateTimesAscending)
            {

                foreach (Manifestacija m in recnik.Keys)
                {
                    DateTime myDate;
                    Manifestacija temp = m;
                    //if (DateTime.TryParseExact(temp.Datumivreme, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    if (DateTime.TryParseExact(temp.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                    {

                        if (myDate == ii)
                        {
                            konacna.Add(m);
                        }
                    }
                }
            }

            var output = JsonConvert.SerializeObject(konacna);
            //var output = JsonConvert.SerializeObject(recnik.Keys.ToList());//ne radi

            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("SortMestoAZ")]
        [HttpPost]
        public HttpResponseMessage SortMestoAZ()
        {
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            var sortirano = svi.OrderBy(i => i.Mestoodrzavanja.ToString());

            var output = JsonConvert.SerializeObject(sortirano);


            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
        [Route("SortMestoZA")]
        [HttpPost]
        public HttpResponseMessage SortMestoZA()
        {
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            var sortirano = svi.OrderByDescending(i => i.Mestoodrzavanja.ToString());

            var output = JsonConvert.SerializeObject(sortirano);


            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("SortCenaRastuce")]
        [HttpPost]
        public HttpResponseMessage SortCenaRastuce()
        {
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            var sortirano = svi.OrderBy(i => i.Cenaregular);

            var output = JsonConvert.SerializeObject(sortirano);


            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
        [Route("SortCenaOpadajuce")]
        [HttpPost]
        public HttpResponseMessage SortCenaOpadajuce()
        {
            //List<Manifestacija> svi = new List<Manifestacija>();
            //List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            //foreach (Manifestacija k in festovi)
            //{
            //    if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
            //        svi.Add(k);
            //}
            Korisnik user = (Korisnik)HttpContext.Current.Session["user"];

            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (user == null || !user.Uloga.ToString().Equals("Administrator"))     //ako nije administrator dodaju se samo aktivni sto nisu obrisani
                {
                    if (user == null || user.Uloga.ToString().Equals("Kupac"))
                    {
                        //kad je kupac samo manifestacije za koje moze da rezervise
                        int result = DateTime.Compare(DateTime.ParseExact(k.Datumivreme, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None), DateTime.Now);
                        if (result > 0)
                        {
                            //prosla je manifestacija r<=0
                            //prikazuju se samo manifestacije kod kojih se mogu kupiti karte
                            if (!k.Obrisan && k.Status.ToString().Equals("Aktivno"))
                                svi.Add(k);

                        }
                    }
                    else if (user.Uloga.ToString().Equals("Prodavac"))
                    {
                        if (k.Prodavac.Equals(user.Korisnickoime))
                        {
                            if (!k.Obrisan)// && k.Status.ToString().Equals("Aktivno"))  mogu da vide sve svoje manifestacije aktivne i neaktivne
                                svi.Add(k);
                        }
                    }
                }
                else
                {
                    //ako je administartor dodaju se samo oni koji nisu obrisani  status AKTIVNO I NEAKTIVNO
                    if (!k.Obrisan)
                        svi.Add(k);
                }
            }

            var sortirano = svi.OrderByDescending(i => i.Cenaregular);

            var output = JsonConvert.SerializeObject(sortirano);


            return Request.CreateResponse(HttpStatusCode.OK, output);
        }

        [Route("RezervisiKarte")]
        [HttpPost]
        public int RezervisiKarte(JObject jsonResult)
        {
            string reg = (string)jsonResult["reg"];
            string vip = (string)jsonResult["vip"];
            string fan = (string)jsonResult["fan"];
            string korime = (string)jsonResult["korime"];
            //string ime = (string)jsonResult["ime"];
            //string prz = (string)jsonResult["prz"];
            string manifestacija = (string)jsonResult["naziv"];
            string datum = (string)jsonResult["datum"];
            string cenareg = (string)jsonResult["cena"];
            string IDman = (string)jsonResult["idman"];

            if(string.IsNullOrWhiteSpace(korime) && string.IsNullOrWhiteSpace(manifestacija) && string.IsNullOrWhiteSpace(datum) && string.IsNullOrWhiteSpace(cenareg))
            {
                return 1;
            }
            //broj_bodova = cena_jedne_karte/1000 * 133
            //string ime;
            //string prezime;
            Korisnik kupac = new Korisnik();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach (Korisnik u in recnik.Values)
            {
                if (u.Korisnickoime.Equals(korime))
                {
                    kupac = u;
                    //ime = u.Ime;
                    //prezime = u.Prezime;
                }
            }

            bool odustanak = false;
            double preostaliDani = DateTime.ParseExact(datum, "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None).Subtract(DateTime.Today).TotalDays;        //racuna dane,da li ima 7 ili vise da bi se moglo odustati
            if (preostaliDani >= 7)
            {
                odustanak = true;
            }


            int ukupanBrojRez = 0;
            //nazivmanifestacije, string datummanifestacije, double cena, string kupac, string korisnikid, StatusKarte status, TypeKarte tipkarte
            if (!string.IsNullOrWhiteSpace(reg) && !reg.Equals("0"))
            {
                int brojreg = Int32.Parse(reg);
                ukupanBrojRez += brojreg;
                for(int i = 0; i < brojreg; i++)
                {
                    Data.SaveKartu(new Karta(manifestacija,datum,double.Parse(cenareg),kupac.Ime+" "+kupac.Prezime,kupac.Korisnickoime,StatusKarte.Odustanak,TypeKarte.REGULAR,GetRandomString(10),false,odustanak));
                    kupac.Sakupljenibodovi +=(double.Parse(cenareg) / 1000 * 133);
                }
            }
            if (!string.IsNullOrWhiteSpace(vip) && !vip.Equals("0"))
            {
                int brojvip = Int32.Parse(vip);
                ukupanBrojRez += brojvip;
                for (int i = 0; i < brojvip; i++)
                {
                    Data.SaveKartu(new Karta(manifestacija, datum, 4*double.Parse(cenareg), kupac.Ime + " " + kupac.Prezime, kupac.Korisnickoime, StatusKarte.Odustanak, TypeKarte.VIP, GetRandomString(10),false,odustanak));
                    kupac.Sakupljenibodovi += (4*double.Parse(cenareg) / 1000 * 133);
                }
            }
            if (!string.IsNullOrWhiteSpace(fan) && !fan.Equals("0"))
            {
                int brojfan = Int32.Parse(fan);
                ukupanBrojRez += brojfan;
                for (int i = 0; i < brojfan; i++)
                {
                    Data.SaveKartu(new Karta(manifestacija, datum, 2*double.Parse(cenareg), kupac.Ime + " " + kupac.Prezime, kupac.Korisnickoime, StatusKarte.Odustanak, TypeKarte.FANPIT, GetRandomString(10),false,odustanak));
                    kupac.Sakupljenibodovi += (2*double.Parse(cenareg) / 1000 * 133);
                }
            }
            //povecati broj kupljenih karata!!!!!
            Manifestacija m = new Manifestacija();
            m = null;
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");
            foreach (Manifestacija k in festovi)
            {
                if (!k.Obrisan && k.Status.ToString().Equals("Aktivno") && k.Datumivreme.Equals(datum) && k.Naziv.Equals(manifestacija))
                {
                    m = k;
                    break;
                }
            }
            if (m == null)
            {
                return 2;//tad ce prijaviti gresku da nema manifestacije
            }

            m.Kupljeno += ukupanBrojRez;
            Data.SaveFest(m);
            //cuva se podatak o kupcu
            Data.SaveUser(kupac);
            return 0;
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

        public HttpResponseMessage DodajProdavca([FromBody]Korisnik korisnik)
        {

            if (ModelState.IsValid)
            {
                korisnik.Rezervisanekarte = new List<string>();
                korisnik.Sakupljenibodovi = 0;
                korisnik.Obrisan = false;
                korisnik.Tip = TipIme.Nepoznat;
                korisnik.Uloga = UlogaType.Prodavac;

                DateTime timestamp;
                if (!DateTime.TryParseExact(korisnik.Datumrodjenja, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out timestamp))
                {
                    //Niste uneli ispravan datum rodjenja.
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject("Niste uneli ispravan datum rodjenja."));
                    //return 0;   //datum nije dobar
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
                   // var output = JsonConvert.SerializeObject("Uspesno ste dodali.");

                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject("Uspesno ste dodali."));

                    //return 1;                   //USPESNO UPISAN 1
                }
                return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject("Postoji korisnik sa tim korisnickim imenom."));
               // return 2;
            }
            return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject("Niste uneli ispravne podatke."));
            //return 2;   //POSTOJI VEC

        }
    }
}
