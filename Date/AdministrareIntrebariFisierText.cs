using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stocare
{
    public class AdministrareIntrebariFisierText : IStocareData
    {
        private string numeFisierIntrebari;
        private string numeFisierExamen = "configExamen.txt"; // Fișier separat pentru a doua entitate

        public AdministrareIntrebariFisierText(string numeFisier)
        {
            this.numeFisierIntrebari = numeFisier;
            if (!File.Exists(numeFisierIntrebari))
                File.Create(numeFisierIntrebari).Dispose();
        }

        // --- IMPLEMENTARE INTREBARI (Existent deja) ---
        public void AdaugaIntrebare(Intrebare i)
        {
            i.Id = GetNextId();
            File.AppendAllLines(numeFisierIntrebari, new[] { i.ConversieLaSirPentruFisier() });
        }

        public List<Intrebare> GetIntrebari()
        {
            if (!File.Exists(numeFisierIntrebari)) return new List<Intrebare>();
            return File.ReadAllLines(numeFisierIntrebari).Select(l => new Intrebare(l)).ToList();
        }

        public Intrebare CautaDupaId(int id) => GetIntrebari().FirstOrDefault(i => i.Id == id);

        public void ModificaIntrebare(Intrebare iModif)
        {
            var toate = GetIntrebari();
            using (StreamWriter sw = new StreamWriter(numeFisierIntrebari, false))
            {
                foreach (var i in toate)
                    sw.WriteLine(i.Id == iModif.Id ? iModif.ConversieLaSirPentruFisier() : i.ConversieLaSirPentruFisier());
            }
        }

        // --- CERINȚA 3: IMPLEMENTARE EXAMEN (A doua entitate) ---

        public void SalveazaExamen(Examen examen)
        {
            // Salvăm valoarea numerică a Flag-urilor (enum) în fișier
            File.WriteAllText(numeFisierExamen, ((int)examen.OptiuniActive).ToString());
        }

        public Examen GetExamen()
        {
            Examen ex = new Examen();
            if (File.Exists(numeFisierExamen))
            {
                string continut = File.ReadAllText(numeFisierExamen);
                if (int.TryParse(continut, out int valoareEnum))
                {
                    // Convertim înapoi din număr în tipul SetariExamen (Enum Flags)
                    ex.OptiuniActive = (LibrarieEnumuri.SetariExamen)valoareEnum;
                }
            }
            return ex;
        }

        private int GetNextId()
        {
            var lista = GetIntrebari();
            return (lista.Count == 0) ? 1 : lista.Max(i => i.Id) + 1;
        }
    }
}