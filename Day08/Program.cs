string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);

var WIDTH = lines[0].Length;
var HEIGHT = lines.Length;

//Console.WriteLine($"W:{WIDTH} H:{HEIGHT}");

var forest = new int[WIDTH][];

int VisibleTrees(int[][] forest, int W, int H)
{
    var edgeCount = EdgeCount();
    //Console.WriteLine(edgeCount);
    
    var interior = InteriorCount();
    //Console.WriteLine(interior);
    
    return edgeCount + interior;

    int EdgeCount()
    {
        return 2 * (W - 1) + 2 * (H - 1);
    }

    int InteriorCount()
    {
        var visibleTrees = 0;
        for(var x = 1; x < W-1; x++)
        {
            for (var y = 1; y < H - 1; y++)
            {
                visibleTrees += VisibleFromAnySide(x, y) ? 1 : 0;
            }
        }

        return visibleTrees;
    }

    bool VisibleFromAnySide(int tx, int ty)
    {
        if (forest[tx][ty] == 0) return false;

        var left = forest[tx][..ty].All(y => y < forest[tx][ty]);
        if (left) return true;
        
        var right = forest[tx][(ty + 1)..].All(y => y < forest[tx][ty]);
        if (right) return true;
        
        var bottom = forest[(tx + 1)..].All(x => x[ty] < forest[tx][ty]);
        if (bottom) return true;
        
        var top = forest[..tx].All(x => x[ty] < forest[tx][ty]);
        return top;
    }
}

int ScenicScore(int[][] forest, int W, int H)
{
    var highestScore = 0;
    for(var x = 1; x < W-1; x++)
    {
        for (var y = 1; y < H - 1; y++)
        {
            var newScore = CalculateScenicScore(x, y);
            highestScore = newScore > highestScore
                ? newScore
                : highestScore;
        }
    }

    return highestScore;
    
    int CalculateScenicScore(int tx, int ty)
    {
        if (forest[tx][ty] == 0) return 0;
        
        var top = 0;
        var bottom = 0;
        var left = 0;
        var right = 0;

        foreach (var x in forest[tx][..ty].Select(y => y).Reverse())
        {
            left++;
            if (x >= forest[tx][ty]) break;
        }
        foreach (var x in forest[tx][(ty + 1)..].Select(y => y))
        {
            right++;
            if (x >= forest[tx][ty]) break;
        }
        foreach (var x in forest[(tx + 1)..].Select(x => x[ty]))
        {
            bottom++;
            if (x >= forest[tx][ty]) break;
        }
        foreach (var x in forest[..tx].Select(x => x[ty]).Reverse())
        {
            top++;
            if (x >= forest[tx][ty]) break;
        }
        
        return left * right * top * bottom;
    }
}

for(var x = 0; x <WIDTH; x++)
{
    forest[x] = new int[HEIGHT];
    for (var y = 0; y < HEIGHT; y++)
    {
        forest[x][y] = int.Parse(lines[x][y].ToString());
        //Console.Write(forest[x][y]);
    }
    //Console.WriteLine();
}

Console.WriteLine($"Visible trees: {VisibleTrees(forest,WIDTH,HEIGHT)}");
Console.WriteLine($"Highest scenic score: {ScenicScore(forest,WIDTH,HEIGHT)}");



