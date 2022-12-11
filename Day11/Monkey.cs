using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Day11;

public class Monkey
{
    private Queue<long> _items = new Queue<long>();
    public bool HasItems => _items.Count > 0;

    private readonly int _index;
    private readonly (OperationType Operation, long Value) _operation;
    private readonly int _testDivider;
    private readonly int _trueMonkey;
    private readonly int _falseMonkey;

    public int InspectedItems { get; private set; } = 0;
    
    public Monkey(int index, List<int> startingItems, string operation, int testDivider, int trueMonkey, int falseMonkey)
    {
        _index = index;
        startingItems.ForEach(x => _items.Enqueue(x));
        _operation = operation.TranslateOperation();
        _testDivider = testDivider;
        _trueMonkey = trueMonkey;
        _falseMonkey = falseMonkey;
    }
    
    public (long Value, int MonkeyIndex) Inspect(bool condition)
    {
        InspectedItems++;
        var item = _items.Dequeue();


        item = Operation(item);

        if (condition)
            item /= 3;

        var index = item % _testDivider == 0 ? _trueMonkey : _falseMonkey;
        return (item, index);
    }

    public void AddItem(long item)
    {
        _items.Enqueue(item);
    }
    
    private long Operation(long item)
    {
        return _operation.Operation switch
        {
            OperationType.OldSumConst => item + _operation.Value,
            OperationType.OldSumOld => item + item,
            OperationType.OldPowerConst => item * _operation.Value,
            OperationType.OldPowerOld => item * item,
            _ => item
        };
    }

    public override string ToString()
    {
        return $"Monkey {_index} inspected items {InspectedItems} times.";
    }
}