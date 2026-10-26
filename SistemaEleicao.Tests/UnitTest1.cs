
using SistemaEleicao.ConsoleApp;

namespace SistemaEleicao.Tests;

public class EleicaoManagerTests
{
    // TESTES DE CADASTRO DE CANDIDATOS

    [Fact]
    public void CadastrarCandidato_ComNumeroNovo_DeveRetornarVerdadeiro()
    {
        var eleicao = new EleicaoManager();

        Assert.True(eleicao.CadastrarCandidato("Ana", 10));
        Assert.Single(eleicao.Candidatos);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);

        Assert.False(eleicao.CadastrarCandidato("Bruno", 10));
        Assert.Single(eleicao.Candidatos);
    }

    // TESTES DE CADASTRO DE ELEITORES

    [Fact]
    public void CadastrarEleitor_ComTituloNovo_DeveRetornarVerdadeiro()
    {
        var eleicao = new EleicaoManager();

        Assert.True(eleicao.CadastrarEleitor("Maria", "123456"));
        Assert.Single(eleicao.Eleitores);
    }

    [Fact]
    public void CadastrarEleitor_ComTituloDuplicado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");

        Assert.False(eleicao.CadastrarEleitor("Joao", "123456"));
        Assert.Single(eleicao.Eleitores);
    }

    // TESTES DE VOTAÇÃO

    [Fact]
    public void Votar_ComCandidatoValido_DeveRegistrarVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.CadastrarEleitor("Maria", "123456");

        Assert.True(eleicao.Votar("123456", 10));
        Assert.Single(eleicao.Votos);
    }

    [Fact]
    public void Votar_DuasVezesPeloMesmoEleitor_DeveRejeitarSegundoVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.CadastrarEleitor("Maria", "123456");

        Assert.True(eleicao.Votar("123456", 10));
        Assert.False(eleicao.Votar("123456", 10));
        Assert.Single(eleicao.Votos);
    }

    [Fact]
    public void Votar_ComEleitorNaoCadastrado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);

        Assert.False(eleicao.Votar("999999", 10));
        Assert.Empty(eleicao.Votos);
    }

    [Fact]
    public void Votar_EmBranco_DeveRegistrarVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");

        Assert.True(eleicao.Votar("123456", null));
        Assert.Null(eleicao.Votos[0].NumeroCandidato);
        Assert.False(eleicao.Votos[0].Nulo);
    }

    [Fact]
    public void Votar_Nulo_DeveRegistrarVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");

        Assert.True(eleicao.Votar("123456", null, true));
        Assert.True(eleicao.Votos[0].Nulo);
    }

    [Fact]
    public void Votar_ComNumeroDeCandidatoInexistente_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");

        Assert.False(eleicao.Votar("123456", 99));
        Assert.Empty(eleicao.Votos);
    }

    // TESTES DE APURAÇÃO DOS RESULTADOS

    [Fact]
    public void ApurarVotos_DeveContarVotosPorCandidato()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.CadastrarCandidato("Bruno", 20);

        eleicao.CadastrarEleitor("Maria", "111");
        eleicao.CadastrarEleitor("Joao", "222");
        eleicao.CadastrarEleitor("Pedro", "333");

        eleicao.Votar("111", 10);
        eleicao.Votar("222", 10);
        eleicao.Votar("333", 20);

        var resultado = eleicao.ApurarVotos();

        Assert.Equal(2, resultado.VotosPorCandidato["Ana (10)"]);
        Assert.Equal(1, resultado.VotosPorCandidato["Bruno (20)"]);
        Assert.Equal("Ana (10)", resultado.Vencedor);
        Assert.False(resultado.HouveEmpate);
    }

    [Fact]
    public void ApurarVotos_DeveContarVotosEmBrancoENulos()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "111");
        eleicao.CadastrarEleitor("Joao", "222");

        eleicao.Votar("111", null);
        eleicao.Votar("222", null, true);

        var resultado = eleicao.ApurarVotos();

        Assert.Equal(1, resultado.VotosEmBranco);
        Assert.Equal(1, resultado.VotosNulos);
    }

    [Fact]
    public void ApurarVotos_ComMesmoNumeroDeVotos_DeveIdentificarEmpate()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.CadastrarCandidato("Bruno", 20);

        eleicao.CadastrarEleitor("Maria", "111");
        eleicao.CadastrarEleitor("Joao", "222");

        eleicao.Votar("111", 10);
        eleicao.Votar("222", 20);

        var resultado = eleicao.ApurarVotos();

        Assert.True(resultado.HouveEmpate);
        Assert.Null(resultado.Vencedor);
    }
}
