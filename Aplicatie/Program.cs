namespace ExempluClase
{
    public class FiguraGeometrica
    {
        // data membra privata
        int[] dimensiuniLaturi;

        // proprietati auto-implemented
        public string Denumire { get; set; }
        public int NrLaturi { get; set; }

        // proprietate computed – varianta 1
        public bool EstePoligon
        {
            get
            {
                return NrLaturi >= 3;
            }
        }

        // proprietate computed – varianta 2 (expresie Lambda)
        public bool EstePoligon_v2 => NrLaturi >= 3;

        public void SetDimensiuniLaturi(int[] _dimensiuniLaturi)
        {
            dimensiuniLaturi = new int[_dimensiuniLaturi.Length];
            _dimensiuniLaturi.CopyTo(dimensiuniLaturi, 0);
        }

        public int[] GetDimensiuniLaturi()
        {
            /* returneaza o copie a vectorului, astfel încât utilizatorii acestei 
               clase să nu poata modifica în mod direct conținutul vectorului */
            return (int[])dimensiuniLaturi.Clone();
        }


        //	Constructor fara parametri
        public FiguraGeometrica()
        {
            Denumire = string.Empty;
            NrLaturi = 0;
        }

        //	Constructor cu parametri
        public FiguraGeometrica(string _denumire, int _nrLaturi)
        {
            Denumire = _denumire;
            NrLaturi = _nrLaturi;
            dimensiuniLaturi = new int[NrLaturi];
        }

        //	Metoda care returneaza informatiile despre figura geometrica 
        //	sub forma unui sir de caractere
        public string Info()
        {
            return $"Denumire: {Denumire}, NrLaturi: {NrLaturi}";
        }

    }
}
