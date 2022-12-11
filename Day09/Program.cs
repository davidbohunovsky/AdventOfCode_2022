using System.Numerics;
using Day09;

string trainDataPath = @"TestFiles\trainData.txt";
string trainData2Path = @"TestFiles\trainData2.txt";
string testDataPath = @"TestFiles\testData.txt";

(Direction Direction, int Steps) ParseInput(string input)
{
    var inputValues = input.Split(" ");
    return (inputValues[0].StringInputToDirection(), int.Parse(inputValues[1]));
}

var ropeMoves = File.ReadAllLines(testDataPath).Select(ParseInput);

var tail = Vector2.Zero;
var head = Vector2.Zero;
var tails = new List<Vector2>();

for (var i = 0; i < 9; i++)
{
    tails.Add(Vector2.Zero);
}

var visitedPositions = new List<Vector2>()
{
    Vector2.Zero
};

var visitedTailsPositions = new List<Vector2>()
{
    Vector2.Zero
};

foreach(var ropeMove in ropeMoves)
{
    for (var i = 0; i < ropeMove.Steps; i++)
    {
        head = head.Move(ropeMove.Direction.Vector());
        tail = tail.Move(tail.DistanceDirection(head));
        
        for (var j = 0; j < tails.Count; j++)
        {
            tails[j] = tails[j].Move(tails[j].DistanceDirection(j == 0 ? head : tails[j - 1]));
        }
        
        if(!visitedPositions.Contains(tail))
            visitedPositions.Add(tail);
        
        if(!visitedTailsPositions.Contains(tails.Last()))
            visitedTailsPositions.Add(tails.Last());
    }
}

Console.WriteLine($"Short rope {visitedPositions.Count}");
Console.WriteLine($"Long rope {visitedTailsPositions.Count}");

