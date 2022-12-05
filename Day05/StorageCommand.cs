namespace Day05;

public class StorageCommand
{
    public static StorageCommand Create(string commandString)
    {
        var values = commandString.Split(" ");
        return new StorageCommand
        {
            Count = int.Parse(values[1]),
            SourceIndex = int.Parse(values[3])-1,
            DestinationIndex = int.Parse(values[5])-1
        };
    }

    public int Count { get; set; }
    public int SourceIndex { get; set; }
    public int DestinationIndex { get; set; }

    public override string ToString()
    {
        return $"move {Count} from {SourceIndex + 1} to {DestinationIndex + 1}";
    }
}
