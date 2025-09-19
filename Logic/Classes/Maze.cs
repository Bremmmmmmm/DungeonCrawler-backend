using Interface.Dtos;

namespace Logic.Classes;

public class Maze
{
    public int size { get; set; }
    public bool[,] visited { get; set; }
    public bool[,] hConnections { get; set; }
    public bool[,] vConnections { get; set; }
    public string[,] roomValues { get; set; }

    public MazeDto ToDto()
    {
        return new MazeDto
        {
            size = size,
            visited = ToJagged(visited),
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
}