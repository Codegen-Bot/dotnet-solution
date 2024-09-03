using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodegenBot;

namespace DotnetSolution;

public static partial class GraphQLOperations { }

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
