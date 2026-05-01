using System;

namespace LibrarieEnumuri
{
    // Definim dificultatea întrebărilor
    public enum Dificultate
    {
        Usor = 0,
        Mediu = 1,
        Greu = 2,
    }

    // Cerința: Enums pentru setări examen (folosim [Flags] pentru a permite combinarea lor)
    [Flags]
    public enum SetariExamen
    {
        FaraSetari = 0,
        TimpLimitat = 1,
        RandomizeIntrebari = 2, // <--- Verifică să fie exact numele acesta!
        PunctajDiferit = 4

    }
}