using Interface.Dtos;
using Interface.Interfaces.Logic;
using Interface.Interfaces.Dal;
using Logic.Classes;

namespace Logic.Containers;

public class MazeContainer(IDalFactory dalFactory) : IMazeContainer
{
    private bool[,] visited;
    private bool[,] hConnections;
    private bool[,] vConnections;
    private int size;
    private string[,] roomValues;

    public Task<MazeDto> CreateMaze(int size)
    {
        this.size = size;
        visited = new bool[size, size];
        hConnections = new bool[size, size - 1];
        vConnections = new bool[size - 1, size];
        roomValues = new string[size, size];

        int center = size / 2;
        DFS(center, center);
        AddExtraConnections();
        AssignRoomValues();

        var maze = new Maze(size)
        {
            hConnections = hConnections,
            vConnections = vConnections,
            roomValues = roomValues
        };

        return Task.FromResult(maze.ToDto());
    }

    private void DFS(int r, int c)
    {
        visited[r, c] = true;

        var dirs = new List<(int dr, int dc, string type)>
        {
            (0, 1, "h"), (1, 0, "v"), (0, -1, "h"), (-1, 0, "v")
        }.OrderBy(_ => Guid.NewGuid()).ToList(); // random shuffle

        foreach (var (dr, dc, type) in dirs)
        {
            int nr = r + dr, nc = c + dc;
            if (nr < 0 || nr >= size || nc < 0 || nc >= size) continue;
            if (visited[nr, nc]) continue;

            if (type == "h")
            {
                if (dc == 1) hConnections[r, c] = true;
                else hConnections[r, c - 1] = true;
            }
            else
            {
                if (dr == 1) vConnections[r, c] = true;
                else vConnections[r - 1, c] = true;
            }

            DFS(nr, nc);
        }
    }

    private void AddExtraConnections(double probability = 0.20)
    {
        var rand = new Random();
        // Horizontal walls
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size - 1; c++)
            {
                if (!hConnections[r, c] && rand.NextDouble() < probability)
                {
                    hConnections[r, c] = true;
                }
            }
        }

        // Vertical walls
        for (int r = 0; r < size - 1; r++)
        {
            for (int c = 0; c < size; c++)
            {
                if (!vConnections[r, c] && rand.NextDouble() < probability)
                {
                    vConnections[r, c] = true;
                }
            }
        }
    }
    
    private void AssignRoomValues()
    {
        var rand = new Random();
        int center = size / 2;
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                if (r == center && c == center)
                {
                    roomValues[r, c] = null;
                    continue;
                }
                int roll = rand.Next(1000); // 0-999
                if (roll < 30) // 3%
                    roomValues[r, c] = "item";
                else if (roll < 230) // next 10%
                    roomValues[r, c] = "enemy";
                else
                    roomValues[r, c] = null;
            }
        }
    }
}