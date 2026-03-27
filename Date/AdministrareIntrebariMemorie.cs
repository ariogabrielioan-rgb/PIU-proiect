using LibrarieModele;
using System.Collections.Generic;
using System.Linq;

namespace Stocare
{
    // Implementăm aceeași interfață ca să poată fi recunoscută de Factory
    public class AdministrareIntrebariMemorie : IStocareData
    {
        private List<Intrebare> intrebari;
        private Examen configExamen; // Stocăm și a doua entitate

        public AdministrareIntrebariMemorie()
        {
            intrebari = new List<Intrebare>();
            configExamen = new Examen(); // Inițializăm cu setări default
        }

        // --- ENTITATEA 1: Intrebare ---
        public void AdaugaIntrebare(Intrebare i)
        {
            i.Id = (intrebari.Count == 0) ? 1 : intrebari.Max(x => x.Id) + 1;
            intrebari.Add(i);
        }

        public List<Intrebare> GetIntrebari() => intrebari;

        public Intrebare CautaDupaId(int id) => intrebari.FirstOrDefault(i => i.Id == id);

        public void ModificaIntrebare(Intrebare iModif)
        {
            var index = intrebari.FindIndex(x => x.Id == iModif.Id);
            if (index != -1) intrebari[index] = iModif;
        }

        // --- CERINȚA 3 - ENTITATEA 2: Examen ---
        public void SalveazaExamen(Examen examen) => configExamen = examen;

        public Examen GetExamen() => configExamen;
    }
}