using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Linq; // Necesar pentru FirstOrDefault, Where, ToList

namespace NivelStocareDate
{
    public class AdministrareStudentiMemorie
    {
        private List<Student> studenti;

        public AdministrareStudentiMemorie()
        {
            studenti = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            student.IdStudent = GetNextIdStudent();
            studenti.Add(student);
        }

        public List<Student> GetStudenti()
        {
            return studenti;
        }

        public Student? GetStudent(int idStudent)
        {
            // Utilizare LINQ pentru căutare după ID
            return studenti.FirstOrDefault(s => s.IdStudent == idStudent);
        }

        // Cerința 3: Utilizare LINQ în metoda de căutare după nume și prenume
        public Student? GetStudent(string nume, string prenume)
        {
            return studenti.FirstOrDefault(student =>
                student.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) &&
                student.Prenume.Equals(prenume, StringComparison.OrdinalIgnoreCase)
            );
        }

        // Cerința 4: Utilizare LINQ pentru căutare după nume (returnează listă)
        public List<Student> GetStudentiByNume(string nume)
        {
            return studenti
                .Where(s => s.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool UpdateStudent(Student s)
        {
            throw new Exception("Optiunea UpdateStudent nu este implementata");
        }

        public int GetNextIdStudent()
        {
            if (studenti.Count == 0)
            {
                return 1;
            }

            return studenti.Last().IdStudent + 1;
        }
    }
}