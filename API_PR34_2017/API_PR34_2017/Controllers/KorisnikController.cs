using API_PR34_2017.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace API_PR34_2017.Controllers
{
    [RoutePrefix("api/Korisnik")]
    public class KorisnikController : ApiController
    {
        [Route("SviKorisnici")]
        public HttpResponseMessage SviKorisnici()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            Dictionary<string, Korisnik> recnik = Data.ReadUser("~/App_Data/korisnici.txt");
            foreach(Korisnik u in recnik.Values)
            {
                korisnici.Add(u);
            }
            var json = JsonConvert.SerializeObject(korisnici);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [Route("ListaManifestacije")]
        public HttpResponseMessage ListaManifestacije()//([FromBody]JToken jtoken)
        {
            List<Manifestacija> svi = new List<Manifestacija>();
            List<Manifestacija> festovi = Data.ReadFest("~/App_Data/manifestacije.txt");     
            foreach (Manifestacija k in festovi)
            {
                svi.Add(k);
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


            mani.Brojmesta = int.Parse(HttpContext.Current.Request.Form["brojmesta"]);
            mani.Cenaregular = Double.Parse(HttpContext.Current.Request.Form["cenaregular"]);
            mani.Datumivreme = HttpContext.Current.Request.Form["datumivreme"];
            Mesto mjesto = new Mesto(HttpContext.Current.Request.Form["ulicabroj"], HttpContext.Current.Request.Form["grad"], HttpContext.Current.Request.Form["postanskibroj"]);

            mani.Mestoodrzavanja = mjesto;

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
                if (fest.Naziv.Equals(mani.Naziv) && fest.Datumivreme.Equals(mani.Datumivreme))
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
    }
}
