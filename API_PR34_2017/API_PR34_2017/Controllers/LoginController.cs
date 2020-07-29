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
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [Route("Login")]
        public Korisnik Login([FromBody]Object korisnik)
        {
            //string uloga = "NEKA ULOGA";
            Dictionary<string, object> obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(korisnik));

            Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();
            korisnici = Data.ReadUser("~/App_Data/korisnici.txt");

            string username = "";
            string password = "";

            foreach (var kljuc in obj.Keys)
            {
                if (kljuc.Equals("korisnickoime"))
                {
                    username = (string)obj[kljuc];
                }
                else if (kljuc.Equals("lozinka"))
                {
                    password = (string)obj[kljuc];
                }
            }

            foreach (Korisnik user in korisnici.Values)
            {
                if (user.Korisnickoime.Equals(username) && user.Lozinka.Equals(password))
                {
                    return user;//postoji,pamti se da je ulogovan

                }
            }

            return null;

        }
    }
}
