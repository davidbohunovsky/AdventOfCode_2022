namespace Day10;

public enum InstructionType
{
    Noop,
    AddX
}

public class Instruction
{
    private readonly InstructionType _type;
    private readonly int _value;

    private bool _prepared;
    
    public Instruction(InstructionType type, int value = 0)
    {
        _type = type;
        _value = value;
    }

    public bool Process(ref int stackValue)
    {
        switch (_type)
        {
            case InstructionType.Noop:
                return true;

            case InstructionType.AddX:
                if (_prepared)
                {
                    stackValue += _value;
                    return true;
                }

                _prepared = true;
                return false;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
