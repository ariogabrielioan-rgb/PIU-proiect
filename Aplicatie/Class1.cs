using LibrarieModele; // Folosești numele de mai sus
using System;
using System.Collections.Generic;
namespace InterfataUtilizator
{
    class Program
    {
        static void Main()
        {
            // 1. Crearea setului de intrebari 
            Intrebare q1 = new Intrebare("Care este capitala Romaniei?", "Bucuresti", 5);
            q1.Id = 1;

            Intrebare q2 = new Intrebare("Cat face 5 inmultit cu 5?", "25", 2);
            q2.Id = 2;

            Intrebare q3 = new Intrebare("In ce an suntem acum?", "2026", 3);
            q3.Id = 3;
            Intrebare q4 = new Intrebare("In ce an suntem acum?", "2025", 3);
            q3.Id = 4;
            // Punem intrebarile într-o lista pentru a le parcurge usor
            List<Intrebare> test = new List<Intrebare> { q1, q2, q3, q4 };

            int punctajTotal = 0;

            Console.WriteLine("--- INCEPE EXAMENUL ---\n");

            // 2. Parcurgerea testului (Structura repetitivă)
            foreach (var intrebareCurenta in test)
            {
                Console.WriteLine(intrebareCurenta.Info());
                Console.Write("Raspunsul tau: ");
                string raspunsDat = Console.ReadLine();

                // Verificam daca utilizatorul a ghicit
                if (intrebareCurenta.Verifica(raspunsDat))
                {
                    Console.WriteLine("Corect!");
                    punctajTotal = punctajTotal + intrebareCurenta.GetPunctaj();
                }
                else
                {
                    Console.WriteLine("Gresit.");
                }
                Console.WriteLine(); // Linie goala pentru aspect
            }

            // 3. Afișarea rezultatului final
            Console.WriteLine("--- REZULTAT FINAL ---");
            Console.WriteLine($"Ai obtinut in total: {punctajTotal} puncte.");

            if (punctajTotal >= 5)
                Console.WriteLine("Status: ADMIS");
            else
                Console.WriteLine("Status: RESPINS");

            Console.ReadKey();
        }
    }
}