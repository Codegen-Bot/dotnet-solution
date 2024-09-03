using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using CodegenBot;
using Extism;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSolution;

public class Functions
{
    public static void Main()
    {
        // Note: a `Main` method is required for the app to compile
    }

    private static IServiceProvider _services;

    [UnmanagedCallersOnly(EntryPoint = "greet")]
    public static int Greet()
    {
        var name = Pdk.GetInputString();
        var greeting = $"Hello, {name}!";
        Pdk.SetOutput(greeting);

        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "entry_point")]
    public static int Run()
    {
        try
        {
            var services = new ServiceCollection();
            CodegenBotHost.Log(
                new LogEvent()
                {
                    Level = LogEventLevel.Information,
                    Message = "Registering services",
                    Args = [],
                }
            );
            services.AddSingleton<IMiniBot, ExampleMiniBot>();

            _services = services.BuildServiceProvider();

            CodegenBotHost.Log(
                new LogEvent()
                {
                    Level = LogEventLevel.Information,
                    Message = "Built service provider",
                    Args = [],
                }
            );
            foreach (var miniBot in _services.GetRequiredService<IEnumerable<IMiniBot>>())
            {
                CodegenBotHost.Log(
                    new LogEvent()
                    {
                        Level = LogEventLevel.Information,
                        Message = "Running minibot {MiniBot}",
                        Args = [miniBot.GetType().Name],
                    }
                );
                try
                {
                    miniBot.Execute();
                }
                catch (Exception e)
                {
                    CodegenBotHost.Log(
                        new LogEvent()
                        {
                            Level = LogEventLevel.Error,
                            Message =
                                "Failed to run minibot {MiniBot}: {ExceptionType} {Message}, {StackTrace}",
                            Args =
                            [
                                miniBot.GetType().Name,
                                e.GetType().Name,
                                e.Message,
                                e.StackTrace,
                            ],
                        }
                    );
                }
            }

            return 0;
        }
        catch (Exception e)
        {
            CodegenBotHost.Log(
                new LogEvent()
                {
                    Level = LogEventLevel.Error,
                    Message = "Failed to initialize bot: {ExceptionType} {Message}, {StackTrace}",
                    Args = [e.GetType().Name, e.Message, e.StackTrace],
                }
            );
            Pdk.SetError($"{e.GetType()}: {e.Message}");
            return 20;
        }
    }
}
