using LibrarieEnumuri;
using LibrarieModele;
using Stocare;           // Aici avem acum IStocareDate
using System;

namespace Aplicatie
{
    class Program
    {
        static void Main()
        {
            // MODIFICARE: Nu mai instanțiem direct "AdministrareIntrebariFisierText"
            // Cerem Factory-ului să ne dea "ceva" care respectă contractul IStocareDate
            IStocareData adminIntrebari = StocareFactory.GetAdministratorStocare();

            Examen examenCurent = new Examen();
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU SISTEM EXAMEN (Structura Laborator) ---");
                Console.WriteLine($"[Setari active examen: {examenCurent.OptiuniActive}]");

                Console.WriteLine("C. Adauga intrebare (Citire)");
                Console.WriteLine("A. Vezi toate intrebarile (Afisare)");
                Console.WriteLine("S. Cauta intrebare dupa ID (Cautare)");
                Console.WriteLine("T. INCEPE EXAMENUL");
                Console.WriteLine("X. Iesire");
                Console.Write("Alege optiunea: ");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        Console.Write("Text intrebare: ");
                        string text = Console.ReadLine();

                        Console.Write("Raspuns corect: ");
                        string rasp = Console.ReadLine();

                        Console.Write("Punctaj: ");
                        int pct = int.Parse(Console.ReadLine() ?? "0");

                        Console.Write("Dificultate (0-Usor, 1-Mediu, 2-Greu): ");
                        int difIndex = int.Parse(Console.ReadLine() ?? "0");
                        Dificultate dificultate = (Dificultate)difIndex;

                        Intrebare noua = new Intrebare(text, rasp, pct, dificultate);

                        // Metoda ramane la fel, dar "adminIntrebari" e acum de tip interfata
                        adminIntrebari.AdaugaIntrebare(noua);

                        Console.WriteLine("Intrebare salvata cu succes!");
                        break;

                    case "A":
                        var listaIntrebari = adminIntrebari.GetIntrebari();
                        Console.WriteLine("\n--- LISTA INTREBARI ---");
                        foreach (var i in listaIntrebari)
                        {
                            Console.WriteLine(i.Info());
                        }
                        break;

                    case "S":
                        Console.Write("ID cautat: ");
                        int idCautat = int.Parse(Console.ReadLine() ?? "0");
                        var intrebareGasita = adminIntrebari.CautaDupaId(idCautat);

                        if (intrebareGasita != null)
                            Console.WriteLine("Gasit: " + intrebareGasita.Info());
                        else
                            Console.WriteLine("Intrebarea nu a fost gasita.");
                        break;

                    case "T":
                        int punctajTotal = 0;
                        Console.WriteLine("\n--- EXAMENUL A INCEPUT ---");

                        if (examenCurent.OptiuniActive.HasFlag(SetariExamen.TimpLimitat))
                        {
                            Console.WriteLine("!! ATENTIE: Timpul este limitat conform setarilor !!");
                        }

                        foreach (var intrebareCurenta in adminIntrebari.GetIntrebari())
                        {
                            Console.WriteLine("\n" + intrebareCurenta.Info());
                            Console.Write("Raspunsul tau: ");
                            string raspunsDat = Console.ReadLine();

                            if (intrebareCurenta.Verifica(raspunsDat))
                            {
                                Console.WriteLine("Corect!");
                                punctajTotal += intrebareCurenta.Punctaj;
                            }
                            else
                            {
                                Console.WriteLine($"Gresit! Raspunsul corect era: {intrebareCurenta.RaspunsCorect}");
                            }
                        }
                        Console.WriteLine($"\n--- REZULTAT FINAL: {punctajTotal} puncte ---");
                        break;
                }
            } while (optiune != "X");
        }
    }
}