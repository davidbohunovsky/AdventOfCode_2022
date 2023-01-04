using System.Text.RegularExpressions;
using Day11;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";


Regex indexRegex = new Regex(@"Monkey (?<index>\d+):");

/*
Regex divideRegex = new Regex(@"  Test: divisible by (?<index>\d+)");
Regex trueRegex = new Regex(@"    If true: throw to monkey (?<index>\d+)");
Regex falseRegex = new Regex(@"    If false: throw to monkey (?<index>\d+)");
*/

Monkey ParseInput(string input)
{
    var inputLines = input.Split("\r\n");

    var indexMatch = indexRegex.Match(inputLines[0]);
    var index = int.Parse(indexMatch.Groups["index"].Value);
    
    var startingItems = inputLines[1]
        .Split(":")[1]
        .Split(",")
        .Select(x => int.Parse(x.Trim()))
        .ToList();

    /*
    var divideMatch = indexRegex.Match(inputLines[3]);
    var divideTest = int.Parse(divideMatch.Groups["index"].Value);
    
    var trueMatch = indexRegex.Match(inputLines[4]);
    var trueMonkey = int.Parse(trueMatch.Groups["index"].Value);
    
    var falseMatch = indexRegex.Match(inputLines[5]);
    var falseMonkey = int.Parse(falseMatch.Groups["index"].Value);
    */
    
    var divideTest = int.Parse(inputLines[3].Split("by")[1]);
    var trueMonkey = int.Parse(inputLines[4].Split("monkey")[1]);
    var falseMonkey = int.Parse(inputLines[5].Split("monkey")[1]);

    return new Monkey(index,startingItems, inputLines[2], divideTest, trueMonkey, falseMonkey);
}

var monkeys = File.ReadAllText(testDataPath)
    .Split("\r\n\r\n")
    .Select(ParseInput)
    .ToList();

var round = 10000;
bool condition = false;

// Nejmensi spolecny nasobek
var modulo = monkeys.Select(m => m.Divider).Aggregate(1,(current,next) => current * next);

for (var i = 0; i < round; i++)
{
    foreach (var monkey in monkeys)
    {
        while (monkey.HasItems)
        {
            var inspectedItem = monkey.Inspect(condition,modulo);
            monkeys[inspectedItem.MonkeyIndex].AddItem(inspectedItem.Value);
        }
    }
}

monkeys.ForEach(x => Console.WriteLine(x.ToString()));

var topMonkeys = monkeys
    .OrderByDescending(x => x.InspectedItems)
    .Take(2)
    .ToList();

Console.WriteLine($"Monkey Business: {topMonkeys[0].InspectedItems * topMonkeys[1].InspectedItems}");
