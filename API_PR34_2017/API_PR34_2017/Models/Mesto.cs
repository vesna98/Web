using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PR34_2017.Models
{
    public class Mesto
    {
        public Mesto(string ulicabroj, string grad, string postanskibroj)
        {
            Ulicabroj = ulicabroj;
            Grad = grad;
            Postanskibroj = postanskibroj;
        }

        public string Ulicabroj { get; set; }
        public string Grad { get; set; }
        public string Postanskibroj { get; set; }

        public override string ToString()
        {
            return Ulicabroj+" "+Grad+" "+Postanskibroj.ToString();
        }
    }
}