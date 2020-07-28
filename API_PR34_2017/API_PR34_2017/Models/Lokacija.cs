using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Lokacija
    {
        public double Geoduzina { get; set; }
        public double Geosirina { get; set; }
        public Mesto MestoOdrzavanja { get; set; }
    }
}