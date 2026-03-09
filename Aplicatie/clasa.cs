namespace SistemExamen
{
    public class Intrebare
    {
        // Date membre private
        private string textIntrebare;
        private string raspunsCorect;
        private int punctaj;

        // Proprietăți auto-implemented
        public int Id { get; set; }

        // Proprietate computed pentru a vedea dacă întrebarea este "valoroasa" (peste 5 puncte)
        public bool EsteImportanta => punctaj > 5;

        // Constructor fără parametri
        public Intrebare()
        {
            textIntrebare = string.Empty;
            raspunsCorect = string.Empty;
            punctaj = 0;
        }

        // Constructor cu parametri
        public Intrebare(string _text, string _raspuns, int _punctaj)
        {
            textIntrebare = _text;
            raspunsCorect = _raspuns;
            punctaj = _punctaj;
        }

        // Metodă care returnează textul întrebării formatat
        public string Info()
        {
            if (string.IsNullOrEmpty(textIntrebare))
                return "INTREBARE INCOMPLETA";

            return $"Intrebarea {Id}: {textIntrebare} ({punctaj} puncte)";
        }

        // Metodă pentru verificarea răspunsului
        public bool Verifica(string raspunsUtilizator)
        {
            return raspunsCorect.ToLower() == raspunsUtilizator.ToLower();
        }

        // Metodă pentru a obține punctajul (getter)
        public int GetPunctaj()
        {
            return punctaj;
        }
    }
}