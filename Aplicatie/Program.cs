using LibrarieModele;
using System;
using System.Collections.Generic;

namespace InterfataUtilizator
{
    class Program
    {
        static void Main()
        {
            List<Intrebare> listaIntrebari = new List<Intrebare>();
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU SISTEM EXAMEN ---");
                Console.WriteLine("C. Adauga intrebare (Citire)");
                Console.WriteLine("A. Vezi toate intrebarile (Afisare)");
                Console.WriteLine("S. Cauta intrebare dupa ID (Cautare)");
                Console.WriteLine("T. INCEPE EXAMENUL (Functionalitatea originala)");
                Console.WriteLine("X. Iesire");
                Console.Write("Alege optiunea: ");
                optiune = Console.ReadLine().ToUpper();

                switch (optiune)
                {
                    case "C":
                        // Citirea datelor de la tastatura 
                        Console.Write("Text intrebare: ");
                        string text = Console.ReadLine();
                        Console.Write("Raspuns corect: ");
                        string rasp = Console.ReadLine();
                        Console.Write("Punctaj: ");
                        int pct = int.Parse(Console.ReadLine());

                        Intrebare noua = new Intrebare(text, rasp, pct);
                        noua.Id = listaIntrebari.Count + 1;
                        listaIntrebari.Add(noua); // Salvarea in vector
                        break;

                    case "A":
                        // Afisarea datelor 
                        foreach (var i in listaIntrebari)
                            Console.WriteLine(i.Info());
                        break;

                    case "S":
                        // Cautarea dupa ID 
                        Console.Write("ID cautat: ");
                        int idCautat = int.Parse(Console.ReadLine());
                        foreach (var i in listaIntrebari)
                            if (i.Id == idCautat) Console.WriteLine("Gasit: " + i.Info());
                        break;

                    case "T":
                        // Logica ta originala de verificare si punctaj
                        int punctajTotal = 0;
                        Console.WriteLine("\n--- EXAMENUL A INCEPUT ---");
                        foreach (var intrebareCurenta in listaIntrebari)
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