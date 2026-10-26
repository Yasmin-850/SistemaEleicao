namespace SistemaEleicao.ConsoleApp;

public class ResultadoEleicao
{
    public Dictionary<string, int> VotosPorCandidato { get; }
    public int VotosEmBranco { get; }
    public int VotosNulos { get; }
    public string? Vencedor { get; }
    public bool HouveEmpate { get; }

    public ResultadoEleicao(
        Dictionary<string, int> votosPorCandidato,
        int votosEmBranco,
        int votosNulos,
        string? vencedor,
        bool houveEmpate)
    {
        VotosPorCandidato = votosPorCandidato;
        VotosEmBranco = votosEmBranco;
        VotosNulos = votosNulos;
        Vencedor = vencedor;
        HouveEmpate = houveEmpate;
    }
}
