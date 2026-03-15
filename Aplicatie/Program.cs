using LibrarieModele;
using System;
using System.Collections.Generic;

namespace InterfataUtilizator
{
    class Program
    {
        static void Main()
        {
            // Salvarea datelor într-un vector de obiecte 
            List<Intrebare> test = new List<Intrebare>();
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU ---");
                Console.WriteLine("C. Citire date de la tastatura");
                Console.WriteLine("A. Afisare date din vector");
                Console.WriteLine("S. Cautare dupa ID");
                Console.WriteLine("X. Iesire");
                Console.Write("Alege optiunea: ");
                optiune = Console.ReadLine().ToUpper();

                switch (optiune)
                {
                    case "C":
                        // 1. Citirea datelor de la tastatură 
                        Console.Write("Introduceti textul intrebarii: ");
                        string text = Console.ReadLine();
                        Console.Write("Introduceti raspunsul corect: ");
                        string rasp = Console.ReadLine();
                        Console.Write("Introduceti punctajul: ");
                        int pct = int.Parse(Console.ReadLine());

                        Intrebare noua = new Intrebare(text, rasp, pct);
                        noua.Id = test.Count + 1;

                        // 2. Salvarea datelor în vectorul de obiecte
                        test.Add(noua);
                        break;

                    case "A":
                        // 3. Afișarea datelor dintr-un vector de obiecte
                        Console.WriteLine("\nIntrebarile din colectie:");
                        foreach (var intrebare in test)
                        {
                            Console.WriteLine(intrebare.Info());
                        }
                        break;

                    case "S":
                        // 4. Căutarea după anumite criterii (ID) 
                        Console.Write("Introduceti ID-ul cautat: ");
                        int idCautat = int.Parse(Console.ReadLine());
                        Intrebare gasita = null;

                        foreach (var i in test)
                        {
                            if (i.Id == idCautat)
                                gasita = i;
                        }

                        if (gasita != null)
                            Console.WriteLine("Gasit: " + gasita.Info());
                        else
                            Console.WriteLine("Intrebarea nu exista.");
                        break;
                }
            } while (optiune != "X");
        }
    }
}