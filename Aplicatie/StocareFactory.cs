using System;
using Stocare; // Referință către proiectul care conține IStocareData și clasele de administrare

namespace Aplicatie
{
    // Clasa este statică deoarece nu avem nevoie să creăm obiecte "StocareFactory", 
    // ci doar să apelăm metoda de fabricare a stocării.
    public static class StocareFactory
    {
        // Păstrăm calea ta relativă pentru a găsi fișierul în folderul rădăcină al soluției
        private const string CALE_FISIER = @"..\..\..\..\intrebari.txt";

        public static IStocareData GetAdministratorStocare()
        {
            // Factory-ul "fabrică" și returnează obiectul de tip AdministrareIntrebariFisierText,
            // dar îl livrează sub formă de IStocareData (interfață).

            return new AdministrareIntrebariFisierText(CALE_FISIER);
        }
    }
}