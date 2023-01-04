using System.Numerics;

namespace Day12;

public class BFS
{
    private List<Vector2Int> _directions;

    private char[][] _grid;
    private int _width, _height;

    public BFS(char[][] grid, int width, int height)
    {
        _grid = grid;
        _width = width;
        _height = height;
        
        _directions = new List<Vector2Int>
        {
            new(0, 1),
            new(1, 0),
            new(0, -1),
            new(-1, 0)
        };
    }

    public int ShortestPath(Vector2Int start, Vector2Int goal)
    {
        var visited = new bool[_width][];
        for (var x = 0; x < _width; x++)
        {
            visited[x] = new bool[_height];

            for (var y = 0; y < _height; y++)
            {
                visited[x][y] = false;
            }
        }

        var queue = new Queue<List<Vector2Int>>();
        queue.Enqueue(new List<Vector2Int> { start });

        List<Vector2Int> currentPath;
        Vector2Int current;
        Vector2Int next;
        
        while (queue.Count > 0)
        {
            currentPath = queue.Dequeue();
            current = currentPath.Last();
            if (current == goal)
                return currentPath.Count - 1;

            foreach (var direction in _directions)
            {
                next = direction + current;
                
                if (!InBounds(next)) continue;
                if(visited[next.X][next.Y]) continue;
                if(!PossiblePath(current,next)) continue;
                
                queue.Enqueue( new List<Vector2Int>(currentPath) { next });
                visited[next.X][next.Y] = true;
            }
        }
        
        return 0;
    }

    private bool InBounds(Vector2Int point)
    {
        if (point.X < 0 || point.X >= _width) return false;
        if (point.Y < 0 || point.Y >= _height) return false;

        return true;
    }

    private bool PossiblePath(Vector2Int cur, Vector2Int next)
    {
        var result = _grid[next.X][next.Y] - _grid[cur.X][cur.Y];
        //Console.WriteLine($"{_grid[cur.X][cur.Y]} - {_grid[next.X][next.Y]} = {result} ");
        return result <= 1;
    }
}