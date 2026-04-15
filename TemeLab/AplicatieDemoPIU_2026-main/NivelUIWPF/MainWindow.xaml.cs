using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LibrarieModele;   // Referința către clasa Student
using NivelStocareDate; // Referința către StocareFactory

namespace NivelUIWPF
{
    [Flags]
    public enum CodEroare
    {
        TOTUL_OK = 0,
        NUME_INCORECT = 1,
        PRENUME_INCORECT = 2,
        NOTE_INCORECTE = 4,
        LUNGIME_NUME_MARE = 8,
        LUNGIME_PRENUME_MARE = 16
    }

    public partial class MainWindow : Window
    {
        IStocareData adminStudenti;
        private readonly Color CULOARE_OK = Colors.Gray;

        public MainWindow()
        {
            InitializeComponent();
            adminStudenti = StocareFactory.GetAdministratorStocare();
            AfiseazaStudenti();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNume.Text;
            string prenume = txtPrenume.Text;
            string noteString = txtNote.Text;

            // 1. Validare
            CodEroare codValidare = Validare(nume, prenume, noteString);

            if (codValidare != CodEroare.TOTUL_OK)
            {
                MarcheazaControaleInvalide(codValidare);
                return;
            }

            ReseteazaCuloriControale();

            try
            {
                // 2. Creare obiect folosind constructorul cu parametri
                Student studentNou = new Student(0, nume, prenume);

                // 3. Conversie Note (String -> int[]) pentru metoda SetNote din clasa ta
                int[] noteArray = noteString.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                           .Select(int.Parse)
                                           .ToArray();

                // Apelăm metoda SetNote din clasa Student pusă de tine
                studentNou.SetNote(noteArray);

                // 4. Salvare
                adminStudenti.AddStudent(studentNou);

                // 5. Resetare interfață
                txtNume.Clear();
                txtPrenume.Clear();
                txtNote.Clear();

                AfiseazaStudenti();
                MessageBox.Show("Studentul a fost adăugat!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Notele trebuie să fie numere (ex: 9 10 7).");
                txtNote.BorderBrush = Brushes.Red;
            }
        }

        private CodEroare Validare(string nume, string prenume, string note)
        {
            CodEroare rezultat = CodEroare.TOTUL_OK;

            if (string.IsNullOrWhiteSpace(nume)) rezultat |= CodEroare.NUME_INCORECT;
            else if (nume.Length > 15) rezultat |= CodEroare.LUNGIME_NUME_MARE;

            if (string.IsNullOrWhiteSpace(prenume)) rezultat |= CodEroare.PRENUME_INCORECT;
            else if (prenume.Length > 15) rezultat |= CodEroare.LUNGIME_PRENUME_MARE;

            if (string.IsNullOrWhiteSpace(note)) rezultat |= CodEroare.NOTE_INCORECTE;

            return rezultat;
        }

        private void MarcheazaControaleInvalide(CodEroare cod)
        {
            ReseteazaCuloriControale();
            if ((cod & CodEroare.NUME_INCORECT) != 0 || (cod & CodEroare.LUNGIME_NUME_MARE) != 0) txtNume.BorderBrush = Brushes.Red;
            if ((cod & CodEroare.PRENUME_INCORECT) != 0 || (cod & CodEroare.LUNGIME_PRENUME_MARE) != 0) txtPrenume.BorderBrush = Brushes.Red;
            if ((cod & CodEroare.NOTE_INCORECTE) != 0) txtNote.BorderBrush = Brushes.Red;
        }

        private void ReseteazaCuloriControale()
        {
            txtNume.BorderBrush = new SolidColorBrush(CULOARE_OK);
            txtPrenume.BorderBrush = new SolidColorBrush(CULOARE_OK);
            txtNote.BorderBrush = new SolidColorBrush(CULOARE_OK);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ReseteazaCuloriControale();
            List<Student> studenti = adminStudenti.GetStudenti();

            if (studenti != null && studenti.Count > 0)
            {
                Student ultimul = studenti.Last();

                // Folosim metoda Info() pe care o ai deja în clasă pentru afișare
                lblStudenti.Content = "Ultimul adăugat:\n" + ultimul.Info();

                txtNume.Clear();
                txtPrenume.Clear();
                txtNote.Clear();
            }
            else
            {
                lblStudenti.Content = "Nu există studenți în fișier.";
            }
        }

        private void AfiseazaStudenti()
        {
            List<Student> studenti = adminStudenti.GetStudenti();
            lblNrStudenti.Content = $"Nr. Studenți: {studenti.Count}";

            // Folosim metoda Info() pentru fiecare student din listă
            lblStudenti.Content = "Studenti:\n" + string.Join("\n",
                studenti.Select(s => s.Info()));
        }
    }
}