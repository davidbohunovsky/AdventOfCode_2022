string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);

var WIDTH = lines[0].Length;
var HEIGHT = lines.Length;

Console.WriteLine($"W:{WIDTH} H:{HEIGHT}");

var forest = new int[WIDTH][];

int VisibleTrees(int[][] forest, int W, int H)
{
    var edgeCount = EdgeCount();
    Console.WriteLine(edgeCount);
    
    var interior = InteriorCount();
    Console.WriteLine(interior);
    
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
        
        var top = true;
        var bottom = true;
        var left = true;
        var right = true;

        for (var x = tx - 1; x > -1; x--)
        {
            if (forest[x][ty] >= forest[tx][ty]) 
                left = false;
        }
        
        for (var x = tx + 1; x < W; x++)
        {
            if (forest[x][ty] >= forest[tx][ty]) 
                right = false;
        }

        for (var y = ty - 1; y > -1; y--)
        {
            if (forest[tx][y] >= forest[tx][ty])
                top = false;
        }

        for (var y = ty + 1; y < H; y++)
        {
            if (forest[tx][y] >= forest[tx][ty])
                bottom = false;
        }

        //Console.WriteLine($"{forest[tx][ty]} - {top || bottom || left || right}");
        return top || bottom || left || right;
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
        
        for (var x = tx - 1; x > -1; x--)
        {
            left++;
            if (forest[x][ty] >= forest[tx][ty])
                break;
        }
        
        for (var x = tx + 1; x < W; x++)
        {
            right++;
            if (forest[x][ty] >= forest[tx][ty]) 
                break;
        }

        for (var y = ty - 1; y > -1; y--)
        {
            top++;
            if (forest[tx][y] >= forest[tx][ty])
                break;
        }

        for (var y = ty + 1; y < H; y++)
        {
            bottom++;
            if (forest[tx][y] >= forest[tx][ty])
                break;

          
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



