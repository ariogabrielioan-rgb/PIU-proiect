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
        IStocareData adminIntrebari;
        List<Intrebare> listaIntrebari;
        int indexCurent = 0;
        int scorCurent = 0;

        public MainWindow()
        {
            InitializeComponent();
            adminIntrebari = StocareFactory.GetAdministratorStocare();
        }

        // --- NAVIGARE ---
        private void BtnNavAdaugare_Click(object sender, RoutedEventArgs e) => SchimbaPanel(pnlAdaugare);
        private void BtnBack_Click(object sender, RoutedEventArgs e) => SchimbaPanel(pnlMeniu);

        private void BtnNavQuiz_Click(object sender, RoutedEventArgs e)
        {
            listaIntrebari = adminIntrebari.GetIntrebari();
            if (listaIntrebari == null || listaIntrebari.Count == 0)
            {
                MessageBox.Show("Nu există întrebări!");
                return;
            }

            indexCurent = 0;
            scorCurent = 0;
            lblScorLive.Content = "Scor: 0";
            SchimbaPanel(pnlQuiz);
            AfiseazaIntrebareaCurenta();
        }

        private void SchimbaPanel(FrameworkElement panelActiv)
        {
            pnlMeniu.Visibility = pnlAdaugare.Visibility = pnlQuiz.Visibility = Visibility.Collapsed;
            panelActiv.Visibility = Visibility.Visible;
        }

        // --- ADAUGARE (LOGICA SIMPLĂ) ---
        private void BtnSalveazaIntrebare_Click(object sender, RoutedEventArgs e)
        {
            // 1. Verificăm textul întrebării
            if (string.IsNullOrWhiteSpace(txtAddText.Text))
            {
                MessageBox.Show("Eroare: Trebuie să introduci textul întrebării!");
                return; // Oprim aici dacă e gol
            }

            // 2. Verificăm răspunsul
            if (string.IsNullOrWhiteSpace(txtAddRaspuns.Text))
            {
                MessageBox.Show("Eroare: Trebuie să introduci un răspuns corect!");
                return; // Oprim aici dacă e gol
            }

            // 3. Verificăm punctajul (să nu fie gol)
            if (string.IsNullOrWhiteSpace(txtAddPunctaj.Text))
            {
                MessageBox.Show("Eroare: Trebuie să introduci un punctaj!");
                return;
            }
            // 2. Aflăm dificultatea din RadioButtons (Am lăsat-o aici ca să fie clar)
            Dificultate dif = Dificultate.Usor;
            if (rbMediu.IsChecked == true) dif = Dificultate.Mediu;
            if (rbGreu.IsChecked == true) dif = Dificultate.Greu;

            try
            {
                int punctaj = int.Parse(txtAddPunctaj.Text);
                Intrebare noua = new Intrebare(txtAddText.Text, txtAddRaspuns.Text, punctaj, dif);
                adminIntrebari.AdaugaIntrebare(noua);

                MessageBox.Show("Salvat!");

                // Resetăm câmpurile
                txtAddText.Clear();
                txtAddRaspuns.Clear();
                txtAddPunctaj.Clear();
                rbUsor.IsChecked = true;
            }
            catch { MessageBox.Show("Punctajul trebuie să fie un număr!"); }
        }

        // --- QUIZ (LOGICA SIMPLĂ CU VALIDARE) ---
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            // VALIDARE: Dacă nu a scris nimic, dăm eroare și oprim funcția
            if (string.IsNullOrWhiteSpace(txtRaspunsUtilizator.Text))
            {
                MessageBox.Show("Trebuie să introduci un răspuns înainte de a trimite!");
                return; // "return" oprește funcția aici, deci nu trece la restul codului
            }

            var intrebare = listaIntrebari[indexCurent];

            // Verificăm dacă răspunsul e corect
            if (intrebare.Verifica(txtRaspunsUtilizator.Text))
            {
                scorCurent += intrebare.Punctaj;
                MessageBox.Show("Corect!✅ ");
            }
            else
            {
                MessageBox.Show($"Greșit!❌ Răspunsul corect era: {intrebare.RaspunsCorect}");
            }

            // Abia după ce a răspuns valid, trecem la următoarea
            indexCurent++;
            AfiseazaIntrebareaCurenta();
        }
        private void AfiseazaIntrebareaCurenta()
        {
            if (indexCurent < listaIntrebari.Count)
            {
                var i = listaIntrebari[indexCurent];
                lblId.Content = $"Întrebarea {indexCurent + 1} / {listaIntrebari.Count}";
                lblScorLive.Content = $"Scor: {scorCurent}";
                txtTextIntrebare.Text = i.Text;
                lblDificultate.Content = $"Dificultate: {i.NivelDificultate}";
                txtRaspunsUtilizator.Clear();
            }
            else
            {
                MessageBox.Show($"Gata! Scor final: {scorCurent}");
                SchimbaPanel(pnlMeniu);
            }
        }
    }
}