using SistemaEleicao.ConsoleApp;

namespace SistemaEleicao.Tests;

public class EleicaoManagerTests
{
    [Fact]
    public void CadastrarCandidato_ComNumeroNovo_DeveRetornarVerdadeiro()
    {
        var eleicao = new EleicaoManager();

        bool resultado = eleicao.CadastrarCandidato("Ana", 10);

        Assert.True(resultado);
        Assert.Single(eleicao.Candidatos);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveRetornarFalso()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);
        bool resultado = eleicao.CadastrarCandidato("Bruno", 10);

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
        bool resultado = eleicao.CadastrarEleitor("Joao", "123456");

        Assert.False(resultado);
        Assert.Single(eleicao.Eleitores);
    }
}
