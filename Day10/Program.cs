using System.Text;
using Day10;
using Microsoft.VisualBasic.FileIO;

string trainDataPath = @"TestFiles\trainData.txt";
string trainData2Path = @"TestFiles\trainData2.txt";
string testDataPath = @"TestFiles\testData.txt";

Instruction ParseInput(string input)
{
    var inputValues = input.Split(" ");
    switch (inputValues[0])
    {
        case "addx":
            return new Instruction(InstructionType.AddX, int.Parse(inputValues[1]));

        case "noop":
            return new Instruction(InstructionType.Noop);

        default: throw new InvalidDataException("This should not happen");
    }
}

Char DrawPixel(int cycle, int stackValue)
{
    var cycleStackPosition = (cycle > 40 ? cycle%40 : cycle) - stackValue;
    return cycleStackPosition is >= 0 and < 3 ? '#' : '.';

}

var instructions = File.ReadAllLines(testDataPath)
    .Select(ParseInput)
    .ToList();

var stackValue = 1;
var cycle = 1;

var milestones = new int[] { 20, 60, 100, 140, 180, 220 };
var milestoneValues = new List<int>();

var stoppingCycle = 241;
var instructionIndex = 0;

List<char> CRT = new List<char>();

while (cycle != stoppingCycle)
{
    if(milestones.Contains(cycle))
        milestoneValues.Add(cycle * stackValue);
    
    CRT.Add(DrawPixel(cycle,stackValue));
    
    if (instructions[instructionIndex].Process(ref stackValue))
        instructionIndex++;

    cycle++;
}

Console.WriteLine(milestoneValues.Sum());
Console.WriteLine();

StringBuilder sb = new StringBuilder();

var nextLineIndex = 40;
var currentLineIndex = 0;
foreach(var pixel in CRT)
{
    sb.Append(pixel);
    currentLineIndex++;
    if (currentLineIndex == nextLineIndex)
    {
        sb.AppendLine();
        currentLineIndex = 0;
    }
}

Console.WriteLine(sb.ToString());