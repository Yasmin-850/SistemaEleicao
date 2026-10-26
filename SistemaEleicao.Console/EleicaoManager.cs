namespace SistemaEleicao.ConsoleApp;

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();

    public IReadOnlyList<Candidato> Candidatos => _candidatos;

    public bool CadastrarCandidato(string nome, int numero)
    {
        if (string.IsNullOrWhiteSpace(nome) || numero <= 0)
        {
            return false;
        }

        bool numeroExiste = _candidatos.Any(c => c.Numero == numero);

        if (numeroExiste)
        {
            return false;
        }

        _candidatos.Add(new Candidato(nome, numero));
        return true;
    }
}
