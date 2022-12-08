namespace Day07.TestFiles;

public class Directory
{
    public string Name { get; private set; }
    private Directory? _parentDirectory;
    
    private List<Directory> _directories;
    private List<File> _files;

    public Directory(string name, Directory? parentDirectory)
    {
        Name = name;
        _parentDirectory = parentDirectory;
        
        _directories = new List<Directory>();
        _files = new List<File>();
    }

    public long GetDirectorySize()
    {
        return _directories.Sum(x => x.GetDirectorySize()) + _files.Sum(x => x.Size);
    }

    public Directory AddDirectory(string name)
    {
        var newDir = new Directory(name, this);
        _directories.Add(newDir);
        return newDir;
    }

    public void AddFile(string name, long size)
    {
        _files.Add(new File(name,size));
    }

    public Directory? ParentDirectory()
    {
        return _parentDirectory ?? null;
    }

    public Directory? NextDirectory(string name)
    {
        Directory nextDir = _directories.FirstOrDefault(x => x.Name == name);
        return nextDir ?? null;
    }

    public override string ToString()
    {
        return $"Directory {Name} has Size {GetDirectorySize()} bytes";
    }
}