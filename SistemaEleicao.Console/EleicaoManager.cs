namespace SistemaEleicao.ConsoleApp;

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();
    private readonly List<Eleitor> _eleitores = new();
    private readonly List<Voto> _votos = new();

    public IReadOnlyList<Candidato> Candidatos => _candidatos;
    public IReadOnlyList<Eleitor> Eleitores => _eleitores;
    public IReadOnlyList<Voto> Votos => _votos;

    public bool CadastrarCandidato(string nome, int numero)
    {
        if (string.IsNullOrWhiteSpace(nome) || numero <= 0)
            return false;

        if (_candidatos.Any(c => c.Numero == numero))
            return false;

        _candidatos.Add(new Candidato(nome, numero));
        return true;
    }

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

    public bool Votar(string tituloEleitor, int? numeroCandidato, bool nulo = false)
    {
        var eleitor = _eleitores.FirstOrDefault(
            e => e.TituloEleitor == tituloEleitor);

        if (eleitor == null || eleitor.JaVotou)
            return false;

        if (nulo && numeroCandidato.HasValue)
            return false;

        if (!nulo && numeroCandidato.HasValue &&
            !_candidatos.Any(c => c.Numero == numeroCandidato.Value))
            return false;

        _votos.Add(new Voto(tituloEleitor, numeroCandidato, nulo));
        eleitor.RegistrarVoto();

        return true;
    }
}
