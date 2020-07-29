using API_PR34_2017.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
