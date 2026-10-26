namespace SistemaEleicao.ConsoleApp;

public class Voto
{
    public string TituloEleitor { get; }
    public int? NumeroCandidato { get; }
    public bool Nulo { get; }

    public Voto(string tituloEleitor, int? numeroCandidato, bool nulo = false)
    {
        TituloEleitor = tituloEleitor;
        NumeroCandidato = numeroCandidato;
        Nulo = nulo;
    }
}
