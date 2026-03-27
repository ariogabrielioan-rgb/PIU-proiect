using Stocare;

namespace Aplicatie
{
    public static class StocareFactory
    {
        private const string CALE_FISIER = @"..\..\..\..\intrebari.txt";

        public static IStocareData GetAdministratorStocare()
        {
            // Metoda A: Stocare în fișier text (Activă)
            //return new AdministrareIntrebariFisierText(CALE_FISIER);

            // Metoda B: Stocare în memorie (Dezactivată - se folosește pentru teste rapide)
             return new AdministrareIntrebariMemorie();
        }
    }
}