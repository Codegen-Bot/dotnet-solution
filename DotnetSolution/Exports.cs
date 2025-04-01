using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using CodegenBot;
using Extism;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSolution;

/// <summary>
/// This class contains all the static methods that codegen.bot calls. See also the Imports class,
/// which contains static methods that we can call from within a bot that are implemented by codegen.bot.
/// </summary>
public class Exports
{
    private static SimpleGraphQLServer? _server;

    public static async Task<int> Main(string[] args)
    {
        var botDebug = BotUtility.GetDebugConfiguration(Environment.CurrentDirectory);
        var consumedUrl = botDebug!.ConsumeGraphQLUrl;
        var httpClient = new HttpClient() { BaseAddress = new Uri(consumedUrl) };
        var graphQLClient = new SyncHttpGraphQLClient(httpClient, consumedUrl);

        _server = new SimpleGraphQLServer(
            botDebug.ProvideGraphQLUrl!,
            request => ProcessGraphQLRequest(request, graphQLClient)
        );

        // Start the server and get the URL
        var serverTask = _server.StartAsync();
        do
        {
            await Task.Delay(100); // Give it a moment to start and get the URL
        } while (_server.Url is null);

        Console.WriteLine($"Server is running at {_server.Url}");
        Console.WriteLine("Press Ctrl+C to stop the server");
        graphQLClient.MarkAsReady(Process.GetCurrentProcess().Id, _server.Url);

        // Set up cancellation on Ctrl+C
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (s, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        try
        {
            // Wait for the server to complete or for cancellation
            await serverTask;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Server is shutting down...");
        }
        finally
        {
            _server.Stop();
        }

        var result = RunBot(graphQLClient, null);
        return result;
    }

    [UnmanagedCallersOnly(EntryPoint = "stop_running")]
    public static int StopRunning()
    {
        // This only gets called when running in WebAssembly, so no need to call _server.Stop() from here
        // because _server is only non-null when running as a separate process.
        return 0;
    }

    private static GraphQLServer? _graphqlServer;

    public static int RunBot(IGraphQLClient graphQLClient, ICodegenBotImports? imports)
    {
        try
        {
            // Create all our minibots here
            IMiniBot[] miniBots =
            [
            ];

            // Run each minibot in order
            foreach (var miniBot in miniBots)
            {
                try
                {
                    miniBot.Execute();
                }
                catch (Exception e)
                {
                    if (imports is not null)
                    {
                        imports.Log(
                            new LogEvent()
                            {
                                Level = LogEventLevel.Critical,
                                Message =
                                    "Failed to run minibot {MiniBot}: {ExceptionType} {Message}, {StackTrace}",
                                Args =
                                [
                                    miniBot.GetType().Name,
                                    e.GetType().Name,
                                    e.Message,
                                    e.StackTrace ?? "",
                                ],
                            }
                        );
                    }
                    else
                    {
                        graphQLClient.Log(
                            // Only a critical error will cause codegen.bot to realize that the generated code should not be used
                            LogSeverity.CRITICAL,
                            "Failed to run minibot {MiniBot}: {ExceptionType} {Message}, {StackTrace}",
                            [
                                miniBot.GetType().Name,
                                e.GetType().Name,
                                e.Message,
                                e.StackTrace ?? "",
                            ]
                        );
                    }
                }
            }

            return 0;
        }
        catch (Exception e)
        {
            if (imports is not null)
            {
                imports.Log(
                    new LogEvent()
                    {
                        Level = LogEventLevel.Critical,
                        Message = "Failed to run bot: {ExceptionType} {Message}, {StackTrace}",
                        Args = [e.GetType().Name, e.Message, e.StackTrace ?? ""],
                    }
                );
                Pdk.SetError($"{e.GetType()}: {e.Message} {e.StackTrace}");
            }
            else
            {
                graphQLClient.Log(
                    // Only a critical error will cause codegen.bot to realize that the generated code should not be used
                    LogSeverity.CRITICAL,
                    "Failed to run bot: {ExceptionType} {Message}, {StackTrace}",
                    [e.GetType().Name, e.Message, e.StackTrace ?? ""]
                );
                Console.WriteLine($"{e.GetType()}: {e.Message}");
            }
            return 0;
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "entry_point")]
    public static int Run()
    {
        var imports = new CodegenBotImports();
        var graphQLClient = new WasmGraphQLClient(imports);

        return RunBot(graphQLClient, imports);
    }

    public static string ProcessGraphQLRequest(string request, IGraphQLClient graphqlClient)
    {
        try
        {
            if (_graphqlServer is null)
            {
                var services = new ServiceCollection();
                services.AddSingleton<IGraphQLClient>(graphqlClient);

                var serviceProvider = services.BuildServiceProvider();
                _graphqlServer = new GraphQLServer(serviceProvider);
            }

            return _graphqlServer.ProcessGraphQLRequest(request);
        }
        catch (Exception e)
        {
            graphqlClient.Log(
                // Only a critical error will cause codegen.bot to realize that the generated code should not be used
                LogSeverity.CRITICAL,
                "Bot failed to handle GraphQL request: {ExceptionType} {Message}, {StackTrace}",
                [e.GetType().Name, e.Message, e.StackTrace ?? ""]
            );
            return "";
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "handle_request")]
    public static int HandleRequest()
    {
        var imports = new CodegenBotImports();
        var graphQLClient = new WasmGraphQLClient(imports);

        var request = Pdk.GetInputString();

        var result = ProcessGraphQLRequest(request, graphQLClient);

        Pdk.SetOutput(result);

        return 0;
    }
}
