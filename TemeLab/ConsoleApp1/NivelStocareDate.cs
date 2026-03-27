using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO; // Necesar pentru File, StreamWriter și StreamReader
using System.Linq;

namespace NivelStocareDate
{
    public class AdministrareStudenti_Fisier
    {
        private string numeFisier;

        public AdministrareStudenti_Fisier(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // Se asigură că fișierul există la pornirea programului
            using (Stream s = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
        }

        public void AddStudent(Student student)
        {
            student.IdStudent = GetNextIdStudent();

            // Cerința 1 & 2: Salvarea propriu-zisă în fișier folosind ConversieLaSirPentruFisier
            using (StreamWriter sw = File.AppendText(numeFisier))
            {
                sw.WriteLine(student.ConversieLaSirPentruFisier());
            }
        }

        public List<Student> GetStudenti()
        {
            List<Student> studenti = new List<Student>();

            // Cerința: Preluarea datelor din fișier
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    // Folosește constructorul Student(string) care extrage Grupa și Programul (Cerința 3)
                    studenti.Add(new Student(linie));
                }
            }
            return studenti;
        }

        public int GetNextIdStudent()
        {
            // Calculăm ID-ul pe baza ultimului student din fișier
            var lista = GetStudenti();
            return (lista.Count == 0) ? 1 : lista.Last().IdStudent + 1;
        }
    }
}