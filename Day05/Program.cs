using System.Diagnostics;
using Day05;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);

var storageColumns = (lines[0].Length + 1) / 4;

var inputs = new List<string>();
var commands = new List<StorageCommand>();

var inputEnd = false;

foreach (var line in lines)
{
    if (line.Length == 0)
    {
        inputEnd = true;
        continue;
    }
    
    if(inputEnd)
        commands.Add(StorageCommand.Create(line));
    else
        inputs.Add(line);
}


void GetSolution(List<string> storageInputs, List<StorageCommand> storageCommands, MachineType type)
{
    var storage = new Storage(storageColumns);
    
    storageInputs
        .Take(storageInputs.Count-1)
        .Reverse()
        .ToList()
        .ForEach(x => storage.AddToStorage(x));
    
    storageCommands.ForEach(x => storage.DoCommand(x,type));
    Console.WriteLine(storage.GetSolution());
}

GetSolution(inputs,commands,MachineType.CrateMover9000);
GetSolution(inputs,commands,MachineType.CrateMover9001);

