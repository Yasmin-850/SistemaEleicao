
using SistemaEleicao.ConsoleApp;

var eleicao = new EleicaoManager();
bool executando = true;

while (executando)
{
    Console.Clear();
    Console.WriteLine("==================================");
    Console.WriteLine("       SISTEMA DE ELEIÇÃO");
    Console.WriteLine("==================================");
    Console.WriteLine("1 - Cadastrar candidato");
    Console.WriteLine("2 - Cadastrar eleitor");
    Console.WriteLine("3 - Votar");
    Console.WriteLine("4 - Apurar resultados");
    Console.WriteLine("5 - Listar candidatos");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("==================================");
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Nome do candidato: ");
            string nomeCandidato = Console.ReadLine() ?? "";

            Console.Write("Número do candidato: ");
            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("Número inválido.");
                Pausar();
                break;
            }

            if (eleicao.CadastrarCandidato(nomeCandidato, numero))
                Console.WriteLine("Candidato cadastrado com sucesso!");
            else
                Console.WriteLine("Não foi possível cadastrar. Verifique o nome e o número.");

            Pausar();
            break;

        case "2":
            Console.Write("Nome do eleitor: ");
            string nomeEleitor = Console.ReadLine() ?? "";

            Console.Write("Título de eleitor simulado: ");
            string titulo = Console.ReadLine() ?? "";

            if (eleicao.CadastrarEleitor(nomeEleitor, titulo))
                Console.WriteLine("Eleitor cadastrado com sucesso!");
            else
                Console.WriteLine("Não foi possível cadastrar. Verifique os dados.");

            Pausar();
            break;

        case "3":
            Console.Write("Título do eleitor: ");
            string tituloVoto = Console.ReadLine() ?? "";

            Console.WriteLine("Digite o número do candidato.");
            Console.WriteLine("Deixe vazio para votar em branco.");
            Console.WriteLine("Digite N para votar nulo.");
            Console.Write("Seu voto: ");

            string voto = Console.ReadLine() ?? "";

            bool registrado;

            if (voto.Trim().Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                registrado = eleicao.Votar(tituloVoto, null, true);
            }
            else if (string.IsNullOrWhiteSpace(voto))
            {
                registrado = eleicao.Votar(tituloVoto, null);
            }
            else if (int.TryParse(voto, out int numeroVoto))
            {
                registrado = eleicao.Votar(tituloVoto, numeroVoto);
            }
            else
            {
                registrado = false;
            }

            Console.WriteLine(registrado
                ? "Voto registrado com sucesso!"
                : "Voto não registrado. Verifique o eleitor e o voto.");

            Pausar();
            break;

        case "4":
            var resultado = eleicao.ApurarVotos();

            Console.WriteLine("\n===== RESULTADO DA ELEIÇÃO =====");

            foreach (var candidato in resultado.VotosPorCandidato)
            {
                Console.WriteLine(
                    $"{candidato.Key}: {candidato.Value} voto(s)");
            }

            Console.WriteLine($"Votos em branco: {resultado.VotosEmBranco}");
            Console.WriteLine($"Votos nulos: {resultado.VotosNulos}");

            if (resultado.HouveEmpate)
                Console.WriteLine("Resultado: houve empate entre os candidatos mais votados.");
            else if (resultado.Vencedor != null)
                Console.WriteLine($"Vencedor: {resultado.Vencedor}");
            else
                Console.WriteLine("Ainda não há vencedor.");

            Pausar();
            break;

        case "5":
            Console.WriteLine("\n===== CANDIDATOS CADASTRADOS =====");

            if (eleicao.Candidatos.Count == 0)
            {
                Console.WriteLine("Nenhum candidato cadastrado.");
            }
            else
            {
                foreach (var candidato in eleicao.Candidatos)
                {
                    Console.WriteLine($"{candidato.Numero} - {candidato.Nome}");
                }
            }

            Pausar();
            break;

        case "0":
            executando = false;
            Console.WriteLine("Sistema encerrado.");
            break;

        default:
            Console.WriteLine("Opção inválida.");
            Pausar();
            break;
    }
}

static void Pausar()
{
    Console.WriteLine("\nPressione Enter para continuar...");
    Console.ReadLine();
}
