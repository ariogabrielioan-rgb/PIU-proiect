namespace LibrarieModele;

public class Intrebare
{
    private string textIntrebare;
    private string raspunsCorect;
    private int punctaj;

    public int Id { get; set; }
    public bool EsteImportanta => punctaj > 5;

    public Intrebare()
    {
        textIntrebare = string.Empty;
        raspunsCorect = string.Empty;
        punctaj = 0;
    }

    public Intrebare(string _text, string _raspuns, int _punctaj)
    {
        textIntrebare = _text;
        raspunsCorect = _raspuns;
        punctaj = _punctaj;
    }

    public string Info()
    {
        if (string.IsNullOrEmpty(textIntrebare))
            return "INTREBARE INCOMPLETA";

        return $"Intrebarea {Id}: {textIntrebare} ({punctaj} puncte)";
    }

    public bool Verifica(string raspunsUtilizator)
    {
        return raspunsCorect.ToLower() == raspunsUtilizator.ToLower();
    }

    public int GetPunctaj()
    {
        return punctaj;
    }
}