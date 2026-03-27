using LibrarieModele;
using NivelStocareDate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvidentaStudenti
{
    class Program
    {
        public static void Main()
        {
            // Cerinta: Modificat pentru a folosi administrarea prin FISIER (pentru salvare permanenta)
            AdministrareStudenti_Fisier adminStudenti = new AdministrareStudenti_Fisier("studenti.txt");
            Student? studentNou = null;
            string optiune;

            do
            {
                Console.WriteLine("\nC. Citire informatii student de la tastatura");
                Console.WriteLine("I. Afisarea informatiilor despre ultimul student introdus");
                Console.WriteLine("A. Afisare studenti din FISIER"); // Cerinta: Preluare si afisare
                Console.WriteLine("S. Salvare student in FISIER"); // Cerinta: Salvare in fisier
                Console.WriteLine("X. Inchidere program");

                Console.Write("\nAlegeti o optiune: ");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        studentNou = CitireStudentTastatura();
                        break;
                    case "I":
                        if (studentNou != null) AfisareStudent(studentNou);
                        else Console.WriteLine("Nu exista un student citit recent.");
                        break;
                    case "A":
                        // Cerinta: Preluare din fisier si afisare
                        List<Student> studenti = adminStudenti.GetStudenti();
                        AfisareStudenti(studenti);
                        break;
                    case "S":
                        if (studentNou != null)
                        {
                            // Cerinta: Salvare in fisier
                            adminStudenti.AddStudent(studentNou);
                            Console.WriteLine("Student salvat in fisier.");
                        }
                        else Console.WriteLine("Studentul nu a fost initializat.");
                        break;
                    case "X": return;
                }
            } while (optiune != "X");
        }

        public static Student CitireStudentTastatura()
        {
            Console.Write("Introduceti numele: ");
            string nume = Console.ReadLine();

            Console.Write("Introduceti prenumele: ");
            string prenume = Console.ReadLine();

            // Cerinta 1: Citire Grupa
            Console.Write("Introduceti grupa: ");
            string grupa = Console.ReadLine();

            // Transmitem si grupa constructorului
            Student student = new Student(0, nume, prenume, grupa);

            Console.WriteLine("Alegeti Programul de Studiu:");
            foreach (int i in Enum.GetValues(typeof(ProgramStudiu)))
                Console.WriteLine($"{i} - {(ProgramStudiu)i}");

            bool succes = false;
            while (!succes)
            {
                try
                {
                    Console.Write("Introduceti cifra programului: ");
                    int optiuneProgram = int.Parse(Console.ReadLine());
                    if (!Enum.IsDefined(typeof(ProgramStudiu), optiuneProgram))
                        throw new Exception("Cod inexistent.");

                    student.Program = (ProgramStudiu)optiuneProgram;
                    succes = true;
                }
                catch { Console.WriteLine("Optiune invalida. Reincercati."); }
            }

            Console.Write("Numar de note: ");
            int.TryParse(Console.ReadLine(), out int nr);
            int[] note = new int[nr];
            for (int i = 0; i < nr; i++)
            {
                Console.Write($"Nota {i + 1}: ");
                int.TryParse(Console.ReadLine(), out note[i]);
            }
            student.SetNote(note);

            return student;
        }

        public static void AfisareStudent(Student student) => Console.WriteLine(student.Info());

        public static void AfisareStudenti(List<Student> studenti)
        {
            Console.WriteLine("\nLista studenti (contine Nume, Prenume, Grupa, Program, Medie):");
            foreach (var s in studenti) AfisareStudent(s);
        }
    }
}