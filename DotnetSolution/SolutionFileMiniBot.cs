using CodegenBot;

namespace DotnetSolution;

public class SolutionFileMiniBot : IMiniBot
{
    public void Execute()
    {
	    var configuration = GraphQLOperations.GetConfiguration().Configuration;

        GraphQLOperations.AddFile(configuration.OutputPath,
            $$"""

              Microsoft Visual Studio Solution File, Format Version 12.00
              # Visual Studio Version 17
              VisualStudioVersion = 17.5.002.0
              MinimumVisualStudioVersion = 10.0.40219.1
              {{CaretRef.New(out var solutionProjects, new CaretTag("location", ".sln/Project"))}}
              Global
                GlobalSection(SolutionConfigurationPlatforms) = preSolution
                {{CaretRef.New(out var solutionConfigurationPlatforms, new CaretTag("location", ".sln/SolutionConfigurationPlatforms"))}}
                EndGlobalSection
                GlobalSection(ProjectConfigurationPlatforms) = postSolution
                {{CaretRef.New(out var solutionProjectConfigurationPlatforms, new CaretTag("location", ".sln/ProjectConfigurationPlatforms"))}}
                EndGlobalSection
                GlobalSection(SolutionProperties) = preSolution
                  	HideSolutionNode = FALSE
                EndGlobalSection
                GlobalSection(ExtensibilityGlobals) = postSolution
                  	SolutionGuid = {20E6FD03-9002-4EBA-ABF2-9DDE2C488842}
                EndGlobalSection
              EndGlobal

              """
        );

        AddSolutionConfigurationPlatform("Debug|Any CPU");
        AddSolutionConfigurationPlatform("Debug|ARM64");
        AddSolutionConfigurationPlatform("Debug|x64");
        AddSolutionConfigurationPlatform("Debug|x86");
        AddSolutionConfigurationPlatform("Release|Any CPU");
        AddSolutionConfigurationPlatform("Release|ARM64");
        AddSolutionConfigurationPlatform("Release|x64");
        AddSolutionConfigurationPlatform("Release|x86");
    }

    private static void AddSolutionConfigurationPlatform(string solutionConfigurationPlatform)
    {
	    GraphQLOperations.AddKeyedTextByTags(solutionConfigurationPlatform,
		    [new CaretTagInput() { Name = "location", Value = ".sln/SolutionConfigurationPlatforms" }],
		    $"""
		    {solutionConfigurationPlatform} = {solutionConfigurationPlatform}

		    """);
    }
}