using System;
using System.Linq;

namespace LibrarieModele
{
    public enum ProgramStudiu
    {
        Informatica = 1,
        Calculatoare = 2,
        Inginerie = 3,
        Management = 4
    }

    public class Student
    {
        private const char SEPARATOR_AFISARE = ' ';
        private const char SEPARATOR_FISIER = ';'; // Separator pentru salvarea in fisier

        private int[] note;

        public int IdStudent { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Grupa { get; set; } // Cerința 1: Noua caracteristică
        public ProgramStudiu Program { get; set; }

        public double Medie
        {
            get
            {
                if (note == null || note.Length == 0) return 0;
                return note.Average();
            }
        }

        public Student()
        {
            Nume = Prenume = Grupa = string.Empty;
            note = new int[0];
        }

        public Student(int idStudent, string nume, string prenume, string grupa)
        {
            IdStudent = idStudent;
            Nume = nume;
            Prenume = prenume;
            Grupa = grupa;
            note = new int[0];
        }

        // Cerința 2: Constructor pentru preluare date din fișier (string)
        public Student(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_FISIER);

            IdStudent = int.Parse(dateFisier[0]);
            Nume = dateFisier[1];
            Prenume = dateFisier[2];

            // Extragere ProgramStudiu (Cerinta 2)
            Program = (ProgramStudiu)Enum.Parse(typeof(ProgramStudiu), dateFisier[3]);

            // Extragere Grupa (Cerinta 1)
            Grupa = dateFisier[4];

            // Extragere Note (dacă există în fișier pe poziția 5)
            if (dateFisier.Length > 5 && !string.IsNullOrEmpty(dateFisier[5]))
            {
                note = dateFisier[5].Split(',').Select(int.Parse).ToArray();
            }
            else
            {
                note = new int[0];
            }
        }

        // Cerința 2: Metoda pentru scrierea în fișier
        public string ConversieLaSirPentruFisier()
        {
            string sNote = string.Join(",", note ?? new int[0]);

            // Format: ID;Nume;Prenume;Program(int);Grupa;Note
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                SEPARATOR_FISIER,
                IdStudent,
                (Nume ?? " "),
                (Prenume ?? " "),
                (int)Program,
                (Grupa ?? " "),
                sNote);
        }

        public void SetNote(int[] _note)
        {
            note = (_note != null) ? (int[])_note.Clone() : new int[0];
        }

        public int[] GetNote() => (int[])note.Clone();

        public string Info()
        {
            string sNote = (note != null && note.Length > 0)
                ? string.Join(SEPARATOR_AFISARE.ToString(), note)
                : "Fara note";

            return $"Id:{IdStudent} Nume:{Nume} Prenume:{Prenume} Grupa:{Grupa} " +
                   $"Program:{Program} Note:{sNote} Medie:{Medie:F2}";
        }
    }
}