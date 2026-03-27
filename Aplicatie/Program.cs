using LibrarieEnumuri;   // Referință către proiectul cu Enum-uri
using LibrarieModele;    // Referință către proiectul cu clasele Intrebare și Examen
using Stocare;           // Referință către proiectul care se ocupă de fișierul text
using System;

namespace Aplicatie
{
    class Program
    {
        static void Main()
        {
            // 1. Inițializăm stocarea (din proiectul Stocare)
            // Fișierul "intrebari.txt" va fi creat în bin/Debug/...
            AdministrareIntrebari adminIntrebari = new AdministrareIntrebari("intrebari.txt");

            // 2. Instanțiem configurația examenului (din proiectul LibrarieModele)
            Examen examenCurent = new Examen();
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU SISTEM EXAMEN ---");
                // Afișăm setările folosind Flags din LibrarieEnumuri
                Console.WriteLine($"[Setari active examen: {examenCurent.OptiuniActive}]");

                Console.WriteLine("C. Adauga intrebare (Citire)");
                Console.WriteLine("A. Vezi toate intrebarile (Afisare din FISIER)");
                Console.WriteLine("S. Cauta intrebare dupa ID (Cautare in FISIER)");
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

                        // Citire enum (din LibrarieEnumuri)
                        Console.Write("Dificultate (0-Usor, 1-Mediu, 2-Greu): ");
                        int difIndex = int.Parse(Console.ReadLine() ?? "0");
                        Dificultate dificultate = (Dificultate)difIndex;

                        // Creăm obiectul și îl trimitem spre salvare în fișier
                        Intrebare noua = new Intrebare(text, rasp, pct, dificultate);
                        adminIntrebari.AdaugaIntrebare(noua);

                        Console.WriteLine("Intrebare salvata cu succes in fisier!");
                        break;

                    case "A":
                        // Preluăm lista din proiectul Stocare
                        var listaIntrebari = adminIntrebari.GetIntrebari();
                        Console.WriteLine("\n--- LISTA INTREBARI DIN FISIER ---");
                        foreach (var i in listaIntrebari)
                        {
                            Console.WriteLine(i.Info());
                        }
                        break;

                    case "S":
                        Console.Write("ID cautat: ");
                        int idCautat = int.Parse(Console.ReadLine() ?? "0");
                        // Folosim metoda de căutare din Stocare
                        var intrebareGasita = adminIntrebari.CautaDupaId(idCautat);

                        if (intrebareGasita != null)
                            Console.WriteLine("Gasit: " + intrebareGasita.Info());
                        else
                            Console.WriteLine("Intrebarea nu a fost gasita.");
                        break;

                    case "T":
                        int punctajTotal = 0;
                        Console.WriteLine("\n--- EXAMENUL A INCEPUT ---");

                        // Verificare Flags (din LibrarieEnumuri)
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