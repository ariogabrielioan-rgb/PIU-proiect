using System;
using System.Linq;
using LibrarieEnumuri; // IMPORTANT: Referința către proiectul de Enum-uri

namespace LibrarieModele
{
    public class Intrebare
    {
        // Separator pentru fișierul text
        private const char SEPARATOR_FISIER = ';';

        // Proprietăți auto-implemented
        public int Id { get; set; }
        public string Text { get; set; }
        public string RaspunsCorect { get; set; }
        public int Punctaj { get; set; }
        public Dificultate NivelDificultate { get; set; }

        // Constructor implicit
        public Intrebare()
        {
            Text = string.Empty;
            RaspunsCorect = string.Empty;
            Punctaj = 0;
            NivelDificultate = Dificultate.Usor;
        }

        // Constructor cu parametri (folosit la citirea de la tastatură)
        public Intrebare(string _text, string _raspuns, int _punctaj, Dificultate _dificultate)
        {
            Text = _text;
            RaspunsCorect = _raspuns;
            Punctaj = _punctaj;
            NivelDificultate = _dificultate;
        }

        // --- CERINȚA 3: Constructor care preia un șir de caractere (din fișier) ---
        public Intrebare(string linieFisier)
        {
            var date = linieFisier.Split(SEPARATOR_FISIER);

            // Format așteptat: Id;Text;Raspuns;Punctaj;Dificultate
            Id = int.Parse(date[0]);
            Text = date[1];
            RaspunsCorect = date[2];
            Punctaj = int.Parse(date[3]);
            NivelDificultate = (Dificultate)int.Parse(date[4]);
        }

        // --- CERINȚA 2: Metodă pentru scrierea în fișier (include Enum-ul) ---
        public string ConversieLaSirPentruFisier()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                SEPARATOR_FISIER,
                Id,
                (Text ?? string.Empty),
                (RaspunsCorect ?? string.Empty),
                Punctaj,
                (int)NivelDificultate); // Salvăm valoarea numerică a Enum-ului
        }

        public string Info()
        {
            return $"#{Id} [{NivelDificultate}]: {Text} ({Punctaj} puncte)";
        }

        public bool Verifica(string raspunsUtilizator)
        {
            return RaspunsCorect.Trim().ToLower() == raspunsUtilizator.Trim().ToLower();
        }

        public int GetPunctaj() => Punctaj;
    }

    // A doua entitate din cadrul aplicației (Cerința 3 de acasă)
    public class Examen
    {
        public SetariExamen OptiuniActive { get; set; }
        public Examen()
        {
            // Setări implicite folosind Flags
            OptiuniActive = SetariExamen.RandomizeIntrebari | SetariExamen.TimpLimitat;
        }
    }
}