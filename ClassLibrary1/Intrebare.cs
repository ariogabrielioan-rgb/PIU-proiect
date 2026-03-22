using System;
namespace LibrarieModele
{
    // Enum simplu
    public enum Dificultate
    {
        Usor,
        Mediu,
        Greu
    }

    // Enum cu atributul Flags pentru a permite selecții multiple
    [Flags]
    public enum SetariExamen
    {
        FaraSetari = 0,
        TimpLimitat = 1,
        RandomizeIntrebari = 2,
        PunctajDiferit = 4,
        ModHardcore = TimpLimitat | RandomizeIntrebari | PunctajDiferit
    }

    public class Intrebare
    {
        private string textIntrebare;
        private string raspunsCorect;
        private int punctaj;

        public int Id { get; set; }
        public bool EsteImportanta => punctaj > 5;

        // Câmp de tip enum simplu
        public Dificultate NivelDificultate { get; set; }

        public Intrebare()
        {
            textIntrebare = string.Empty;
            raspunsCorect = string.Empty;
            punctaj = 0;
            NivelDificultate = Dificultate.Usor; // Default
        }

        public Intrebare(string _text, string _raspuns, int _punctaj, Dificultate _dificultate)
        {
            textIntrebare = _text;
            raspunsCorect = _raspuns;
            punctaj = _punctaj;
            NivelDificultate = _dificultate;
        }

        public string Info()
        {
            if (string.IsNullOrEmpty(textIntrebare))
                return "INTREBARE INCOMPLETA";

            return $"Intrebarea {Id} [{NivelDificultate}]: {textIntrebare} ({punctaj} puncte)";
        }

        public bool Verifica(string raspunsUtilizator)
        {
            return raspunsCorect.ToLower() == raspunsUtilizator.ToLower();
        }

        public int GetPunctaj()
        {
            return punctaj;
        }
    }

    // O clasă nouă care să stocheze setările examenului folosind enum-ul cu Flags
    public class Examen
    {
        public SetariExamen OptiuniActive { get; set; }
        public Examen()
        {
            // Setăm implicit o opțiune sau mai multe
            OptiuniActive = SetariExamen.RandomizeIntrebari | SetariExamen.TimpLimitat;
        }
    }
}