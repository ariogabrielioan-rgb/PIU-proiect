using Aplicatie;
using LibrarieEnumuri;
using LibrarieModele;
using Stocare;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Interface
{
    public partial class MainWindow : Window
    {
        IStocareData adminIntrebari;
        List<Intrebare> listaIntrebari;
        int indexCurent = 0;

        public MainWindow()
        {
            InitializeComponent();
            adminIntrebari = StocareFactory.GetAdministratorStocare();
        }

        private void BtnNavAdaugare_Click(object sender, RoutedEventArgs e) => SchimbaPanel(pnlAdaugare);
        private void BtnNavQuiz_Click(object sender, RoutedEventArgs e)
        {
            listaIntrebari = adminIntrebari.GetIntrebari();
            if (listaIntrebari.Count == 0)
            {
                MessageBox.Show("Nu există întrebări! Adaugă una mai întâi.");
                return;
            }
            indexCurent = 0;
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

        // --- VALIDARE ȘI SALVARE ---
        private void BtnSalveazaIntrebare_Click(object sender, RoutedEventArgs e)
        {
            // Verificăm dacă există câmpuri goale
            if (string.IsNullOrWhiteSpace(txtAddText.Text) ||
                string.IsNullOrWhiteSpace(txtAddRaspuns.Text) ||
                string.IsNullOrWhiteSpace(txtAddPunctaj.Text))
            {
                MessageBox.Show("Toate câmpurile sunt obligatorii!");
                return;
            }

            if (!int.TryParse(txtAddPunctaj.Text, out int punctaj))
            {
                MessageBox.Show("Punctajul trebuie să fie un număr!");
                return;
            }

            try
            {
                // Mapare directă pe Enum-ul tău (0=Usor, 1=Mediu, 2=Greu)
                Dificultate dif = (Dificultate)cmbDificultate.SelectedIndex;

                Intrebare noua = new Intrebare(txtAddText.Text, txtAddRaspuns.Text, punctaj, dif);
                adminIntrebari.AdaugaIntrebare(noua);

                MessageBox.Show("Întrebare adăugată!");

                // Resetare câmpuri
                txtAddText.Clear();
                txtAddRaspuns.Clear();
                txtAddPunctaj.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message);
            }
        }

        // --- VALIDARE ȘI QUIZ ---
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            // Validare: utilizatorul trebuie să scrie ceva înainte de a trece mai departe
            if (string.IsNullOrWhiteSpace(txtRaspunsUtilizator.Text))
            {
                MessageBox.Show("Te rugăm să introduci un răspuns!");
                return;
            }

            var intrebareActuala = listaIntrebari[indexCurent];
            if (intrebareActuala.Verifica(txtRaspunsUtilizator.Text))
            {
                lblFeedback.Content = "Corect!";
                lblFeedback.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                lblFeedback.Content = $"Greșit! (Corect: {intrebareActuala.RaspunsCorect})";
                lblFeedback.Foreground = System.Windows.Media.Brushes.Red;
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
                lblId.Content = $"Întrebarea {indexCurent + 1} / {listaIntrebari.Count}";
                txtTextIntrebare.Text = i.Text;
                lblDificultate.Content = $"Dificultate: {i.NivelDificultate}";
                txtRaspunsUtilizator.Clear();
                lblFeedback.Content = "";
            }
            else
            {
                MessageBox.Show("Ai terminat testul!");
                SchimbaPanel(pnlMeniu);
            }
        }
    }
}