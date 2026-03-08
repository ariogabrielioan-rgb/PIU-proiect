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
        public int Perimetru
        {
            get
            {
                int suma = 0;
                if (dimensiuniLaturi != null)
                {
                    foreach (int latura in dimensiuniLaturi)
                    {
                        suma = suma + latura;
                    }
                }
                return suma;
            }
        }
        public string TipFigura
        {
            get
            {
                switch (NrLaturi)
                {
                    case 0: return "Punct";
                    case 1: return "Linie";
                    case 2: return "Unghi";
                    case 3: return "Triunghi";
                    case 4: return "Patrulater";
                    case 5: return "Pentagon";
                    case 6: return "Hexagon";
                    default: return "Poligon complex";
                }
            }
        }
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
            if (string.IsNullOrEmpty(Denumire))
            {
                return "FIGURA NESETATA";
            }
            string laturiAfisate = dimensiuniLaturi != null ? string.Join(" ", dimensiuniLaturi) : "";
            return $"Denumire: {Denumire}, NrLaturi: {NrLaturi}, Laturi: {laturiAfisate}";
        }

    }
}
