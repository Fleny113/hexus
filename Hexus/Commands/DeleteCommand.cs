using Hexus.Daemon.Contracts;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Hexus.Commands;

internal static class DeleteCommand
{
    private static readonly Argument<string> NameArgument = new("name", "The name of the application to delete");
    private static readonly Option<bool> ForceOption = new(["--force", "-f"], "Force the stop of the application if it needs to be stopped");
    
    public static readonly Command Command = new("delete", "Stops and delete an application")
    {
        NameArgument,
        ForceOption,
    };

    static DeleteCommand()
    {        
        Command.SetHandler(HandleAsync);
    }
    
    private static async Task HandleAsync(InvocationContext context)
    {
        var name = context.ParseResult.GetValueForArgument(NameArgument);
        var force = context.ParseResult.GetValueForOption(ForceOption);
        var ct = context.GetCancellationToken();

        if (!await HttpInvocation.CheckForRunningDaemon(ct))
        {
            PrettyConsole.Error.MarkupLine(PrettyConsole.DaemonNotRunningError);
            return;
        }

        var stopRequest = await HttpInvocation.HttpClient.DeleteAsync($"/{name}/delete?forceStop={force}", ct);

        if (!stopRequest.IsSuccessStatusCode)
        {
            var response = await stopRequest.Content.ReadFromJsonAsync<ErrorResponse>(HttpInvocation.JsonSerializerOptions, ct);

            Debug.Assert(response is not null);

            PrettyConsole.Error.MarkupLine($"There [indianred1]was an error[indianred1] deleting the application \"{name}\": {response.Error}");
            return;
        }

        PrettyConsole.Out.MarkupLine($"Application \"{name}\" [darkred_1]deleted[/]!");
    }
}
