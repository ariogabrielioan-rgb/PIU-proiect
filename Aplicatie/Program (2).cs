namespace ExempluClase
{
    class Program
    {
        static void Main()
        {
            //	Instantierea unui obiect de tip FiguraGeometrica utilizand constructorul fara parametri
            //	Tipul variabilei f este 'var' (determinat de compilator)
            var figur1 = new FiguraGeometrica();
            string figura1AsString = figur1.Info();
            Console.WriteLine(figura1AsString);
            Console.WriteLine($"Este poligon? {figur1.EstePoligon}");

            //	Instantierea unui obiect de tip FiguraGeometrica utilizand constructorul cu parametri
            //	Tipul variabilei f1 este explicit 'FiguraGeometrica'
            FiguraGeometrica figura2 = new FiguraGeometrica("patrat", 4);
            string figura2AsString = figura2.Info();
            Console.WriteLine(figura2AsString);
            Console.WriteLine($"Este poligon? {figura2.EstePoligon}");

            Console.ReadKey();
        }
    }

}