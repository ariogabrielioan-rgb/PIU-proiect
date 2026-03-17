using LibrarieModele;
using NivelAccesDate;
using System;

namespace InterfataUtilizator
{
    class Program
    {
        static void Main()
        {
            // Instantiem managerul de date (strat separat)
            AdministrareIntrebari adminIntrebari = new AdministrareIntrebari();

            // Instantiem configuratia examenului
            Examen examenCurent = new Examen();
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU SISTEM EXAMEN ---");
                Console.WriteLine($"[Setari active examen: {examenCurent.OptiuniActive}]");
                Console.WriteLine("C. Adauga intrebare (Citire)");
                Console.WriteLine("A. Vezi toate intrebarile (Afisare)");
                Console.WriteLine("S. Cauta intrebare dupa ID (Cautare)");
                Console.WriteLine("T. INCEPE EXAMENUL");
                Console.WriteLine("X. Iesire");
                Console.Write("Alege optiunea: ");
                optiune = Console.ReadLine().ToUpper();

                switch (optiune)
                {
                    case "C":
                        Console.Write("Text intrebare: ");
                        string text = Console.ReadLine();

                        Console.Write("Raspuns corect: ");
                        string rasp = Console.ReadLine();

                        Console.Write("Punctaj: ");
                        int pct = int.Parse(Console.ReadLine());

                        // Citire enum
                        Console.Write("Dificultate (0-Usor, 1-Mediu, 2-Greu): ");
                        int difIndex = int.Parse(Console.ReadLine());
                        Dificultate dificultate = (Dificultate)difIndex;

                        Intrebare noua = new Intrebare(text, rasp, pct, dificultate);
                        adminIntrebari.AdaugaIntrebare(noua);
                        Console.WriteLine("Intrebare salvata cu succes!");
                        break;

                    case "A":
                        foreach (var i in adminIntrebari.GetIntrebari())
                            Console.WriteLine(i.Info());
                        break;

                    case "S":
                        Console.Write("ID cautat: ");
                        int idCautat = int.Parse(Console.ReadLine());
                        var intrebareGasita = adminIntrebari.CautaDupaId(idCautat);

                        if (intrebareGasita != null)
                            Console.WriteLine("Gasit: " + intrebareGasita.Info());
                        else
                            Console.WriteLine("Intrebarea nu a fost gasita.");
                        break;

                    case "T":
                        int punctajTotal = 0;
                        Console.WriteLine("\n--- EXAMENUL A INCEPUT ---");

                        // Exemplu de utilizare a Flags
                        if (examenCurent.OptiuniActive.HasFlag(SetariExamen.TimpLimitat))
                        {
                            Console.WriteLine("Atentie: Timpul este limitat!");
                        }

                        foreach (var intrebareCurenta in adminIntrebari.GetIntrebari())
                        {
                            Console.WriteLine(intrebareCurenta.Info());
                            Console.Write("Raspunsul tau: ");
                            string raspunsDat = Console.ReadLine();

                            if (intrebareCurenta.Verifica(raspunsDat))
                            {
                                Console.WriteLine("Corect!");
                                punctajTotal += intrebareCurenta.GetPunctaj();
                            }
                            else
                            {
                                Console.WriteLine("Gresit.");
                            }
                        }
                        Console.WriteLine($"\nREZULTAT FINAL: {punctajTotal} puncte.");
                        break;
                }
            } while (optiune != "X");
        }
    }
}