using System;
using System.Collections.Generic;
using System.Linq; // Necesar pentru .Select și .Join
using System.Windows;
using LibrarieModele;
using NivelStocareDate;

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
        // 1. Declarăm variabila la nivel de clasă
        IStocareData adminStudenti;

        public MainWindow()
        {
            InitializeComponent();

            // 2. Inițializăm stocarea
            adminStudenti = StocareFactory.GetAdministratorStocare();

            // 3. Afișăm datele inițiale
            AfiseazaStudenti();
        }

        // Metodă pentru butonul "Adaugă"
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            // Preluăm datele din interfață
            string nume = txtNume.Text;
            string prenume = txtPrenume.Text;
            string note = txtNote.Text;

            // Validare simplă
            if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(prenume))
            {
                MessageBox.Show("Numele și prenumele sunt obligatorii!");
                return;
            }

            // Creăm obiectul Student
            // Notă: Verifică dacă constructorul tău acceptă aceste argumente
            Student studentNou = new Student(0,nume, prenume);

            // Dacă ai o metodă sau proprietate pentru note în clasa Student, o apelăm aici:
            // studentNou.Note = note; 

            // Salvăm studentul prin nivelul de stocare
            adminStudenti.AddStudent(studentNou);

            // Curățăm câmpurile după salvare
            txtNume.Clear();
            txtPrenume.Clear();
            txtNote.Clear();

            // Refresh la listă pentru a vedea noul student
            AfiseazaStudenti();

            MessageBox.Show("Studentul a fost adăugat!");
        }

        // Mutăm logica de afișare aici pentru a fi reutilizabilă
        private void AfiseazaStudenti()
        {
            List<Student> studenti = adminStudenti.GetStudenti();

            lblNrStudenti.Content = $"Numar studenti: {studenti.Count}";

            lblStudenti.Content = "Studenti:\n" + string.Join("\n",
                studenti.Select(s => $"{s.IdStudent}: {s.Nume} {s.Prenume}"));
        }
    }
}