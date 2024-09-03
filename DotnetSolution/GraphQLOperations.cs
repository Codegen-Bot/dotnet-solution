using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodegenBot;

namespace DotnetSolution;

public static partial class GraphQLOperations
{
    public static GetConfigurationData GetConfiguration()
    {
        var request = new GraphQLRequest<GetConfigurationVariables>
        {
            Query =
                @"
            query GetConfiguration {
  configuration {
    outputPath
  }
},
            ",
            OperationName = "GetConfiguration",
            Variables = new GetConfigurationVariables() { },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetConfigurationData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static GetFilesData GetFiles(List<string>? whitelist, List<string>? blacklist)
    {
        var request = new GraphQLRequest<GetFilesVariables>
        {
            Query =
                @"
            query GetFiles($whitelist: [String!]!, $blacklist: [String!]!) {
  files(whitelist: $whitelist, blacklist: $blacklist) {
    path
    kind
  }
},
            ",
            OperationName = "GetFiles",
            Variables = new GetFilesVariables() { Whitelist = whitelist, Blacklist = blacklist },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetFilesData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static GetFileContentsData GetFileContents(string textFilePath)
    {
        var request = new GraphQLRequest<GetFileContentsVariables>
        {
            Query =
                @"
            query GetFileContents($textFilePath: String!) {
  readTextFile(textFilePath: $textFilePath)
},
            ",
            OperationName = "GetFileContents",
            Variables = new GetFileContentsVariables() { TextFilePath = textFilePath },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetFileContentsData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static AddFileData AddFile(string filePath, string textAndCarets)
    {
        var request = new GraphQLRequest<AddFileVariables>
        {
            Query =
                @"
            mutation AddFile($filePath: String!, $textAndCarets: String!) {
  addFile(filePath: $filePath, textAndCarets: $textAndCarets) {
    id
  }
},
            ",
            OperationName = "AddFile",
            Variables = new AddFileVariables()
            {
                FilePath = filePath,
                TextAndCarets = textAndCarets,
            },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddFileData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static AddTextData AddText(string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextVariables>
        {
            Query =
                @"
            mutation AddText($caretId: String!, $textAndCarets: String!) {
  addText(caretId: $caretId, textAndCarets: $textAndCarets) {
    id
  }
},
            ",
            OperationName = "AddText",
            Variables = new AddTextVariables() { CaretId = caretId, TextAndCarets = textAndCarets },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static AddKeyedTextData AddKeyedText(string key, string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddKeyedTextVariables>
        {
            Query =
                @"
            mutation AddKeyedText($key: String!, $caretId: String!, $textAndCarets: String!) {
  addKeyedText(key: $key, caretId: $caretId, textAndCarets: $textAndCarets) {
    id
  }
},
            ",
            OperationName = "AddKeyedText",
            Variables = new AddKeyedTextVariables()
            {
                Key = key,
                CaretId = caretId,
                TextAndCarets = textAndCarets,
            },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static AddTextByTagsData AddTextByTags(List<CaretTagInput>? tags, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextByTagsVariables>
        {
            Query =
                @"
            mutation AddTextByTags($tags: [CaretTagInput!]!, $textAndCarets: String!) {
  addTextByTags(tags: $tags, textAndCarets: $textAndCarets) {
    id
  }
},
            ",
            OperationName = "AddTextByTags",
            Variables = new AddTextByTagsVariables() { Tags = tags, TextAndCarets = textAndCarets },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextByTagsData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static AddKeyedTextByTagsData AddKeyedTextByTags(
        string key,
        List<CaretTagInput>? tags,
        string textAndCarets
    )
    {
        var request = new GraphQLRequest<AddKeyedTextByTagsVariables>
        {
            Query =
                @"
            mutation AddKeyedTextByTags($key: String!, $tags: [CaretTagInput!]!, $textAndCarets: String!) {
  addKeyedTextByTags(key: $key, tags: $tags, textAndCarets: $textAndCarets) {
    id
  }
},
            ",
            OperationName = "AddKeyedTextByTags",
            Variables = new AddKeyedTextByTagsVariables()
            {
                Key = key,
                Tags = tags,
                TextAndCarets = textAndCarets,
            },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextByTagsData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }

    public static LogData Log(LogSeverity severity, string message, List<string>? arguments)
    {
        var request = new GraphQLRequest<LogVariables>
        {
            Query =
                @"
            mutation Log($severity: LogSeverity!, $message: String!, $arguments: [String!]) {
  log(severity: $severity, message: $message, arguments: $arguments)
},
            ",
            OperationName = "Log",
            Variables = new LogVariables()
            {
                Severity = severity,
                Message = message,
                Arguments = arguments,
            },
        };

        var response = CodegenBotHost.GraphQL(request);
        var result = JsonSerializer.Deserialize<GraphQLResponse<LogData>>(
            response,
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        return result?.Data;
    }
}

public class GraphQLResponse<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("errors")]
    public List<GraphQLError>? Errors { get; set; }
}

public class GraphQLError
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}

public enum FileKind
{
    BINARY,
    TEXT,
}

public enum FileVersion
{
    GENERATED,
    HEAD,
}

public enum LogSeverity
{
    TRACE,
    DEBUG,
    INFORMATION,
    WARNING,
    ERROR,
    CRITICAL,
}

public class CaretTagInput
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class GetConfigurationData
{
    [JsonPropertyName("configuration")]
    public GetConfiguration Configuration { get; set; }
}

public class GetConfigurationVariables { }

public class GetConfiguration
{
    [JsonPropertyName("outputPath")]
    public string OutputPath { get; set; }
}

public class GetFilesData
{
    [JsonPropertyName("files")]
    public List<GetFiles>? Files { get; set; }
}

public class GetFilesVariables
{
    [JsonPropertyName("whitelist")]
    public List<string>? Whitelist { get; set; }

    [JsonPropertyName("blacklist")]
    public List<string>? Blacklist { get; set; }
}

public class GetFiles
{
    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("kind")]
    public FileKind Kind { get; set; }
}

public class GetFileContentsData
{
    [JsonPropertyName("readTextFile")]
    public string ReadTextFile { get; set; }
}

public class GetFileContentsVariables
{
    [JsonPropertyName("textFilePath")]
    public string TextFilePath { get; set; }
}

public class AddFileData
{
    [JsonPropertyName("addFile")]
    public AddFile AddFile { get; set; }
}

public class AddFileVariables
{
    [JsonPropertyName("filePath")]
    public string FilePath { get; set; }

    [JsonPropertyName("textAndCarets")]
    public string TextAndCarets { get; set; }
}

public class AddFile
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class AddTextData
{
    [JsonPropertyName("addText")]
    public AddText AddText { get; set; }
}

public class AddTextVariables
{
    [JsonPropertyName("caretId")]
    public string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public string TextAndCarets { get; set; }
}

public class AddText
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class AddKeyedTextData
{
    [JsonPropertyName("addKeyedText")]
    public AddKeyedText AddKeyedText { get; set; }
}

public class AddKeyedTextVariables
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("caretId")]
    public string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public string TextAndCarets { get; set; }
}

public class AddKeyedText
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class AddTextByTagsData
{
    [JsonPropertyName("addTextByTags")]
    public List<AddTextByTags>? AddTextByTags { get; set; }
}

public class AddTextByTagsVariables
{
    [JsonPropertyName("tags")]
    public List<CaretTagInput>? Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public string TextAndCarets { get; set; }
}

public class AddTextByTags
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class AddKeyedTextByTagsData
{
    [JsonPropertyName("addKeyedTextByTags")]
    public List<AddKeyedTextByTags>? AddKeyedTextByTags { get; set; }
}

public class AddKeyedTextByTagsVariables
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("tags")]
    public List<CaretTagInput>? Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public string TextAndCarets { get; set; }
}

public class AddKeyedTextByTags
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class LogData
{
    [JsonPropertyName("log")]
    public string Log { get; set; }
}

public class LogVariables
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("severity")]
    public LogSeverity Severity { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("arguments")]
    public List<string>? Arguments { get; set; }
}
