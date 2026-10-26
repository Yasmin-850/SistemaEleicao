
using SistemaEleicao.ConsoleApp;
using Xunit;

namespace SistemaEleicao.Tests;

public class UnitTest1
{
    [Fact]
    public void CadastrarCandidato_ComNumeroNovo_DeveRetornarVerdadeiro()
    {
        var eleicao = new EleicaoManager();

        bool resultado = eleicao.CadastrarCandidato("João", 10);

        Assert.True(resultado);
        Assert.Single(eleicao.Candidatos);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("João", 10);
        bool resultado = eleicao.CadastrarCandidato("Maria", 10);

        Assert.False(resultado);
        Assert.Single(eleicao.Candidatos);
    }

    [Fact]
    public void CadastrarEleitor_ComTituloNovo_DeveRetornarVerdadeiro()
    {
        var eleicao = new EleicaoManager();

        bool resultado = eleicao.CadastrarEleitor("Maria", "123456");

        Assert.True(resultado);
        Assert.Single(eleicao.Eleitores);
    }

    [Fact]
    public void CadastrarEleitor_ComTituloDuplicado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");
        bool resultado = eleicao.CadastrarEleitor("João", "123456");

        Assert.False(resultado);
        Assert.Single(eleicao.Eleitores);
    }

    [Fact]
    public void Votar_ComCandidatoValido_DeveRegistrarVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("João", 10);
        eleicao.CadastrarEleitor("Maria", "123456");

        bool resultado = eleicao.Votar("123456", 10);

        Assert.True(resultado);
        Assert.Single(eleicao.Votos);
        Assert.False(eleicao.Votos[0].Nulo);
        Assert.Equal(10, eleicao.Votos[0].NumeroCandidato);
    }

    [Fact]
    public void Votar_DuasVezesPeloMesmoEleitor_DeveRejeitarSegundoVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("João", 10);
        eleicao.CadastrarEleitor("Maria", "123456");

        bool primeiroVoto = eleicao.Votar("123456", 10);
        bool segundoVoto = eleicao.Votar("123456", 10);

        Assert.True(primeiroVoto);
        Assert.False(segundoVoto);
        Assert.Single(eleicao.Votos);
    }

    [Fact]
    public void Votar_ComEleitorNaoCadastrado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("João", 10);

        bool resultado = eleicao.Votar("999999", 10);

        Assert.False(resultado);
        Assert.Empty(eleicao.Votos);
    }

    [Fact]
    public void Votar_EmBranco_DeveRegistrarVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");

        bool resultado = eleicao.Votar("123456", null);

        Assert.True(resultado);
        Assert.Single(eleicao.Votos);
        Assert.Null(eleicao.Votos[0].NumeroCandidato);
        Assert.False(eleicao.Votos[0].Nulo);
    }

    [Fact]
    public void Votar_Nulo_DeveRegistrarVoto()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");

        bool resultado = eleicao.Votar("123456", null, true);

        Assert.True(resultado);
        Assert.Single(eleicao.Votos);
        Assert.True(eleicao.Votos[0].Nulo);
    }

    [Fact]
    public void Votar_ComNumeroDeCandidatoInexistente_DeveRegistrarVotoNulo()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarEleitor("Maria", "123456");

        bool resultado = eleicao.Votar("123456", 99);

        Assert.True(resultado);
        Assert.Single(eleicao.Votos);
        Assert.True(eleicao.Votos[0].Nulo);
        Assert.Null(eleicao.Votos[0].NumeroCandidato);
    }

    [Fact]
    public void ApurarVotos_DeveContarVotosPorCandidato()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("João", 10);
        eleicao.CadastrarCandidato("Maria", 20);
        eleicao.CadastrarEleitor("Ana", "111111");
        eleicao.CadastrarEleitor("Pedro", "222222");

        eleicao.Votar("111111", 10);
        eleicao.Votar("222222", 10);

        ResultadoEleicao resultado = eleicao.ApurarVotos();

        Assert.Equal(2, resultado.VotosPorCandidato["João (10)"]);
        Assert.Equal(0, resultado.VotosPorCandidato["Maria (20)"]);
        Assert.Equal("João (10)", resultado.Vencedor);
        Assert.False(resultado.HouveEmpate);
    }

    [Fact]
    public void ApurarVotos_DeveContarVotosEmBrancoENulos()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("João", 10);
        eleicao.CadastrarEleitor("Ana", "111111");
        eleicao.CadastrarEleitor("Pedro", "222222");
        eleicao.CadastrarEleitor("Maria", "333333");

        eleicao.Votar("111111", null);
        eleicao.Votar("222222", null, true);
        eleicao.Votar("333333", 99);

        ResultadoEleicao resultado = eleicao.ApurarVotos();

        Assert.Equal(1, resultado.VotosEmBranco);
        Assert.Equal(2, resultado.VotosNulos);
    }

    [Fact]
    public void ApurarVotos_ComMesmoNumeroDeVotos_DeveIdentificarEmpate()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("João", 10);
        eleicao.CadastrarCandidato("Maria", 20);
        eleicao.CadastrarEleitor("Ana", "111111");
        eleicao.CadastrarEleitor("Pedro", "222222");

        eleicao.Votar("111111", 10);
        eleicao.Votar("222222", 20);

        ResultadoEleicao resultado = eleicao.ApurarVotos();

        Assert.True(resultado.HouveEmpate);
        Assert.Null(resultado.Vencedor);
    }
}
