using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO; // Necesar pentru lucrul cu fișiere
using System.Linq;

namespace Stocare
{
    public class AdministrareIntrebari
    {
        private string numeFisier;

        public AdministrareIntrebari(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // Creăm fișierul dacă nu există deja
            using (Stream s = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
        }

        // --- CERINȚA 1: Salvare în fișier text ---
        public void AdaugaIntrebare(Intrebare intrebare)
        {
            intrebare.Id = GetNextId();

            using (StreamWriter sw = File.AppendText(numeFisier))
            {
                sw.WriteLine(intrebare.ConversieLaSirPentruFisier());
            }
        }

        public List<Intrebare> GetIntrebari()
        {
            List<Intrebare> intrebari = new List<Intrebare>();

            if (!File.Exists(numeFisier)) return intrebari;

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    // Folosim constructorul care "știe" să spargă linia din fișier
                    intrebari.Add(new Intrebare(linie));
                }
            }
            return intrebari;
        }

        // --- CERINȚA 2: Căutare după ID ---
        public Intrebare CautaDupaId(int id)
        {
            return GetIntrebari().FirstOrDefault(i => i.Id == id);
        }

        // --- CERINȚA 2: Modificare ---
        public void ModificaIntrebare(Intrebare intrebareModificata)
        {
            var toateIntrebarile = GetIntrebari();

            // Rescriem tot fișierul (false înseamnă că nu adăugăm la final, ci suprascriem)
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (var i in toateIntrebarile)
                {
                    if (i.Id == intrebareModificata.Id)
                    {
                        // Scriem noua versiune a întrebării
                        sw.WriteLine(intrebareModificata.ConversieLaSirPentruFisier());
                    }
                    else
                    {
                        // Scriem întrebarea originală, neschimbată
                        sw.WriteLine(i.ConversieLaSirPentruFisier());
                    }
                }
            }
        }

        private int GetNextId()
        {
            var lista = GetIntrebari();
            return (lista.Count == 0) ? 1 : lista.Max(i => i.Id) + 1;
        }
    }
}