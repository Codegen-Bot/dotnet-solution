using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using CodegenBot;

namespace DotnetSolution;

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

        if (parsedRequest is not null) { }
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
