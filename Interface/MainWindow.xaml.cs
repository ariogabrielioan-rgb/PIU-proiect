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
                MessageBox.Show("Nu există întrebări! Adaugă una mai întâi.");
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

        // --- ADAUGARE ÎNTREBARE ---
        private void BtnSalveazaIntrebare_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddText.Text))
            {
                MessageBox.Show("Introdu textul întrebării!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddVarA.Text) ||
                string.IsNullOrWhiteSpace(txtAddVarB.Text) ||
                string.IsNullOrWhiteSpace(txtAddVarC.Text))
            {
                MessageBox.Show("Completează toate cele 3 variante de răspuns!");
                return;
            }

            string raspunsCorectCalculat = "";
            if (chkAddA.IsChecked == true) raspunsCorectCalculat += "A";
            if (chkAddB.IsChecked == true) raspunsCorectCalculat += "B";
            if (chkAddC.IsChecked == true) raspunsCorectCalculat += "C";

            if (string.IsNullOrEmpty(raspunsCorectCalculat))
            {
                MessageBox.Show("Bifează cel puțin o variantă ca fiind corectă!");
                return;
            }

            Dificultate dif = rbMediu.IsChecked == true ? Dificultate.Mediu :
                             rbGreu.IsChecked == true ? Dificultate.Greu : Dificultate.Usor;

            try
            {
                int punctaj = int.Parse(txtAddPunctaj.Text);
                Intrebare noua = new Intrebare(txtAddText.Text, raspunsCorectCalculat, punctaj, dif);

                // IMPORTANT: Aceste proprietăți trebuie să existe în clasa Intrebare
                noua.VariantaA = txtAddVarA.Text;
                noua.VariantaB = txtAddVarB.Text;
                noua.VariantaC = txtAddVarC.Text;

                adminIntrebari.AdaugaIntrebare(noua);

                MessageBox.Show("Salvat cu succes! ✅");

                // Resetare câmpuri
                txtAddText.Clear();
                txtAddVarA.Clear(); txtAddVarB.Clear(); txtAddVarC.Clear();
                chkAddA.IsChecked = chkAddB.IsChecked = chkAddC.IsChecked = false;
                txtAddPunctaj.Clear();
                rbUsor.IsChecked = true;
            }
            catch { MessageBox.Show("Punctajul trebuie să fie un număr!"); }
        }

        // --- LOGICA QUIZ ---
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            string raspunsUtilizator = "";
            if (chkA.IsChecked == true) raspunsUtilizator += "A";
            if (chkB.IsChecked == true) raspunsUtilizator += "B";
            if (chkC.IsChecked == true) raspunsUtilizator += "C";

            if (string.IsNullOrEmpty(raspunsUtilizator))
            {
                MessageBox.Show("Bifează măcar o variantă!");
                return;
            }

            var intrebare = listaIntrebari[indexCurent];

            if (intrebare.Verifica(raspunsUtilizator))
            {
                scorCurent += intrebare.Punctaj;
                MessageBox.Show("Corect! ✅");
            }
            else
            {
                MessageBox.Show($"Greșit! ❌ Varianta corect era: {intrebare.RaspunsCorect}");
            }

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

                // Punem textele variantelor pe CheckBox-uri
                // Test de diagnostic:
                chkA.Content = "A)"+i.VariantaA;
                chkB.Content = "B)" + i.VariantaB;
                chkC.Content = "C)" + i.VariantaC;

                // Resetăm bifele pentru noua întrebare
                chkA.IsChecked = chkB.IsChecked = chkC.IsChecked = false;
            }
            else
            {
                MessageBox.Show($"Gata! Scor final: {scorCurent}");
                SchimbaPanel(pnlMeniu);
            }
        }
    }
}