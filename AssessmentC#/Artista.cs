using System;
using System.Collections.Generic;

public class Artista
{
    public string Nome { get; set; }
    public string Genero { get; set; }
    public string Local { get; set; }
    public List<Musica> Musicas { get; set; } = new List<Musica>();

}
