using Day07.TestFiles;
using File = System.IO.File;

const long DISK_SIZE = 70000000;
const long UPDATE_SIZE = 30000000;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var cdmCommands = File.ReadAllLines(testDataPath).ToList();

var system = new FileSystem();

cdmCommands.ForEach(command => system.CMD(command));

//system.ListDirectories();
Console.WriteLine($"Solution Part One: {system.SolutionPartOne()}");

var takenSpace = system.SystemSize();
var needSpace = UPDATE_SIZE - (DISK_SIZE - takenSpace);

Console.WriteLine($"Solution Part Two: {system.SolutionPartTwo(needSpace)}");