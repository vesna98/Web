﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace API_PR34_2017.Models
{
    public class Data
    {
        public static void SaveUser(Korisnik p)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            string[] lines = System.IO.File.ReadAllLines(putanja);

            bool izmeni = false;
            string nova = "";

            for (int i = 0; i < lines.Count(); i++)
            {
                string[] tokens = lines[i].Split(';');

                if (tokens[0].Equals(p.Korisnickoime))//jednaki id
                {
                    nova = p.ToString();
                    lines[i] = nova;
                    izmeni = true;
                    break;
                }

            }

            if (izmeni)//menja postojeca
            {
                System.IO.File.WriteAllLines(putanja, lines);
            }
            else
            {
                //dopisuje
                FileStream fs = new FileStream(putanja, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                string str = p.ToString();
                sw.WriteLine(str);
                sw.Close();
                fs.Close();
            }
        }

        public static Dictionary<string, Korisnik> ReadUser(string path)
        {
            Dictionary<string, Korisnik> users = new Dictionary<string, Korisnik>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {

                string[] tokens = line.Split(';');


                bool obr;
                bool.TryParse(tokens[9], out obr);
                //                   Korisnickoime + ";" + Lozinka + ";" + Ime + ";" + Prezime + ";" + Pol.ToString() + ";" + Datumrodjenja + ";" + Sakupljenibodovi.ToString() +";"+Tip.Imetipa +";"+Uloga.ToString()+ ";"+ Obrisan.ToString()
                Korisnik p = new Korisnik(tokens[0], tokens[1], tokens[2], tokens[3], (PolType)Enum.Parse(typeof(PolType), tokens[4]), tokens[5], int.Parse(tokens[6]), (TipIme)Enum.Parse(typeof(TipIme), tokens[7]), (UlogaType)Enum.Parse(typeof(UlogaType), tokens[8]), obr);

                users.Add(p.Korisnickoime, p);

            }
            sr.Close();
            stream.Close();

            return users;
        }

        public static List<Manifestacija> ReadFest(string path)
        {
            List<Manifestacija> fests = new List<Manifestacija>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {

                string[] tokens = line.Split(';');


                bool obr;
                bool.TryParse(tokens[9], out obr);

                //Prodavac+";"+ Naziv+";"+Tipmanifestacije.ToString() + ";" +Brojmesta.ToString() + ";" +Datumivreme.ToString() + ";" +Cenaregular.ToString() + ";" +Mestoodrzavanja.Unicabroj + ";" +Mestoodrzavanja.Grad + ";" +Mestoodrzavanja.Postanskibroj.ToString() + ";" +Poster;

                Mesto mjesto = new Mesto(tokens[6], tokens[7], tokens[8]);
                //mjesto.Ulicabroj = ;
                //mjesto.Grad = ;
                //mjesto.Postanskibroj = ;
                Manifestacija p = new Manifestacija(tokens[0], tokens[1], (TypeManifestacije)Enum.Parse(typeof(TypeManifestacije), tokens[2]), int.Parse(tokens[3]), tokens[4], Double.Parse(tokens[5]), mjesto, tokens[9], int.Parse(tokens[12]), Double.Parse(tokens[13]), Double.Parse(tokens[14]), int.Parse(tokens[11]));
                fests.Add(p);//DODATI FESTIVAL

            }
            sr.Close();
            stream.Close();

            return fests;
        }


        public static void SaveFest(Manifestacija p)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/manifestacije.txt");
            string[] lines = System.IO.File.ReadAllLines(putanja);

            bool izmeni = false;
            string nova = "";

            //for (int i = 0; i < lines.Count(); i++)
            //{
            //    string[] tokens = lines[i].Split(';');

            //    if (tokens[1].Equals(p.Id.ToString()))//jednaki id
            //    {
            //        nova = p.ToString();
            //        lines[i] = nova;
            //        izmeni = true;
            //        break;
            //    }

            //}

            if (izmeni)//menja postojeca
            {
                System.IO.File.WriteAllLines(putanja, lines);
            }
            else
            {
                //dopisuje
                FileStream fs = new FileStream(putanja, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                string str = p.ToString();
                sw.WriteLine(str);
                sw.Close();
                fs.Close();
            }
        }
    }
}