namespace SistemaEleicao.ConsoleApp;

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();
    private readonly List<Eleitor> _eleitores = new();

    public IReadOnlyList<Candidato> Candidatos => _candidatos;
    public IReadOnlyList<Eleitor> Eleitores => _eleitores;

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
}
