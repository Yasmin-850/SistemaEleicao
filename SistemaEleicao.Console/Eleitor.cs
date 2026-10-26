namespace SistemaEleicao.ConsoleApp;

public class Eleitor
{
    public string Nome { get; }
    public string TituloEleitor { get; }

    public Eleitor(string nome, string tituloEleitor)
    {
        Nome = nome;
        TituloEleitor = tituloEleitor;
    }
}
