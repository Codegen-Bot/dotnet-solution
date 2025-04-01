using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetSolution;

public class ListOfSolutions
{
    private readonly List<string> _solutionFileList = new();
    private readonly Dictionary<string, HashSet<string>> _solutionConfigurations = new();

    public string? GetDefaultSolution(string projectFilePath)
    {
        if (projectFilePath.StartsWith("/"))
        {
            projectFilePath = projectFilePath.Remove(0, 1);
        }

        if (projectFilePath == "")
        {
            return _solutionFileList.Where(x => !x.Trim('/').Contains("/")).OrderBy(x => x.Length).FirstOrDefault();
        }
        
        foreach (var solutionFile in _solutionFileList.OrderByDescending(x => x.Length))
        {
            var solutionFolder = Path.GetDirectoryName(solutionFile)!.Replace("\\", "/");
            if (solutionFolder.StartsWith("/"))
            {
                solutionFolder = solutionFolder.Remove(0, 1);
            }

            if (projectFilePath.StartsWith(solutionFolder))
            {
                return solutionFile;
            }
        }

        return null;
    }

    public bool ContainsSolution(string solutionFilePath)
    {
        return _solutionFileList.Contains(solutionFilePath);
    }

    public void AddSolution(string solutionFilePath)
    {
        _solutionFileList.Add(solutionFilePath);
    }

    public void AddSolutionConfiguration(string solutionFilePath, string solutionConfigurationPlatform)
    {
        if (!_solutionConfigurations.TryGetValue(solutionFilePath, out var solutionConfigurations))
        {
            solutionConfigurations = new();
            _solutionConfigurations.Add(solutionFilePath, solutionConfigurations);
        }

        solutionConfigurations.Add(solutionConfigurationPlatform);
    }
    
    public IEnumerable<string> GetSolutionConfigurations(string solutionFilePath)
    {
        if (!_solutionConfigurations.TryGetValue(solutionFilePath, out var solutionConfigurations))
        {
            return Enumerable.Empty<string>();
        }
        
        return solutionConfigurations;
    }
}