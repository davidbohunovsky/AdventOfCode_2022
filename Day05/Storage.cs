namespace Day05;

public class Storage
{
    public List<Stack<string>> _storageColumns;

    public Storage(int storageRoomCount)
    {
        _storageColumns = new List<Stack<string>>();

        for (var i = 0; i < storageRoomCount; i++)
        {
            _storageColumns.Add(new Stack<string>());
        }
    }

    public void AddToStorage(string storageStringValues)
    {
        var storageValues = new List<string>();
        
        for (var i = 0; i < _storageColumns.Count; i++)
        {
            var value = storageStringValues[i * 4 + 1].ToString();
            storageValues.Add(value);
        }
        
        foreach (var storage in _storageColumns.Zip(storageValues, (c , rv) => new { Column = c, RowValue = rv}))
        {
            if(!string.IsNullOrWhiteSpace(storage.RowValue))
                storage.Column.Push(storage.RowValue);
        }
    }

    public void DoCommand(StorageCommand command, MachineType type)
    {
        switch (type)
        {
            case MachineType.CrateMover9000:
                for (var i = 0; i < command.Count; i++)
                {
                    var sValue = _storageColumns[command.SourceIndex].Pop();
                    _storageColumns[command.DestinationIndex].Push(sValue);
                }
                break;
            
            case MachineType.CrateMover9001:
                var crateStack = new Stack<string>();
                for (var i = 0; i < command.Count; i++)
                {
                    var sValue = _storageColumns[command.SourceIndex].Pop();
                    crateStack.Push(sValue);
                }
                
                while(crateStack.TryPop(out string popValue))
                    _storageColumns[command.DestinationIndex].Push(popValue);
                
                break;
        }
    }

    public string GetSolution()
    {
        var result = "";

        foreach (var column in _storageColumns)
        {
            column.TryPeek(out var value);
            result += value;
        }

        return result;
    }
}