using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CodegenBot;

#nullable enable

namespace DotnetSolution;

public partial class GraphQLResponse<TData>
{
    [JsonPropertyName("data")]
    public TData? Data { get; set; }

    [JsonPropertyName("errors")]
    public List<GraphQLError>? Errors { get; set; }
}

public partial class GraphQLError
{
    [JsonPropertyName("message")]
    public required string Message { get; set; }
}

public partial class WasmGraphQLClient(ICodegenBotImports imports) : IGraphQLClient
{
    public MarkAsReadyData MarkAsReady(int processId, string? providedApiUrl)
    {
        var request = new GraphQLRequest<MarkAsReadyVariables>
        {
            Query = """
                query MarkAsReady($processId: Int!, $providedApiUrl: String) {
                  markAsReady(processId: $processId, providedApiUrl: $providedApiUrl)
                }
                """,
            OperationName = "MarkAsReady",
            Variables = new MarkAsReadyVariables()
            {
                ProcessId = processId,
                ProvidedApiUrl = providedApiUrl,
            },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestMarkAsReadyVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<MarkAsReadyData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseMarkAsReadyData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request MarkAsReady.");
    }

    public GetFilesData GetFiles(IReadOnlyList<string> whitelist, IReadOnlyList<string> blacklist)
    {
        var request = new GraphQLRequest<GetFilesVariables>
        {
            Query = """
                query GetFiles($whitelist: [String!]!, $blacklist: [String!]!) {
                  files(whitelist: $whitelist, blacklist: $blacklist) {
                    path
                    kind
                  }
                }
                """,
            OperationName = "GetFiles",
            Variables = new GetFilesVariables() { Whitelist = whitelist, Blacklist = blacklist },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetFilesVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetFilesData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetFilesData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request GetFiles.");
    }

    public ReadTextFileData ReadTextFile(string textFilePath)
    {
        var request = new GraphQLRequest<ReadTextFileVariables>
        {
            Query = """
                query ReadTextFile($textFilePath: String!) {
                  readTextFile(textFilePath: $textFilePath)
                }
                """,
            OperationName = "ReadTextFile",
            Variables = new ReadTextFileVariables() { TextFilePath = textFilePath },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestReadTextFileVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<ReadTextFileData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseReadTextFileData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request ReadTextFile.");
    }

    public AddFileData AddFile(string filePath, string textAndCarets)
    {
        var request = new GraphQLRequest<AddFileVariables>
        {
            Query = """
                mutation AddFile($filePath: String!, $textAndCarets: String!) {
                  addFile(filePath: $filePath, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddFile",
            Variables = new AddFileVariables()
            {
                FilePath = filePath,
                TextAndCarets = textAndCarets,
            },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddFileVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddFileData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddFileData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddFile.");
    }

    public AddTextData AddText(string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextVariables>
        {
            Query = """
                mutation AddText($caretId: String!, $textAndCarets: String!) {
                  addText(caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddText",
            Variables = new AddTextVariables() { CaretId = caretId, TextAndCarets = textAndCarets },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddTextVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddText.");
    }

    public AddKeyedTextData AddKeyedText(string key, string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddKeyedTextVariables>
        {
            Query = """
                mutation AddKeyedText($key: String!, $caretId: String!, $textAndCarets: String!) {
                  addKeyedText(key: $key, caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddKeyedText",
            Variables = new AddKeyedTextVariables()
            {
                Key = key,
                CaretId = caretId,
                TextAndCarets = textAndCarets,
            },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddKeyedTextVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddKeyedTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddKeyedText.");
    }

    public AddSimpleKeyedTextData AddSimpleKeyedText(string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddSimpleKeyedTextVariables>
        {
            Query = """
                mutation AddSimpleKeyedText($caretId: String!, $textAndCarets: String!) {
                  addKeyedText(key: $textAndCarets, caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddSimpleKeyedText",
            Variables = new AddSimpleKeyedTextVariables()
            {
                CaretId = caretId,
                TextAndCarets = textAndCarets,
            },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddSimpleKeyedTextVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddSimpleKeyedTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddSimpleKeyedTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request AddSimpleKeyedText."
            );
    }

    public AddTextByTagsData AddTextByTags(IReadOnlyList<CaretTagInput> tags, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextByTagsVariables>
        {
            Query = """
                mutation AddTextByTags($tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addTextByTags(tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddTextByTags",
            Variables = new AddTextByTagsVariables() { Tags = tags, TextAndCarets = textAndCarets },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddTextByTagsVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddTextByTags.");
    }

    public AddKeyedTextByTagsData AddKeyedTextByTags(
        string key,
        IReadOnlyList<CaretTagInput> tags,
        string textAndCarets
    )
    {
        var request = new GraphQLRequest<AddKeyedTextByTagsVariables>
        {
            Query = """
                mutation AddKeyedTextByTags($key: String!, $tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addKeyedTextByTags(key: $key, tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddKeyedTextByTags",
            Variables = new AddKeyedTextByTagsVariables()
            {
                Key = key,
                Tags = tags,
                TextAndCarets = textAndCarets,
            },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddKeyedTextByTagsVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddKeyedTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request AddKeyedTextByTags."
            );
    }

    public AddSimpleKeyedTextByTagsData AddSimpleKeyedTextByTags(
        IReadOnlyList<CaretTagInput> tags,
        string textAndCarets
    )
    {
        var request = new GraphQLRequest<AddSimpleKeyedTextByTagsVariables>
        {
            Query = """
                mutation AddSimpleKeyedTextByTags($tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addKeyedTextByTags(key: $textAndCarets, tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddSimpleKeyedTextByTags",
            Variables = new AddSimpleKeyedTextByTagsVariables()
            {
                Tags = tags,
                TextAndCarets = textAndCarets,
            },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext
                .Default
                .GraphQLRequestAddSimpleKeyedTextByTagsVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddSimpleKeyedTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddSimpleKeyedTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request AddSimpleKeyedTextByTags."
            );
    }

    public LogData Log(LogSeverity severity, string message, IReadOnlyList<string> arguments)
    {
        var request = new GraphQLRequest<LogVariables>
        {
            Query = """
                mutation Log($severity: LogSeverity!, $message: String!, $arguments: [String!]) {
                  log(severity: $severity, message: $message, arguments: $arguments)
                }
                """,
            OperationName = "Log",
            Variables = new LogVariables()
            {
                Severity = severity,
                Message = message,
                Arguments = arguments,
            },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestLogVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<LogData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseLogData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request Log.");
    }

    public GetConfigurationData GetConfiguration()
    {
        var request = new GraphQLRequest<GetConfigurationVariables>
        {
            Query = """
                query GetConfiguration {
                  configuration {
                    outputPath
                  }
                }
                """,
            OperationName = "GetConfiguration",
            Variables = new GetConfigurationVariables() { },
        };

        var response = imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetConfigurationVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetConfigurationData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetConfigurationData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request GetConfiguration."
            );
    }
}

public partial class SyncHttpGraphQLClient(HttpClient httpClient, string uri) : IGraphQLClient
{
    public MarkAsReadyData MarkAsReady(int processId, string? providedApiUrl)
    {
        var request = new GraphQLRequest<MarkAsReadyVariables>
        {
            Query = """
                query MarkAsReady($processId: Int!, $providedApiUrl: String) {
                  markAsReady(processId: $processId, providedApiUrl: $providedApiUrl)
                }
                """,
            OperationName = "MarkAsReady",
            Variables = new MarkAsReadyVariables()
            {
                ProcessId = processId,
                ProvidedApiUrl = providedApiUrl,
            },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestMarkAsReadyVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<MarkAsReadyData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseMarkAsReadyData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request MarkAsReady.");
    }

    public GetFilesData GetFiles(IReadOnlyList<string> whitelist, IReadOnlyList<string> blacklist)
    {
        var request = new GraphQLRequest<GetFilesVariables>
        {
            Query = """
                query GetFiles($whitelist: [String!]!, $blacklist: [String!]!) {
                  files(whitelist: $whitelist, blacklist: $blacklist) {
                    path
                    kind
                  }
                }
                """,
            OperationName = "GetFiles",
            Variables = new GetFilesVariables() { Whitelist = whitelist, Blacklist = blacklist },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetFilesVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetFilesData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetFilesData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request GetFiles.");
    }

    public ReadTextFileData ReadTextFile(string textFilePath)
    {
        var request = new GraphQLRequest<ReadTextFileVariables>
        {
            Query = """
                query ReadTextFile($textFilePath: String!) {
                  readTextFile(textFilePath: $textFilePath)
                }
                """,
            OperationName = "ReadTextFile",
            Variables = new ReadTextFileVariables() { TextFilePath = textFilePath },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestReadTextFileVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<ReadTextFileData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseReadTextFileData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request ReadTextFile.");
    }

    public AddFileData AddFile(string filePath, string textAndCarets)
    {
        var request = new GraphQLRequest<AddFileVariables>
        {
            Query = """
                mutation AddFile($filePath: String!, $textAndCarets: String!) {
                  addFile(filePath: $filePath, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddFile",
            Variables = new AddFileVariables()
            {
                FilePath = filePath,
                TextAndCarets = textAndCarets,
            },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddFileVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddFileData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddFileData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddFile.");
    }

    public AddTextData AddText(string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextVariables>
        {
            Query = """
                mutation AddText($caretId: String!, $textAndCarets: String!) {
                  addText(caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddText",
            Variables = new AddTextVariables() { CaretId = caretId, TextAndCarets = textAndCarets },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddTextVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddText.");
    }

    public AddKeyedTextData AddKeyedText(string key, string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddKeyedTextVariables>
        {
            Query = """
                mutation AddKeyedText($key: String!, $caretId: String!, $textAndCarets: String!) {
                  addKeyedText(key: $key, caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddKeyedText",
            Variables = new AddKeyedTextVariables()
            {
                Key = key,
                CaretId = caretId,
                TextAndCarets = textAndCarets,
            },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddKeyedTextVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddKeyedTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddKeyedText.");
    }

    public AddSimpleKeyedTextData AddSimpleKeyedText(string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddSimpleKeyedTextVariables>
        {
            Query = """
                mutation AddSimpleKeyedText($caretId: String!, $textAndCarets: String!) {
                  addKeyedText(key: $textAndCarets, caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddSimpleKeyedText",
            Variables = new AddSimpleKeyedTextVariables()
            {
                CaretId = caretId,
                TextAndCarets = textAndCarets,
            },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddSimpleKeyedTextVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddSimpleKeyedTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddSimpleKeyedTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request AddSimpleKeyedText."
            );
    }

    public AddTextByTagsData AddTextByTags(IReadOnlyList<CaretTagInput> tags, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextByTagsVariables>
        {
            Query = """
                mutation AddTextByTags($tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addTextByTags(tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddTextByTags",
            Variables = new AddTextByTagsVariables() { Tags = tags, TextAndCarets = textAndCarets },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddTextByTagsVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddTextByTags.");
    }

    public AddKeyedTextByTagsData AddKeyedTextByTags(
        string key,
        IReadOnlyList<CaretTagInput> tags,
        string textAndCarets
    )
    {
        var request = new GraphQLRequest<AddKeyedTextByTagsVariables>
        {
            Query = """
                mutation AddKeyedTextByTags($key: String!, $tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addKeyedTextByTags(key: $key, tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddKeyedTextByTags",
            Variables = new AddKeyedTextByTagsVariables()
            {
                Key = key,
                Tags = tags,
                TextAndCarets = textAndCarets,
            },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddKeyedTextByTagsVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddKeyedTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request AddKeyedTextByTags."
            );
    }

    public AddSimpleKeyedTextByTagsData AddSimpleKeyedTextByTags(
        IReadOnlyList<CaretTagInput> tags,
        string textAndCarets
    )
    {
        var request = new GraphQLRequest<AddSimpleKeyedTextByTagsVariables>
        {
            Query = """
                mutation AddSimpleKeyedTextByTags($tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addKeyedTextByTags(key: $textAndCarets, tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddSimpleKeyedTextByTags",
            Variables = new AddSimpleKeyedTextByTagsVariables()
            {
                Tags = tags,
                TextAndCarets = textAndCarets,
            },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext
                .Default
                .GraphQLRequestAddSimpleKeyedTextByTagsVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddSimpleKeyedTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddSimpleKeyedTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request AddSimpleKeyedTextByTags."
            );
    }

    public LogData Log(LogSeverity severity, string message, IReadOnlyList<string> arguments)
    {
        var request = new GraphQLRequest<LogVariables>
        {
            Query = """
                mutation Log($severity: LogSeverity!, $message: String!, $arguments: [String!]) {
                  log(severity: $severity, message: $message, arguments: $arguments)
                }
                """,
            OperationName = "Log",
            Variables = new LogVariables()
            {
                Severity = severity,
                Message = message,
                Arguments = arguments,
            },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestLogVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<LogData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseLogData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request Log.");
    }

    public GetConfigurationData GetConfiguration()
    {
        var request = new GraphQLRequest<GetConfigurationVariables>
        {
            Query = """
                query GetConfiguration {
                  configuration {
                    outputPath
                  }
                }
                """,
            OperationName = "GetConfiguration",
            Variables = new GetConfigurationVariables() { },
        };

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Version = HttpVersion.Version11,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        requestMessage.Content = JsonContent.Create(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetConfigurationVariables
        );
        var responseMessage = httpClient.Send(requestMessage);
        var responseStream = responseMessage.Content.ReadAsStream();
        using var reader = new StreamReader(responseStream);
        var response = reader.ReadToEnd();

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetConfigurationData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetConfigurationData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request GetConfiguration."
            );
    }
}

public partial interface IGraphQLClient
{
    MarkAsReadyData MarkAsReady(int processId, string? providedApiUrl);

    GetFilesData GetFiles(IReadOnlyList<string> whitelist, IReadOnlyList<string> blacklist);

    ReadTextFileData ReadTextFile(string textFilePath);

    AddFileData AddFile(string filePath, string textAndCarets);

    AddTextData AddText(string caretId, string textAndCarets);

    AddKeyedTextData AddKeyedText(string key, string caretId, string textAndCarets);

    AddSimpleKeyedTextData AddSimpleKeyedText(string caretId, string textAndCarets);

    AddTextByTagsData AddTextByTags(IReadOnlyList<CaretTagInput> tags, string textAndCarets);

    AddKeyedTextByTagsData AddKeyedTextByTags(
        string key,
        IReadOnlyList<CaretTagInput> tags,
        string textAndCarets
    );

    AddSimpleKeyedTextByTagsData AddSimpleKeyedTextByTags(
        IReadOnlyList<CaretTagInput> tags,
        string textAndCarets
    );

    LogData Log(LogSeverity severity, string message, IReadOnlyList<string> arguments);

    GetConfigurationData GetConfiguration();
}

[JsonSerializable(typeof(GraphQLRequest<MarkAsReadyVariables>))]
[JsonSerializable(typeof(GraphQLResponse<MarkAsReadyData>))]
[JsonSerializable(typeof(MarkAsReadyData))]
[JsonSerializable(typeof(GraphQLRequest<GetFilesVariables>))]
[JsonSerializable(typeof(GraphQLResponse<GetFilesData>))]
[JsonSerializable(typeof(GetFilesData))]
[JsonSerializable(typeof(GetFilesFile))]
[JsonSerializable(typeof(GraphQLRequest<ReadTextFileVariables>))]
[JsonSerializable(typeof(GraphQLResponse<ReadTextFileData>))]
[JsonSerializable(typeof(ReadTextFileData))]
[JsonSerializable(typeof(GraphQLRequest<AddFileVariables>))]
[JsonSerializable(typeof(GraphQLResponse<AddFileData>))]
[JsonSerializable(typeof(AddFileData))]
[JsonSerializable(typeof(AddFileAddFile))]
[JsonSerializable(typeof(GraphQLRequest<AddTextVariables>))]
[JsonSerializable(typeof(GraphQLResponse<AddTextData>))]
[JsonSerializable(typeof(AddTextData))]
[JsonSerializable(typeof(AddTextAddText))]
[JsonSerializable(typeof(GraphQLRequest<AddKeyedTextVariables>))]
[JsonSerializable(typeof(GraphQLResponse<AddKeyedTextData>))]
[JsonSerializable(typeof(AddKeyedTextData))]
[JsonSerializable(typeof(AddKeyedTextAddKeyedText))]
[JsonSerializable(typeof(GraphQLRequest<AddSimpleKeyedTextVariables>))]
[JsonSerializable(typeof(GraphQLResponse<AddSimpleKeyedTextData>))]
[JsonSerializable(typeof(AddSimpleKeyedTextData))]
[JsonSerializable(typeof(AddSimpleKeyedTextAddKeyedText))]
[JsonSerializable(typeof(GraphQLRequest<AddTextByTagsVariables>))]
[JsonSerializable(typeof(GraphQLResponse<AddTextByTagsData>))]
[JsonSerializable(typeof(AddTextByTagsData))]
[JsonSerializable(typeof(AddTextByTagsAddTextByTag))]
[JsonSerializable(typeof(GraphQLRequest<AddKeyedTextByTagsVariables>))]
[JsonSerializable(typeof(GraphQLResponse<AddKeyedTextByTagsData>))]
[JsonSerializable(typeof(AddKeyedTextByTagsData))]
[JsonSerializable(typeof(AddKeyedTextByTagsAddKeyedTextByTag))]
[JsonSerializable(typeof(GraphQLRequest<AddSimpleKeyedTextByTagsVariables>))]
[JsonSerializable(typeof(GraphQLResponse<AddSimpleKeyedTextByTagsData>))]
[JsonSerializable(typeof(AddSimpleKeyedTextByTagsData))]
[JsonSerializable(typeof(AddSimpleKeyedTextByTagsAddKeyedTextByTag))]
[JsonSerializable(typeof(GraphQLRequest<LogVariables>))]
[JsonSerializable(typeof(GraphQLResponse<LogData>))]
[JsonSerializable(typeof(LogData))]
[JsonSerializable(typeof(GraphQLRequest<GetConfigurationVariables>))]
[JsonSerializable(typeof(GraphQLResponse<GetConfigurationData>))]
[JsonSerializable(typeof(GetConfigurationData))]
[JsonSerializable(typeof(GetConfigurationConfiguration))]
[JsonSourceGenerationOptions(WriteIndented = true)]
public partial class GraphQLClientJsonSerializerContext : JsonSerializerContext;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum FileKind
{
    [EnumMember(Value = "BINARY")]
    BINARY,

    [EnumMember(Value = "TEXT")]
    TEXT,
}

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum FileVersion
{
    [EnumMember(Value = "GENERATED")]
    GENERATED,

    [EnumMember(Value = "HEAD")]
    HEAD,
}

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum LogSeverity
{
    [EnumMember(Value = "TRACE")]
    TRACE,

    [EnumMember(Value = "DEBUG")]
    DEBUG,

    [EnumMember(Value = "INFORMATION")]
    INFORMATION,

    [EnumMember(Value = "WARNING")]
    WARNING,

    [EnumMember(Value = "ERROR")]
    ERROR,

    [EnumMember(Value = "CRITICAL")]
    CRITICAL,
}

public partial class CaretTagInput
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}

public partial class MarkAsReadyVariables
{
    [JsonPropertyName("processId")]
    public required int ProcessId { get; set; }

    [JsonPropertyName("providedApiUrl")]
    public required string? ProvidedApiUrl { get; set; }
}

public partial class MarkAsReadyData
{
    [JsonPropertyName("markAsReady")]
    public required bool MarkAsReady { get; set; }
}

public partial class GetFilesVariables
{
    [JsonPropertyName("whitelist")]
    public required IReadOnlyList<string> Whitelist { get; set; }

    [JsonPropertyName("blacklist")]
    public required IReadOnlyList<string> Blacklist { get; set; }
}

public partial class GetFilesData
{
    [JsonPropertyName("files")]
    public required List<GetFilesFile> Files { get; set; }
}

public partial class GetFilesFile
{
    [JsonPropertyName("path")]
    public required string Path { get; set; }

    [JsonPropertyName("kind")]
    public required FileKind Kind { get; set; }
}

public partial class ReadTextFileVariables
{
    [JsonPropertyName("textFilePath")]
    public required string TextFilePath { get; set; }
}

public partial class ReadTextFileData
{
    [JsonPropertyName("readTextFile")]
    public string? ReadTextFile { get; set; }
}

public partial class AddFileVariables
{
    [JsonPropertyName("filePath")]
    public required string FilePath { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddFileData
{
    [JsonPropertyName("addFile")]
    public required AddFileAddFile AddFile { get; set; }
}

public partial class AddFileAddFile
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddTextVariables
{
    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddTextData
{
    [JsonPropertyName("addText")]
    public required AddTextAddText AddText { get; set; }
}

public partial class AddTextAddText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddKeyedTextVariables
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddKeyedTextData
{
    [JsonPropertyName("addKeyedText")]
    public required AddKeyedTextAddKeyedText AddKeyedText { get; set; }
}

public partial class AddKeyedTextAddKeyedText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddSimpleKeyedTextVariables
{
    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddSimpleKeyedTextData
{
    [JsonPropertyName("addKeyedText")]
    public required AddSimpleKeyedTextAddKeyedText AddKeyedText { get; set; }
}

public partial class AddSimpleKeyedTextAddKeyedText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddTextByTagsVariables
{
    [JsonPropertyName("tags")]
    public required IReadOnlyList<CaretTagInput> Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddTextByTagsData
{
    [JsonPropertyName("addTextByTags")]
    public required List<AddTextByTagsAddTextByTag> AddTextByTags { get; set; }
}

public partial class AddTextByTagsAddTextByTag
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddKeyedTextByTagsVariables
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("tags")]
    public required IReadOnlyList<CaretTagInput> Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddKeyedTextByTagsData
{
    [JsonPropertyName("addKeyedTextByTags")]
    public required List<AddKeyedTextByTagsAddKeyedTextByTag> AddKeyedTextByTags { get; set; }
}

public partial class AddKeyedTextByTagsAddKeyedTextByTag
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddSimpleKeyedTextByTagsVariables
{
    [JsonPropertyName("tags")]
    public required IReadOnlyList<CaretTagInput> Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddSimpleKeyedTextByTagsData
{
    [JsonPropertyName("addKeyedTextByTags")]
    public required List<AddSimpleKeyedTextByTagsAddKeyedTextByTag> AddKeyedTextByTags { get; set; }
}

public partial class AddSimpleKeyedTextByTagsAddKeyedTextByTag
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class LogVariables
{
    [JsonPropertyName("severity")]
    public required LogSeverity Severity { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("arguments")]
    public required IReadOnlyList<string> Arguments { get; set; }
}

public partial class LogData
{
    [JsonPropertyName("log")]
    public required string Log { get; set; }
}

public partial class GetConfigurationVariables { }

public partial class GetConfigurationData
{
    [JsonPropertyName("configuration")]
    public required GetConfigurationConfiguration Configuration { get; set; }
}

public partial class GetConfigurationConfiguration
{
    [JsonPropertyName("outputPath")]
    public required string OutputPath { get; set; }
}
