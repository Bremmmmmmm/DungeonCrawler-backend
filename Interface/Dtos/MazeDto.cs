namespace Interface.Dtos;

public class MazeDto
{
    public int size { get; set; }
    public bool[][] visited { get; set; }
    public bool[][] hConnections { get; set; }
    public bool[][] vConnections { get; set; }
    public string[][] roomValues { get; set; }
}
