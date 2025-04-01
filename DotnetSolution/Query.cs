using Microsoft.Extensions.DependencyInjection;

namespace DotnetSolution;

public partial class Query
{
    private readonly ListOfSolutions _listOfSolutions = services.GetRequiredService<ListOfSolutions>();
    private readonly IGraphQLClient _graphqlClient = services.GetRequiredService<IGraphQLClient>();

    public partial string? GetNearestSolutionPath(string? folderPath)
    {
        var solutionFile = _listOfSolutions.GetDefaultSolution(folderPath ?? "");
        return solutionFile;
    }
}