using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Student> GetStudenti() => studenti;

        public int GetNextIdStudent() => (studenti.Count == 0) ? 1 : studenti.Last().IdStudent + 1;

        // ... restul metodelor raman neschimbate
    }
}