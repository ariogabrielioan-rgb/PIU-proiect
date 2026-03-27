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
        private const char SEPARATOR_FISIER = ';';

        private int[] note;

        public int IdStudent { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Grupa { get; set; } // Cerința 1
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

        // Cerința 3: Constructor actualizat pentru extragere ProgramStudiu și Grupa
        public Student(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_FISIER);

            // Verificare lungime array pentru a evita erori la linii malformate
            if (dateFisier.Length >= 5)
            {
                IdStudent = int.Parse(dateFisier[0]);
                Nume = dateFisier[1];
                Prenume = dateFisier[2];

                // Extragere ProgramStudiu (Cerința 3)
                Program = (ProgramStudiu)Enum.Parse(typeof(ProgramStudiu), dateFisier[3]);

                // Extragere Grupa (Cerința 1)
                Grupa = dateFisier[4];

                // Extragere Note (poziția 5)
                if (dateFisier.Length > 5 && !string.IsNullOrEmpty(dateFisier[5]))
                {
                    note = dateFisier[5].Split(',').Select(int.Parse).ToArray();
                }
                else
                {
                    note = new int[0];
                }
            }
        }

        // Cerința 2: Metodă actualizată pentru scrierea ProgramStudiu în fișier
        public string ConversieLaSirPentruFisier()
        {
            string sNote = string.Join(",", note ?? new int[0]);

            // Format: ID;Nume;Prenume;Program(int);Grupa;Note
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                SEPARATOR_FISIER,
                IdStudent,
                (Nume ?? string.Empty),
                (Prenume ?? string.Empty),
                (int)Program,
                (Grupa ?? string.Empty),
                sNote);
        }

        public void SetNote(int[] _note)
        {
            note = (_note != null) ? (int[])_note.Clone() : new int[0];
        }

        public int[] GetNote() => (int[])note?.Clone() ?? new int[0];

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