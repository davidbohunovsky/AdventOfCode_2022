namespace Day07.TestFiles;

public class FileSystem
{
    private readonly string ROOT = "/";
    private readonly string BACK = "..";

    private readonly string COMMAND = "$";
    private readonly string CMD_DIR = "cd";
    private readonly string CMD_LS = "ls";
    
    private readonly string LS_DIRECTORY = "dir";

    private readonly Directory _root;
    private Directory? _currentDirectory;

    private List<Directory> _systemDirectories;
    
    private bool _listing;
    
    public FileSystem()
    {
        _root = _currentDirectory = new Directory("",null);
        _systemDirectories = new List<Directory>();
    }

    public void CMD(string command)
    {
        if (command[..1].Equals(COMMAND))
        {
            _listing = false;
            var commandSplit = command.Split(" ");
            if (commandSplit[1].Equals(CMD_DIR))
            {
                DirectoryChange(commandSplit[2]);
            }

            if (commandSplit[1].Equals(CMD_LS))
            {
                _listing = true;
            }
        }
        else // _listing == true
        {
            var listSplit = command.Split(" ");
            if (listSplit[0].Equals(LS_DIRECTORY))
            {
                _systemDirectories.Add(_currentDirectory?.AddDirectory(listSplit[1]));
            }
            else // File
            {
                long.TryParse(listSplit[0], out var fileSize);
                _currentDirectory?.AddFile(listSplit[1],fileSize);
            }
        }
    }

    public long SystemSize() => _root.GetDirectorySize();
    
    public long SolutionPartOne(long maxSize = 100000)
    {
        return _systemDirectories
            .Where(x => x.GetDirectorySize() <= maxSize)
            .Sum(x => x.GetDirectorySize());
    }

    public long SolutionPartTwo(long minSize = 8381165) // Traindata
    {
        return _systemDirectories
            .Where(x => x.GetDirectorySize() > minSize)
            .OrderBy(x => x.GetDirectorySize())
            .First()
            .GetDirectorySize();
    }

    public void ListDirectories()
    {
        _systemDirectories
            .OrderBy(x => x.GetDirectorySize())
            .ToList()
            .ForEach(x => Console.WriteLine(x.ToString()));
    }
    
    private void DirectoryChange(string directoryName)
    {
        if (directoryName.Equals(ROOT))
        {
            _currentDirectory = _root;
            return;
        }

        _currentDirectory = directoryName.Equals(BACK) 
            ? _currentDirectory?.ParentDirectory() 
            : _currentDirectory?.NextDirectory(directoryName);

        _currentDirectory ??= _root;
    }
    
    
}