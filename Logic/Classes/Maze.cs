using Interface.Dtos;

namespace Logic.Classes;

public class Maze
{
    public int size { get; set; }
    public bool[,] visited { get; set; }
    public bool[,] hConnections { get; set; }
    public bool[,] vConnections { get; set; }
    public string[,] roomValues { get; set; }

    
    public Maze(int size)
    {
        this.size = size;
        visited = new bool[size, size];
        hConnections = new bool[size, size - 1];
        vConnections = new bool[size - 1, size];
        roomValues = new string[size, size];
    }
    
    public MazeDto ToDto()
    {
        return new MazeDto
        {
            size = size,
            hConnections = ToJagged(hConnections),
            vConnections = ToJagged(vConnections),
            roomValues = roomValues == null ? null : ToJagged(roomValues)
        };
    }

    private static bool[][] ToJagged(bool[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        var jagged = new bool[rows][];
        for (int r = 0; r < rows; r++)
        {
            jagged[r] = new bool[cols];
            for (int c = 0; c < cols; c++)
                jagged[r][c] = array[r, c];
        }
        return jagged;
    }

    private static string[][] ToJagged(string[,] array)
    {
        if (array == null) return null;
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        var jagged = new string[rows][];
        for (int r = 0; r < rows; r++)
        {
            jagged[r] = new string[cols];
            for (int c = 0; c < cols; c++)
                jagged[r][c] = array[r, c];
        }
        return jagged;
    }
    
    public Maze(MazeDto dto)
    {
        size = dto.size;
        hConnections = FromJagged(dto.hConnections);
        vConnections = FromJagged(dto.vConnections);
        roomValues = dto.roomValues == null ? null : FromJagged(dto.roomValues);
        visited = new bool[size, size];
    }

    private static bool[,] FromJagged(bool[][] jagged)
    {
        int rows = jagged.Length;
        int cols = jagged[0].Length;
        var array = new bool[rows, cols];
        for (int r = 0; r < rows; r++)
        for (int c = 0; c < cols; c++)
            array[r, c] = jagged[r][c];
        return array;
    }

    private static string[,] FromJagged(string[][] jagged)
    {
        int rows = jagged.Length;
        int cols = jagged[0].Length;
        var array = new string[rows, cols];
        for (int r = 0; r < rows; r++)
        for (int c = 0; c < cols; c++)
            array[r, c] = jagged[r][c];
        return array;
    }
}

