namespace SistemaEleicao.ConsoleApp;

public class Eleitor
{
    public string Nome { get; }
    public string TituloEleitor { get; }
    public bool JaVotou { get; private set; }

    public Eleitor(string nome, string tituloEleitor)
    {
        Nome = nome;
        TituloEleitor = tituloEleitor;
        JaVotou = false;
    }

    public void RegistrarVoto()
    {
        JaVotou = true;
    }
}
