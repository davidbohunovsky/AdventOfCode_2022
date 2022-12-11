using System.Text.RegularExpressions;

namespace Day11;

public enum OperationType
{
    OldSumConst,
    OldSumOld,
    OldPowerConst,
    OldPowerOld,
}

public static class Operation
{
    static Regex OldSumConst = new Regex(@"old [+] (?<number>\d+)");
    static Regex OldSumOld = new Regex(@"old [+] old");
    static Regex OldPowerConst =  new Regex(@"old [*] (?<number>\d+)");
    static Regex OldPowerOld =  new Regex(@"old [*] old");
    
    public static (OperationType operation, long constantvalue) TranslateOperation(this string operation)
    {
        if (OldSumConst.IsMatch(operation))
        {
            var match = OldSumConst.Match(operation);
            var constantNumber = long.Parse(match.Groups["number"].Value);
            return (OperationType.OldSumConst, constantNumber);
        }
        
        if (OldSumOld.IsMatch(operation))
        {
            return (OperationType.OldSumOld, 0);
        }
        
        if (OldPowerConst.IsMatch(operation))
        {
            var match = OldPowerConst.Match(operation);
            var constantNumber = long.Parse(match.Groups["number"].Value);
            return (OperationType.OldPowerConst, constantNumber);

        }
        
        if (OldPowerOld.IsMatch(operation))
        {
            return (OperationType.OldPowerOld, 0);
        }

        throw new InvalidCastException("Regex Failed");
    }
}