using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Lokacija
    {
        public Lokacija(string idmanifestacije, string geoduzina, string geosirina,string grad,string ulicabroj,string postanskibroj)
        {
            Idmanifestacije = idmanifestacije;
            Geoduzina = geoduzina;
            Geosirina = geosirina;
            Mesto mesto = new Mesto(ulicabroj,grad,postanskibroj);
            MestoOdrzavanja = mesto;

        }

        public string Idmanifestacije { get; set; }
        public string Geoduzina { get; set; }
        public string Geosirina { get; set; }
        public Mesto MestoOdrzavanja { get; set; }

        public override string ToString()
        {
            return Idmanifestacije+";"+Geoduzina + ";" +Geosirina + ";" +MestoOdrzavanja.Grad + ";" +MestoOdrzavanja.Ulicabroj + ";" +MestoOdrzavanja.Postanskibroj;
        }
    }
}