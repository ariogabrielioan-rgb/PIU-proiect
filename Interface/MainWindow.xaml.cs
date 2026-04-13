using Aplicatie; // Necesar pentru StocareFactory
using LibrarieModele;
using Stocare;   // Necesar pentru IStocareData
using System;
using System.Linq;
using System.Windows;
using LibrarieEnumuri;
namespace Interface
{
    public partial class MainWindow : Window
    {
        // Declari administratorul de stocare la nivel de clasă
        IStocareData adminIntrebari;

        public MainWindow()
        {
            InitializeComponent();

            // 1. Iei obiectul de stocare direct din Factory
            adminIntrebari = StocareFactory.GetAdministratorStocare();

            // 2. Încarci datele (metoda GetIntrebari() trebuie să existe în IStocareData)
            var listaIntrebari = adminIntrebari.GetIntrebari();

            // 3. Afișezi prima întrebare dacă lista nu e goală
            if (listaIntrebari != null && listaIntrebari.Any())
            {
                Intrebare primaIntrebare = listaIntrebari.First();
                PopuleazaDate(primaIntrebare);
            }
            else
            {
                MessageBox.Show("Nu au fost găsite întrebări în fișier.");
            }
        }

        // Metodă auxiliară pentru a păstra constructorul curat
        private void PopuleazaDate(Intrebare intrebare)
        {
            lblId.Content = $"ID: {intrebare.Id}";
            txtTextIntrebare.Text = intrebare.Text;
            lblDificultate.Content = $"Dificultate: {intrebare.NivelDificultate}";

            // Opțional: O mică stilizare din cod (Cerința 3)
            if (intrebare.NivelDificultate == Dificultate.Greu)
                lblDificultate.Foreground = System.Windows.Media.Brushes.Red;
        }
    }
}