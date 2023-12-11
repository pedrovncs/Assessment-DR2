using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Gerenciador
{
    public List<Artista> Artistas { get; set; } = new List<Artista>();
    public List<Musica> Musicas { get; set; } = new List<Musica>();
    private const string ArquivoArtistas = "artistas.json";
    private const string ArquivoMusicas = "musicas.json";

    public Gerenciador()
    {
        this.IniciarListas();
    }

    public void IniciarListas()
    {
        if (!File.Exists(ArquivoArtistas))
        {
            File.Create(ArquivoArtistas).Close();
        }
        if (!File.Exists(ArquivoMusicas))
        {
            File.Create(ArquivoMusicas).Close();
        }

        using (StreamReader sR = new StreamReader(ArquivoArtistas))
        {
            try
            {
                string json = sR.ReadToEnd();
                Artistas = JsonSerializer.Deserialize<List<Artista>>(json);
                sR.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao ler o arquivo {ArquivoArtistas}: {e.Message}");
            }
        }

        using (StreamReader sR = new StreamReader(ArquivoMusicas))
        {
            try
            {
                string json = sR.ReadToEnd();
                Musicas = JsonSerializer.Deserialize<List<Musica>>(json);
                sR.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao ler o arquivo {ArquivoMusicas}: {e.Message}");
            }
        }

    }

    public void SalvarListas()
    {
        if (File.Exists(ArquivoArtistas))
        {
            File.Delete(ArquivoArtistas);
        }
        if (File.Exists(ArquivoMusicas))
        {
            File.Delete(ArquivoMusicas);
        }

        using (StreamWriter sW = new StreamWriter(File.Open(ArquivoArtistas, FileMode.OpenOrCreate)))
        {
            try
            {
                string json = JsonSerializer.Serialize(Artistas);
                sW.Write(json);
                sW.Flush();
                sW.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao salvar o arquivo {ArquivoArtistas}: {e.Message}");
            }
        }

        using (StreamWriter sW = new StreamWriter(File.Open(ArquivoMusicas, FileMode.OpenOrCreate)))
        {
            try
            {
                string json = JsonSerializer.Serialize(Musicas);
                sW.Write(json);
                sW.Flush();
                sW.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao salvar o arquivo {ArquivoMusicas}: {e.Message}");
            }
        }
    }

    public void AdicionarArtista(Artista artista)
    {
        if (BuscarArtistaPorNome(artista.Nome) != null)
        {
            Console.WriteLine("Artista já cadastrado. \n");
            return;
        }

        this.Artistas.Add(artista);
        Console.WriteLine("Artista cadastrado com sucesso! \n");
    }

    public void AdicionarMusica(Musica musica)
    {
        Artista artistaExistente = BuscarArtistaPorNome(musica.Artista.Nome);

        if (artistaExistente == null)
        {
            Console.WriteLine("Artista não encontrado. Cadastre o artista primeiro.");
            return;
        }

        artistaExistente.Musicas.Add(musica);
        this.Musicas.Add(musica);
        Console.WriteLine("Música cadastrada com sucesso! \n");
    }

    public void RemoverMusica(string nomeMusica)
    {
        Musica musica = BuscarMusicaPorNome(nomeMusica);
        Artista artistaExistente = BuscarArtistaPorNome(musica.Artista.Nome);

        if (artistaExistente == null)
        {
            Console.WriteLine("Não foi possível remover a música. Artista não encontrado.");
            return;
        }

        artistaExistente.Musicas.Remove(musica);
        this.Musicas.Remove(musica);
        Console.WriteLine($"A música {musica.Nome} foi removida com sucesso! \n");
    }

    public void RemoverArtista(string nomeArtista)
    {
        Artista artista = BuscarArtistaPorNome(nomeArtista);

        if (artista == null)
        {
            Console.WriteLine("Artista não encontrado.");
            return;
        }

        foreach (var musica in artista.Musicas)
        {
            this.Musicas.Remove(musica);
        }

        this.Artistas.Remove(artista);
        Console.WriteLine($"artista: {artista.Nome} removido com sucesso! \n");
    }

    public Artista BuscarArtistaPorNome(string nome)
    {
        nome = nome.ToLower();
        return Artistas.FirstOrDefault(artista => artista.Nome.ToLower() == nome);
    }

    public void ExibirTodosArtistas()
    {
        foreach (var artista in Artistas)
        {
            Console.WriteLine($"- {artista.Nome}");
        }
        Console.WriteLine("\n");
    }

    public void ExibirTodasMusicas()
    {
        foreach (var musica in Musicas)
        {
            Console.WriteLine($"- {musica.Nome} - {musica.Artista.Nome}");
        }
        Console.WriteLine("\n");
    }

    public Musica BuscarMusicaPorNome(string nome)
    {
        nome = nome.ToLower();
        return Musicas.FirstOrDefault(musica => musica.Nome.ToLower() == nome);
    }

}
