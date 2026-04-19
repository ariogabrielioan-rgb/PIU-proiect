using Aplicatie;
using LibrarieEnumuri;
using LibrarieModele;
using Stocare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Interface
{
    public partial class MainWindow : Window
    {
        private const int MIN_LUNGIME_TEXT = 5;
        private const int MIN_PUNCTAJ = 1;
        private const int MAX_PUNCTAJ = 100;

        IStocareData adminIntrebari;
        List<Intrebare> listaIntrebari;
        int indexCurent = 0;
        int scorCurent = 0;

        public MainWindow()
        {
            InitializeComponent();
            adminIntrebari = StocareFactory.GetAdministratorStocare();
        }

        private void BtnNavAdaugare_Click(object sender, RoutedEventArgs e) => SchimbaPanel(pnlAdaugare);

        private void BtnNavQuiz_Click(object sender, RoutedEventArgs e)
        {
            listaIntrebari = adminIntrebari.GetIntrebari();
            if (listaIntrebari == null || listaIntrebari.Count == 0)
            {
                MessageBox.Show("Nu există întrebări! Adaugă una mai întâi.");
                return;
            }

            indexCurent = 0;
            scorCurent = 0;
            lblScorLive.Content = "Scor: 0"; // Resetare vizuală scor

            SchimbaPanel(pnlQuiz);
            AfiseazaIntrebareaCurenta();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => SchimbaPanel(pnlMeniu);

        private void SchimbaPanel(FrameworkElement panelActiv)
        {
            pnlMeniu.Visibility = Visibility.Collapsed;
            pnlAdaugare.Visibility = Visibility.Collapsed;
            pnlQuiz.Visibility = Visibility.Collapsed;
            panelActiv.Visibility = Visibility.Visible;
        }

        private void BtnSalveazaIntrebare_Click(object sender, RoutedEventArgs e)
        {
            lblLgText.Foreground = Brushes.Black;
            lblLgRaspuns.Foreground = Brushes.Black;
            lblLgPunctaj.Foreground = Brushes.Black;

            string erori = "";

            if (string.IsNullOrWhiteSpace(txtAddText.Text) || txtAddText.Text.Length < MIN_LUNGIME_TEXT)
            {
                erori += $"- Textul trebuie să aibă minim {MIN_LUNGIME_TEXT} caractere.\n";
                lblLgText.Foreground = Brushes.Red;
            }

            if (string.IsNullOrWhiteSpace(txtAddRaspuns.Text))
            {
                erori += "- Răspunsul nu poate fi gol.\n";
                lblLgRaspuns.Foreground = Brushes.Red;
            }

            if (!int.TryParse(txtAddPunctaj.Text, out int punctaj) || punctaj < MIN_PUNCTAJ || punctaj > MAX_PUNCTAJ)
            {
                erori += $"- Punctajul trebuie să fie între {MIN_PUNCTAJ} și {MAX_PUNCTAJ}.\n";
                lblLgPunctaj.Foreground = Brushes.Red;
            }

            if (!string.IsNullOrEmpty(erori))
            {
                MessageBox.Show(erori, "Erori de validare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Dificultate dif = (Dificultate)cmbDificultate.SelectedIndex;
                Intrebare noua = new Intrebare(txtAddText.Text, txtAddRaspuns.Text, punctaj, dif);
                adminIntrebari.AdaugaIntrebare(noua);

                MessageBox.Show("Întrebare adăugată cu succes!");
                txtAddText.Clear();
                txtAddRaspuns.Clear();
                txtAddPunctaj.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Eroare: " + ex.Message); }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRaspunsUtilizator.Text))
            {
                lblFeedback.Content = "Introdu un răspuns!";
                lblFeedback.Foreground = Brushes.OrangeRed;
                return;
            }

            var intrebareActuala = listaIntrebari[indexCurent];

            if (intrebareActuala.Verifica(txtRaspunsUtilizator.Text))
            {
                scorCurent += intrebareActuala.Punctaj;
                lblFeedback.Content = $"Corect! (+{intrebareActuala.Punctaj} pct)";
                lblFeedback.Foreground = Brushes.Green;
            }
            else
            {
                lblFeedback.Content = $"Greșit! (Corect: {intrebareActuala.RaspunsCorect})";
                lblFeedback.Foreground = Brushes.Red;
            }

            MessageBox.Show(lblFeedback.Content.ToString());
            indexCurent++;
            AfiseazaIntrebareaCurenta();
        }

        private void AfiseazaIntrebareaCurenta()
        {
            if (indexCurent < listaIntrebari.Count)
            {
                var i = listaIntrebari[indexCurent];

                // Aici am scos "| Scor: {scorCurent}"
                lblId.Content = $"Întrebarea {indexCurent + 1} / {listaIntrebari.Count}";

                // Actualizăm scorul doar în colț
                lblScorLive.Content = $"Scor: {scorCurent}";

                txtTextIntrebare.Text = i.Text;
                lblDificultate.Content = $"Dificultate: {i.NivelDificultate} | Puncte: {i.Punctaj}";
                txtRaspunsUtilizator.Clear();
                lblFeedback.Content = "";
            }
            else
            {
                int punctajTotal = listaIntrebari.Sum(x => x.Punctaj);
                MessageBox.Show($"Test finalizat!\nScor final: {scorCurent} / {punctajTotal} puncte.", "Rezultat Test");
                SchimbaPanel(pnlMeniu);
            }
        }
    }
}