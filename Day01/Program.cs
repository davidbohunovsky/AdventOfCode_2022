string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File
    .ReadAllText(testDataPath)
    .Split("\r\n\r\n")
    .ToList();

var sums = lines.Select(line => line.Split("\r\n").Select(int.Parse).Sum()).ToList();

Console.WriteLine($"Part 1: {sums.MaxBy(x => x)}");
Console.WriteLine($"Part 2: {sums.OrderByDescending(x => x).Take(3).Sum()}");