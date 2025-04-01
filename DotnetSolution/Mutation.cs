using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodegenBot;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSolution;

public partial class Mutation
{
    private readonly ListOfSolutions _listOfSolutions = services.GetRequiredService<ListOfSolutions>();
    private readonly IGraphQLClient _graphqlClient = services.GetRequiredService<IGraphQLClient>();
    
    public partial string AddProject(string? solutionFile, string projectFilePath, string projectTypeUuid, IReadOnlyList<string>? projectConfigurationPlatforms)
    {
        solutionFile ??= _listOfSolutions.GetDefaultSolution(projectFilePath);
        if (solutionFile is null)
        {
            solutionFile = _graphqlClient.GetConfiguration().Configuration.DefaultSolutionPath;
            if (!solutionFile.EndsWith(".sln"))
            {
                solutionFile = solutionFile + ".sln";
            }
        }
        if (solutionFile is null)
        {
            _graphqlClient.Log(LogSeverity.CRITICAL,
                "Cannot pick a default solution for project {Project} because no solution exists in the project's directory or any of the project's ancestor directories. Try specifying a defaultSolutionFile in the configuration for bot://hub/dotnet/solution.",
                [projectFilePath]);
            throw new InvalidOperationException("Cannot pick a default solution for project " + projectFilePath + " because no such solution exists");
        }

        if (!_listOfSolutions.ContainsSolution(solutionFile))
        {
            _listOfSolutions.AddSolution(solutionFile);

            var solutionUuid = _graphqlClient.GetProjectUuid(solutionFile).RandomUuid.ToUpper();

            _graphqlClient.AddFile(solutionFile,
                $$"""

                  Microsoft Visual Studio Solution File, Format Version 12.00
                  # Visual Studio Version 17
                  VisualStudioVersion = 17.5.002.0
                  MinimumVisualStudioVersion = 10.0.40219.1
                  {{CaretRef.New(out var solutionProjects, new CaretTag("location", ".sln/Project"), new CaretTag("path", solutionFile))}}
                  Global
                    GlobalSection(SolutionConfigurationPlatforms) = preSolution
                    {{CaretRef.New(out var solutionConfigurationPlatforms, new CaretTag("location", ".sln/SolutionConfigurationPlatforms"), new CaretTag("path", solutionFile))}}
                    EndGlobalSection
                    GlobalSection(ProjectConfigurationPlatforms) = postSolution
                    {{CaretRef.New(out var solutionProjectConfigurationPlatforms, new CaretTag("location", ".sln/ProjectConfigurationPlatforms"), new CaretTag("path", solutionFile))}}
                    EndGlobalSection
                    GlobalSection(SolutionProperties) = preSolution
                      	HideSolutionNode = FALSE
                    EndGlobalSection
                    GlobalSection(ExtensibilityGlobals) = postSolution
                      	SolutionGuid = {{{solutionUuid}}}
                    EndGlobalSection
                  EndGlobal

                  """
            );
        }

        var projectUuid = _graphqlClient.GetProjectUuid(projectFilePath).RandomUuid.ToUpper();
        
        _graphqlClient.AddTextByTags(
        [
            new CaretTagInput() { Name = "location", Value = ".sln/Project" }
        ], $$"""
             Project("{{{projectTypeUuid}}}") = "{{Path.GetFileName(projectFilePath)}}", "{{projectFilePath.Trim('/').Replace("/", "\\")}}", "{{{projectUuid}}}"
             EndProject

             """);

        projectConfigurationPlatforms ??= _listOfSolutions.GetSolutionConfigurations(solutionFile).ToList();
        foreach (var projectConfigurationPlatform in projectConfigurationPlatforms)
        {
            if (projectConfigurationPlatform.Contains("="))
            {
                var secondHalf = projectConfigurationPlatform.Split('=')[1].Trim();
                AddSolutionConfigurationPlatform(solutionFile, secondHalf);
                _graphqlClient.AddTextByTags(
                [
                    new CaretTagInput() { Name = "location", Value = ".sln/ProjectConfigurationPlatforms" },
                    new CaretTagInput() { Name = "path", Value = solutionFile },
                ], $$"""
                     {{{projectUuid}}}.{{projectConfigurationPlatform}}

                     """);
            }
            else
            {
                AddSolutionConfigurationPlatform(solutionFile, projectConfigurationPlatform);
                _graphqlClient.AddTextByTags(
                [
                    new CaretTagInput() { Name = "location", Value = ".sln/ProjectConfigurationPlatforms" },
                    new CaretTagInput() { Name = "path", Value = solutionFile },
                ], $$"""
                     {{{projectUuid}}}.{{projectConfigurationPlatform}}.ActiveCfg = {{projectConfigurationPlatform}}
                     {{{projectUuid}}}.{{projectConfigurationPlatform}}.Build.0 = {{projectConfigurationPlatform}}

                     """);
            }
        }
        
        return solutionFile;
    }

    public partial string AddSolutionConfigurationPlatform(string? solutionFile, string? path, string solutionConfigurationPlatform)
    {
        solutionFile ??= _listOfSolutions.GetDefaultSolution(path ?? "");
        if (solutionFile is null)
        {
            _graphqlClient.Log(LogSeverity.CRITICAL,
                "Cannot pick a default solution for path {Path} because no solution exists in the path or any of the path's ancestor directories",
                [path ?? "null"]);
            throw new InvalidOperationException("Cannot pick a default solution for path " + path + " because no such solution exists");
        }

        AddSolutionConfigurationPlatform(solutionFile, solutionConfigurationPlatform);
        return solutionFile;
    }

    private void AddSolutionConfigurationPlatform(string solutionFile, string solutionConfigurationPlatform)
    {
        _listOfSolutions.AddSolutionConfiguration(solutionFile, solutionConfigurationPlatform);
        _graphqlClient.AddKeyedTextByTags(solutionConfigurationPlatform,
            [
                new CaretTagInput() { Name = "location", Value = ".sln/SolutionConfigurationPlatforms" },
                new CaretTagInput() { Name = "path", Value = solutionFile },
            ],
            $"""
             {solutionConfigurationPlatform} = {solutionConfigurationPlatform}

             """);
    }
}