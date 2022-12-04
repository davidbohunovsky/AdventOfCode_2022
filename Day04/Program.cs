string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File
    .ReadAllLines(testDataPath)
    .Select(x => x.Split(","));

var contains = 0;
var overlaps = 0;

foreach (var line in lines)
{
    var firstElf = line[0].Split("-").Select(int.Parse).ToArray();
    var secondElf = line[1].Split("-").Select(int.Parse).ToArray();

    var firstStorage = Enumerable.Range(firstElf[0], firstElf[1] - firstElf[0] + 1).ToArray();
    var secondStorage = Enumerable.Range(secondElf[0], secondElf[1]- secondElf[0] + 1).ToArray();

    if (firstStorage.All(s1 => secondStorage.Contains(s1)) || secondStorage.All(s2 => firstStorage.Contains(s2)))
        contains++;

    if (firstStorage.Any(s1 => secondStorage.Contains(s1)) || secondStorage.Any(s2 => firstStorage.Contains(s2)))
        overlaps++;
}

Console.WriteLine($"Part1: {contains}");
Console.WriteLine($"Part2: {overlaps}");