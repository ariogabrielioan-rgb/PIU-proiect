using System;
using System.Linq; // Necesar pentru .Average()

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
        // Constante
        private const char SEPARATOR = ' ';

        // Date membre
        private int[] note;

        // Proprietăți auto-implemented
        public int IdStudent { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public ProgramStudiu Program { get; set; }

        // Cerința 3: Proprietate Medie utilizând LINQ
        public double Medie
        {
            get
            {
                if (note == null || note.Length == 0)
                    return 0;
                return note.Average(); // LINQ
            }
        }

        // Constructor implicit
        public Student()
        {
            Nume = string.Empty;
            Prenume = string.Empty;
            note = new int[0];
        }

        // Constructor cu parametri
        public Student(int idStudent, string nume, string prenume)
        {
            IdStudent = idStudent;
            Nume = nume;
            Prenume = prenume;
            note = new int[0];
        }

        public void SetNote(int[] _note)
        {
            if (_note != null)
                note = (int[])_note.Clone();
            else
                note = new int[0];
        }

        public int[] GetNote()
        {
            return (int[])note.Clone();
        }

        public string Info()
        {
            string sNote = (note != null && note.Length > 0)
                ? string.Join(SEPARATOR.ToString(), note)
                : "Fara note";

            // Am adăugat afișarea Programului și a Mediei conform cerinței
            return $"Id:{IdStudent} Nume:{Nume ?? "NECUNOSCUT"} Prenume:{Prenume ?? "NECUNOSCUT"} " +
                   $"Program:{Program} Note:{sNote} Medie:{Medie:F2}";
        }
    }
}
