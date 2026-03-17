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
            AdministrareStudentiMemorie adminStudenti = new AdministrareStudentiMemorie();
            Student? studentNou = null;
            string optiune;

            List<Student> studenti = adminStudenti.GetStudenti();

            do
            {
                Console.WriteLine("\nC. Citire informatii student de la tastatura");
                Console.WriteLine("I. Afisarea informatiilor despre ultimul student introdus");
                Console.WriteLine("A. Afisare studenti din lista");
                Console.WriteLine("Z. Afisare studenti fara note din lista");
                Console.WriteLine("S. Salvare student in lista");
                Console.WriteLine("X. Inchidere program");

                Console.WriteLine("Alegeti o optiune:");
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
                        AfisareStudenti(studenti);
                        break;
                    case "Z":
                        AfisareStudentiFaraNote(studenti);
                        break;
                    case "S":
                        if (studentNou != null)
                        {
                            adminStudenti.AddStudent(studentNou);
                            Console.WriteLine("Student salvat.");
                        }
                        else Console.WriteLine("Studentul nu a fost initializat");
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");
                        break;
                }
            } while (optiune != "X");
        }

        public static Student CitireStudentTastatura()
        {
            Console.WriteLine("Introduceti numele:");
            string nume = Console.ReadLine();

            Console.WriteLine("Introduceti prenumele:");
            string prenume = Console.ReadLine();

            Student student = new Student(0, nume, prenume);

            // --- Cerinta 1 & 2: Afisare Enum si Tratare Exceptii ---
            Console.WriteLine("Alegeti Programul de Studiu:");
            foreach (int i in Enum.GetValues(typeof(ProgramStudiu)))
            {
                Console.WriteLine($"{i} - {(ProgramStudiu)i}");
            }

            bool succes = false;
            while (!succes)
            {
                try
                {
                    Console.Write("Introduceti optiunea (cifra): ");
                    string input = Console.ReadLine();
                    int optiuneProgram = int.Parse(input);

                    if (!Enum.IsDefined(typeof(ProgramStudiu), optiuneProgram))
                        throw new Exception("Codul programului nu exista in lista.");

                    student.Program = (ProgramStudiu)optiuneProgram;
                    succes = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Eroare: Te rog sa introduci un numar valid.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare: {ex.Message}");
                }
            }
            // -------------------------------------------------------

            Console.WriteLine("Introduceti numarul de note:");
            int.TryParse(Console.ReadLine(), out int nrNote);

            int[] note = new int[nrNote];
            for (int i = 0; i < nrNote; i++)
            {
                Console.Write($"Nota {i + 1}: ");
                if (int.TryParse(Console.ReadLine(), out int nota))
                    note[i] = nota;
                else
                    note[i] = 0;
            }
            student.SetNote(note);

            return student;
        }

        public static void AfisareStudent(Student student)
        {
            Console.WriteLine(student.Info());
        }

        public static void AfisareStudenti(List<Student> studenti)
        {
            Console.WriteLine("Studentii sunt:");
            foreach (Student student in studenti)
            {
                AfisareStudent(student);
            }
        }

        public static void AfisareStudentiFaraNote(List<Student> studenti)
        {
            Console.WriteLine("Studentii fara note (sau < 2 note) sunt:");
            var studentiSelectati = studenti.Where(student => student.GetNote().Length < 2);
            foreach (Student student in studentiSelectati)
            {
                AfisareStudent(student);
            }
        }
    }
}