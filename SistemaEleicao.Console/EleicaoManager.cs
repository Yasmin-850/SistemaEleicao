
namespace SistemaEleicao.ConsoleApp;

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();
    private readonly List<Eleitor> _eleitores = new();
    private readonly List<Voto> _votos = new();

    public IReadOnlyList<Candidato> Candidatos => _candidatos;
    public IReadOnlyList<Eleitor> Eleitores => _eleitores;
    public IReadOnlyList<Voto> Votos => _votos;

    // Cadastrar candidato
    public bool CadastrarCandidato(string nome, int numero)
    {
        if (string.IsNullOrWhiteSpace(nome) || numero <= 0)
            return false;

        if (_candidatos.Any(c => c.Numero == numero))
            return false;

        _candidatos.Add(new Candidato(nome, numero));
        return true;
    }

    // Cadastrar eleitor
    public bool CadastrarEleitor(string nome, string tituloEleitor)
    {
        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(tituloEleitor))
            return false;

        if (_eleitores.Any(e => e.TituloEleitor == tituloEleitor))
            return false;

        _eleitores.Add(new Eleitor(nome, tituloEleitor));
        return true;
    }

    // Registrar voto
    public bool Votar(
        string tituloEleitor,
        int? numeroCandidato,
        bool nulo = false)
    {
        var eleitor = _eleitores.FirstOrDefault(
            e => e.TituloEleitor == tituloEleitor);

        // Verifica se o eleitor existe e ainda não votou
        if (eleitor == null || eleitor.JaVotou)
            return false;

        // Um voto nulo não pode ter número de candidato
        if (nulo && numeroCandidato.HasValue)
            return false;

        // Verifica se o candidato existe
        if (!nulo && numeroCandidato.HasValue &&
    !_candidatos.Any(c => c.Numero == numeroCandidato.Value))
    {
         numeroCandidato = null;
         nulo = true;
    }

        _votos.Add(new Voto(tituloEleitor, numeroCandidato, nulo));
        eleitor.RegistrarVoto();

        return true;
        }

    // Apurar os resultados da eleição
    public ResultadoEleicao ApurarVotos()
        {
        var votosPorCandidato = new Dictionary<string, int>();

        // Conta os votos recebidos por cada candidato
        foreach (var candidato in _candidatos)
        {
            int quantidade = _votos.Count(v =>
                v.NumeroCandidato == candidato.Numero && !v.Nulo);

            string identificacao =
                $"{candidato.Nome} ({candidato.Numero})";

            votosPorCandidato[identificacao] = quantidade;
        }

        // Conta os votos em branco
        int votosEmBranco = _votos.Count(v =>
            !v.Nulo && !v.NumeroCandidato.HasValue);

        // Conta os votos nulos
        int votosNulos = _votos.Count(v => v.Nulo);

        // Descobre a maior quantidade de votos
        int maiorQuantidade = votosPorCandidato.Count > 0
            ? votosPorCandidato.Values.Max()
            : 0;

        var maisVotados = votosPorCandidato
            .Where(v => v.Value == maiorQuantidade)
            .ToList();

        string? vencedor = null;
        bool houveEmpate = false;

        // Só existe vencedor se alguém recebeu pelo menos um voto
        if (maiorQuantidade > 0)
        {
            houveEmpate = maisVotados.Count > 1;

            if (!houveEmpate)
                vencedor = maisVotados[0].Key;
        }

        return new ResultadoEleicao(
            votosPorCandidato,
            votosEmBranco,
            votosNulos,
            vencedor,
            houveEmpate);
    }
}
