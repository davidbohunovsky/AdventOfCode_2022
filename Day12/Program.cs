using Day12;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);

var WIDTH = lines.Length;
var HEIGHT = lines[0].Length;

var mountains = new char[WIDTH][];

Vector2Int start = new Vector2Int(0, 0);
Vector2Int goal = new Vector2Int(0, 0);

List<Vector2Int> lowestPoints = new List<Vector2Int>();

for(var x = 0; x <WIDTH; x++)
{
    mountains[x] = new char[HEIGHT];

    for (var y = 0; y < HEIGHT; y++)
    {
        mountains[x][y] = lines[x][y];

        if (mountains[x][y] == 'S')
        {
            start = new Vector2Int(x, y);
            mountains[x][y] = 'a';
        }
        
        if (mountains[x][y] == 'a')
        {
            lowestPoints.Add(new Vector2Int(x, y));
        }
        
        if (mountains[x][y] == 'E')
        {
            goal = new Vector2Int(x, y);
            mountains[x][y] = 'z';
        }
    }
}

var BFS = new BFS(mountains, WIDTH, HEIGHT);
Console.WriteLine($"Part 1: {BFS.ShortestPath(start,goal)}");
Console.WriteLine($"Part 2: {lowestPoints.Select(a => BFS.ShortestPath(a,goal)).Where(a => a != 0).MinBy(x => x)}");




