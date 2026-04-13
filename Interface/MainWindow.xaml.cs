using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LibrarieModele;
using LibrarieEnumuri;
using Aplicatie;
using Stocare;

namespace Interface
{
    // Trebuie să fie "public partial class" pentru ca InitializeComponent să funcționeze
    public partial class MainWindow : Window
    {
        IStocareData adminIntrebari;
        List<Intrebare> listaIntrebari;
        int indexCurent = 0;

        public MainWindow()
        {
            InitializeComponent(); // Dacă clasa e "partial", eroarea CS0103 dispare

            adminIntrebari = StocareFactory.GetAdministratorStocare();
            listaIntrebari = adminIntrebari.GetIntrebari();

            AfiseazaIntrebareaCurenta();
        }

        private void AfiseazaIntrebareaCurenta()
        {
            if (listaIntrebari != null && indexCurent < listaIntrebari.Count)
            {
                var intrebare = listaIntrebari[indexCurent];

                lblId.Content = $"Întrebarea {indexCurent + 1} din {listaIntrebari.Count}";
                txtTextIntrebare.Text = intrebare.Text;
                lblDificultate.Content = $"Dificultate: {intrebare.NivelDificultate}";

                txtRaspunsUtilizator.Text = string.Empty;
                lblFeedback.Content = "";
            }
            else
            {
                MessageBox.Show("Testul s-a terminat!");
                btnNext.IsEnabled = false;
            }
        }

        // Această metodă rezolvă eroarea CS1061 (lipsa definiției pentru Click)
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (indexCurent < listaIntrebari.Count)
            {
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

                // Așteptăm puțin sau trecem direct? 
                // Pentru simplitate, trecem direct la următoarea după un MessageBox
                MessageBox.Show(lblFeedback.Content.ToString());

                indexCurent++;
                AfiseazaIntrebareaCurenta();
            }
        }
    }
}