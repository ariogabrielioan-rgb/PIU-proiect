using LibrarieModele;
using System.Collections.Generic;
using System.Linq;

namespace NivelAccesDate
{
    public class AdministrareIntrebari
    {
        private List<Intrebare> listaIntrebari;

        public AdministrareIntrebari()
        {
            listaIntrebari = new List<Intrebare>();
        }

        public void AdaugaIntrebare(Intrebare intrebare)
        {
            intrebare.Id = listaIntrebari.Count + 1;
            listaIntrebari.Add(intrebare);
        }

        public List<Intrebare> GetIntrebari()
        {
            return listaIntrebari;
        }

        public Intrebare CautaDupaId(int id)
        {
            return listaIntrebari.FirstOrDefault(i => i.Id == id);
        }
    }
}