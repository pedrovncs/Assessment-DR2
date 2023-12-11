using System;

internal class Program
{
    private static Gerenciador manager;

    private static void Main(string[] args)
    {
        var stopKey = "0";
        var selectedMenu = "";
        manager = new Gerenciador();

        while (stopKey != selectedMenu)
        {
            Console.WriteLine("1 - Cadastrar Artista");
            Console.WriteLine("2 - Cadastrar Música");
            Console.WriteLine("3 - Atualizar Artista");
            Console.WriteLine("4 - Exibir Artista");
            Console.WriteLine("5 - Remover Música");
            Console.WriteLine("6 - Remover Artista");
            Console.WriteLine("7 - Exibir todos os Artistas");
            Console.WriteLine("8 - Exibir todas as Músicas");
            Console.WriteLine("0 - Sair");

            selectedMenu = Console.ReadLine();

            ExercutarOpcao(selectedMenu);
        }
    }

    private static void ExercutarOpcao(string? selectedMenu)
    {
        switch (selectedMenu)
        {
            case "1":
                CadastrarArtista();
                break;
            case "2":
                CadastrarMusica();
                break;
            case "3":
                AtualizarArtista();
                break;
            case "4":
                ExibirMusicasArtista();
                break;
            case "5":
                RemoverMusica();
                break;
            case "6":
                RemoverArtista();
                break;
            case "7":
                manager.ExibirTodosArtistas();
                break;
            case "8":
                manager.ExibirTodasMusicas();
                break;
            case "0":
                manager.SalvarListas();
                Console.WriteLine("Salvando e saindo...");
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }
    }

    private static void CadastrarMusica()
    {
        Musica musica = new Musica();

        Console.WriteLine("Digite o nome da música:");
        musica.Nome = Console.ReadLine();

        Console.WriteLine("Digite o nome do artista:");
        musica.Artista = manager.BuscarArtistaPorNome(Console.ReadLine());

        manager.AdicionarMusica(musica);
    }

    private static void CadastrarArtista()
    {
        Artista artista = new Artista();

        Console.WriteLine("Digite o nome do artista:");
        artista.Nome = Console.ReadLine();

        Console.WriteLine("Digite o gênero do artista:");
        artista.Genero = Console.ReadLine();

        Console.WriteLine("Digite o país do artista:");
        artista.Local = Console.ReadLine();

        manager.AdicionarArtista(artista);
    }

    private static void AtualizarArtista()
    {
        Console.WriteLine("Digite o nome do artista que deseja atualizar:");
        string nomeArtista = Console.ReadLine();
        Artista artistaExistente = manager.BuscarArtistaPorNome(nomeArtista);

        if (artistaExistente == null)
        {
            Console.WriteLine("Artista não encontrado.");
            return;
        }

        Console.WriteLine("Digite o novo nome do artista:");
        string novoNomeArtista = Console.ReadLine();

        artistaExistente.Nome = novoNomeArtista;
        manager.SalvarListas();
    }

    private static void ExibirMusicasArtista()
    {
        Console.WriteLine("Digite o nome do artista:");
        string nomeArtista = Console.ReadLine();
        Artista artista = manager.BuscarArtistaPorNome(nomeArtista);

        if (artista == null)
        {
            Console.WriteLine("Artista não encontrado.");
            return;
        }

        Console.WriteLine($"\n Nome do Artista: {artista.Nome}");
        Console.WriteLine($"Gênero: {artista.Genero}");
        Console.WriteLine($"País: {artista.Local} \n");

        Console.WriteLine("Músicas:");
        foreach (var musica in artista.Musicas)
        {
            Console.WriteLine($"- {musica.Nome}");
        }
        Console.WriteLine("\n");
    }

    private static void RemoverMusica()
    {
        Console.WriteLine("Digite o nome da música:");
        string nomeMusica = Console.ReadLine();

        manager.RemoverMusica(nomeMusica);
    }

    private static void RemoverArtista()
    {
        Console.WriteLine("Digite o nome do artista:");
        string nomeArtista = Console.ReadLine();

        manager.RemoverArtista(nomeArtista);
    }
}


