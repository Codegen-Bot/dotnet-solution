using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using CodegenBot;

namespace DotnetSolution;

#pragma warning disable CS9113 // Parameter is unread.
public partial class Query(IServiceProvider services)
#pragma warning restore CS9113 // Parameter is unread.
{
    public JsonNode Resolve(
        IReadOnlyDictionary<string, object?> variables,
        IEnumerable<IPreParsedGraphQLSelection> selections
    )
    {
        var jsonNode = new JsonObject();

        foreach (var selection in selections)
        {
            if (selection is PreParsedGraphQLFieldSelection fieldSelection)
            {
                if (fieldSelection.Name == "nearestSolutionPath")
                {
                    string? folderPath = null;

                    foreach (var arg in fieldSelection.Arguments)
                    {
                        if (arg.Name == "folderPath")
                        {
                            if (arg.Value is PreParsedGraphQLStringValue literal)
                            {
                                folderPath = literal.Value;
                            }
                            else if (arg.Value is PreParsedGraphQLNullValue)
                            {
                                folderPath = null;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                folderPath = ((JsonElement?)result)?.GetString();
                            }
                            else
                            {
                                throw new ArgumentException("folderPath is not specified");
                            }
                        }
                    }
                    jsonNode[fieldSelection.Alias ?? fieldSelection.Name] = GetNearestSolutionPath(
                        folderPath
                    );
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return jsonNode;
    }

    public partial string? GetNearestSolutionPath(string? folderPath);
}

#pragma warning disable CS9113 // Parameter is unread.
public partial class Mutation(IServiceProvider services)
#pragma warning restore CS9113 // Parameter is unread.
{
    public JsonNode Resolve(
        IReadOnlyDictionary<string, object?> variables,
        IEnumerable<IPreParsedGraphQLSelection> selections
    )
    {
        var jsonNode = new JsonObject();

        foreach (var selection in selections)
        {
            if (selection is PreParsedGraphQLFieldSelection fieldSelection)
            {
                if (fieldSelection.Name == "addProject")
                {
                    string? solutionFile = null;
                    string? projectFilePath = null;
                    string? projectTypeUuid = null;
                    IReadOnlyList<string>? projectConfigurationPlatforms = null;

                    foreach (var arg in fieldSelection.Arguments)
                    {
                        if (arg.Name == "solutionFile")
                        {
                            if (arg.Value is PreParsedGraphQLStringValue literal)
                            {
                                solutionFile = literal.Value;
                            }
                            else if (arg.Value is PreParsedGraphQLNullValue)
                            {
                                solutionFile = null;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                solutionFile = ((JsonElement?)result)?.GetString();
                            }
                            else
                            {
                                throw new ArgumentException("solutionFile is not specified");
                            }
                        }
                        if (arg.Name == "projectFilePath")
                        {
                            if (arg.Value is PreParsedGraphQLStringValue literal)
                            {
                                projectFilePath = literal.Value;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                if (result is null)
                                {
                                    throw new ArgumentNullException("projectFilePath is null");
                                }
                                projectFilePath = ((JsonElement)result).GetString();
                            }
                        }
                        if (arg.Name == "projectTypeUuid")
                        {
                            if (arg.Value is PreParsedGraphQLStringValue literal)
                            {
                                projectTypeUuid = literal.Value;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                if (result is null)
                                {
                                    throw new ArgumentNullException("projectTypeUuid is null");
                                }
                                projectTypeUuid = ((JsonElement)result).GetString();
                            }
                        }
                        if (arg.Name == "projectConfigurationPlatforms")
                        {
                            if (arg.Value is PreParsedGraphQLListValue literal)
                            {
                                projectConfigurationPlatforms = literal
                                    .Value.Select(x =>
                                    {
                                        string? projectConfigurationPlatformsElement = null;
                                        if (x is PreParsedGraphQLStringValue literal)
                                        {
                                            projectConfigurationPlatformsElement = literal.Value;
                                        }
                                        else if (x is PreParsedGraphQLVariableValue graphqlVariable)
                                        {
                                            if (
                                                !variables.TryGetValue(
                                                    graphqlVariable.Name,
                                                    out var result
                                                )
                                            )
                                            {
                                                throw new ArgumentException(
                                                    $"{graphqlVariable.Name} is not specified"
                                                );
                                            }
                                            if (result is null)
                                            {
                                                throw new ArgumentNullException(
                                                    "projectConfigurationPlatformsElement is null"
                                                );
                                            }
                                            projectConfigurationPlatformsElement = (
                                                (JsonElement)result
                                            ).GetString();
                                        }
                                        if (projectConfigurationPlatformsElement is null)
                                        {
                                            throw new ArgumentException(
                                                "projectConfigurationPlatformsElement is not specified"
                                            );
                                        }
                                        return projectConfigurationPlatformsElement;
                                    })
                                    .ToList();
                            }
                            else if (arg.Value is PreParsedGraphQLNullValue)
                            {
                                projectConfigurationPlatforms = null;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                projectConfigurationPlatforms = ((JsonElement?)result)
                                    ?.EnumerateArray()
                                    .Select(x => x.GetString()!)
                                    .ToList();
                            }
                            else
                            {
                                throw new ArgumentException(
                                    "projectConfigurationPlatforms is not specified"
                                );
                            }
                        }
                    }
                    jsonNode[fieldSelection.Alias ?? fieldSelection.Name] = AddProject(
                        solutionFile,
                        projectFilePath
                            ?? throw new ArgumentNullException("projectFilePath is null"),
                        projectTypeUuid
                            ?? throw new ArgumentNullException("projectTypeUuid is null"),
                        projectConfigurationPlatforms
                    );
                }
                if (fieldSelection.Name == "addSolutionConfigurationPlatform")
                {
                    string? solutionFile = null;
                    string? path = null;
                    string? solutionConfigurationPlatform = null;

                    foreach (var arg in fieldSelection.Arguments)
                    {
                        if (arg.Name == "solutionFile")
                        {
                            if (arg.Value is PreParsedGraphQLStringValue literal)
                            {
                                solutionFile = literal.Value;
                            }
                            else if (arg.Value is PreParsedGraphQLNullValue)
                            {
                                solutionFile = null;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                solutionFile = ((JsonElement?)result)?.GetString();
                            }
                            else
                            {
                                throw new ArgumentException("solutionFile is not specified");
                            }
                        }
                        if (arg.Name == "path")
                        {
                            if (arg.Value is PreParsedGraphQLStringValue literal)
                            {
                                path = literal.Value;
                            }
                            else if (arg.Value is PreParsedGraphQLNullValue)
                            {
                                path = null;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                path = ((JsonElement?)result)?.GetString();
                            }
                            else
                            {
                                throw new ArgumentException("path is not specified");
                            }
                        }
                        if (arg.Name == "solutionConfigurationPlatform")
                        {
                            if (arg.Value is PreParsedGraphQLStringValue literal)
                            {
                                solutionConfigurationPlatform = literal.Value;
                            }
                            else if (arg.Value is PreParsedGraphQLVariableValue graphqlVariable)
                            {
                                if (!variables.TryGetValue(graphqlVariable.Name, out var result))
                                {
                                    throw new ArgumentException(
                                        $"{graphqlVariable.Name} is not specified"
                                    );
                                }
                                if (result is null)
                                {
                                    throw new ArgumentNullException(
                                        "solutionConfigurationPlatform is null"
                                    );
                                }
                                solutionConfigurationPlatform = ((JsonElement)result).GetString();
                            }
                        }
                    }
                    jsonNode[fieldSelection.Alias ?? fieldSelection.Name] =
                        AddSolutionConfigurationPlatform(
                            solutionFile,
                            path,
                            solutionConfigurationPlatform
                                ?? throw new ArgumentNullException(
                                    "solutionConfigurationPlatform is null"
                                )
                        );
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return jsonNode;
    }

    public partial string AddProject(
        string? solutionFile,
        string projectFilePath,
        string projectTypeUuid,
        IReadOnlyList<string>? projectConfigurationPlatforms
    );

    public partial string AddSolutionConfigurationPlatform(
        string? solutionFile,
        string? path,
        string solutionConfigurationPlatform
    );
}

#pragma warning disable CS9113 // Parameter is unread.
public partial class GraphQLServer(IServiceProvider services)
#pragma warning restore CS9113 // Parameter is unread.
{
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
        Justification = "<Pending>"
    )]
    public string ProcessGraphQLRequest(string request)
    {
        var parsedRequest = PreParsedGraphQLRequest.FromJsonString(request);

        JsonNode? jsonNode = null;
        var errors = new JsonArray();

        if (parsedRequest is not null)
        {
            if (parsedRequest.Query.Operation.OperationType == PreParsedGraphQLOperationType.Query)
            {
                var obj = new Query(services);
                jsonNode = obj.Resolve(
                    parsedRequest.Variables ?? new(),
                    parsedRequest.Query.Operation.Selections
                );
            }
            if (
                parsedRequest.Query.Operation.OperationType
                == PreParsedGraphQLOperationType.Mutation
            )
            {
                var obj = new Mutation(services);
                jsonNode = obj.Resolve(
                    parsedRequest.Variables ?? new(),
                    parsedRequest.Query.Operation.Selections
                );
            }
        }
        else
        {
            errors.Add("Failed to parse request");
        }

        if (jsonNode is null)
        {
            errors.Add("No result");
        }

        var result = new JsonObject();
        result["errors"] = errors;
        result["data"] = jsonNode;

        var resultJson = result.ToJsonString();
        return resultJson;
    }
}
