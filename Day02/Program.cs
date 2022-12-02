string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

Symbol GetSymbol(string stringSymbol)
{
    return stringSymbol[0] switch
    {
        'A' or 'X' => Symbol.Rock,
        'B' or 'Y' => Symbol.Paper,
        'C' or 'Z' => Symbol.Scissors,
        _ => throw new Exception("Invalid input data")
    };
}

int GetPointsOne(Symbol opponent, Symbol player)
{
    return CalculateWin() + CalculateSymbol();

    int CalculateWin()
    {
        // Draw
        if (opponent == player) return 3;

        if ((opponent == Symbol.Paper && player == Symbol.Scissors)
            || (opponent == Symbol.Rock && player == Symbol.Paper)
            || (opponent == Symbol.Scissors && player == Symbol.Rock))
            return 6;

        return 0;
    }

    int CalculateSymbol()
    {
        return (int)player;
    }
}

int GetPointsTwo(Symbol opponent, Symbol player)
{
    var gameResult = GetResult(player);
    
    return CalculateGame() + CalculateSymbol();

    int CalculateGame()
    {
        return (int)gameResult;
    }

    int CalculateSymbol()
    {
        if (gameResult == Result.Draw) 
            return (int)opponent;
        
        if (gameResult == Result.Lose)
        {
            return opponent switch
            {
                Symbol.Rock => (int)Symbol.Scissors,
                Symbol.Paper => (int)Symbol.Rock,
                Symbol.Scissors => (int)Symbol.Paper,
                _ => throw new Exception("Invalid input data")
            };
        }
        
        return opponent switch
        {
            Symbol.Rock => (int)Symbol.Paper,
            Symbol.Paper => (int)Symbol.Scissors,
            Symbol.Scissors => (int)Symbol.Rock,
            _ => throw new Exception("Invalid input data")
        };
    }
    
    Result GetResult(Symbol symbol)
    {
        // Well it works.. :D
        return symbol switch
        {
            Symbol.Rock => Result.Lose,
            Symbol.Paper => Result.Draw,
            Symbol.Scissors => Result.Win,
            _ => throw new Exception("Invalid input data")
        };
    }
}

(Symbol opponent, Symbol player) GetGame(string stringValue)
{
    var gameValues = stringValue.Split(" ");
    
    if (gameValues.Length != 2)
        throw new Exception("Invalid input data");
        
    return (GetSymbol(gameValues[0]), GetSymbol(gameValues[1]));
}

var lines = File
    .ReadAllText(testDataPath)
    .Split("\n")
    .Select(GetGame)
    .ToList();

var partOne = lines
    .Select(x => GetPointsOne(x.opponent, x.player))
    .Sum();

var partTwo = lines
    .Select(x => GetPointsTwo(x.opponent, x.player))
    .Sum();


Console.WriteLine($"Part One: {partOne}");
Console.WriteLine($"Part One: {partTwo}");

enum Symbol
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

enum Result
{
    Win = 6,
    Lose = 0,
    Draw = 3
}