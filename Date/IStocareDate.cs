using LibrarieModele;
using System.Collections.Generic;

namespace Stocare
{
    public interface IStocareData
    {
        // --- ENTITATEA 1: Intrebare ---
        void AdaugaIntrebare(Intrebare intrebare);
        List<Intrebare> GetIntrebari();
        Intrebare CautaDupaId(int id);
        void ModificaIntrebare(Intrebare intrebareModificata);

        // --- CERINȚA 3 - ENTITATEA 2: Examen (Setări/Rezultate) ---
        // Aici definim cum salvăm și citim setările examenului
        void SalveazaExamen(Examen examen);
        Examen GetExamen();
    }
}