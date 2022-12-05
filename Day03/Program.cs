using System.Text;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

string GetCommonChar(string[] values)
{
    return values[0]
        .FirstOrDefault(c1 => values[1].Any(c2 => c1 == c2))
        .ToString();
}

string GetBadge(string[] values)
{
    return values[0]
        .FirstOrDefault(c1 => values[1]
            .Any(c2 => values[2]
                .Any(c3 => c1 == c2 && c2 == c3)))
        .ToString();
}

bool IsUpperCase(string c)
{
    return string.CompareOrdinal(c, c.ToUpper()) == 0;
}

int GetValue(string c)
{
    return Encoding.ASCII.GetBytes(c)[0] - (IsUpperCase(c) ? 38 : 96);
}

var lines = File.ReadAllLines(testDataPath);

var parOneSum = lines
    .Select(x => x.Insert(x.Length / 2, " ").Split(" "))
    .Select(GetCommonChar)
    .Sum(GetValue);

Console.WriteLine($"Part 1: { parOneSum }");


var totalCount = 0;

for (var i = 0; i < lines.Length; i += 3)
{
    totalCount += GetValue(GetBadge(lines.Skip(i).Take(3).ToArray()));
}

Console.WriteLine($"Part 2: {totalCount}");