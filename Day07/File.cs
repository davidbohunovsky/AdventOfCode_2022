namespace Day07.TestFiles;

public class File
{
    public long Size { get; private set; }
    public string Name { get; private set; }
    
    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }
}