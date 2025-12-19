using Spectre.Console;

namespace Advocu.NuGet;

internal static class StatusExtensions
{
    public static T RunWithStatus<T>(this Task<T> task, string message)
    {
        return AnsiConsole.Status()
            .Start(message, ctx =>
            {
                ctx.Spinner(Spinner.Known.Dots);
                ctx.SpinnerStyle(Style.Parse("green"));
                return task.GetAwaiter().GetResult();
            });
    }

    public static async Task<T> RunWithStatusAsync<T>(this Task<T> task, string message)
    {
         return await AnsiConsole.Status()
            .StartAsync(message, async ctx =>
            {
                ctx.Spinner(Spinner.Known.Dots);
                ctx.SpinnerStyle(Style.Parse("green"));
                return await task;
            });
    }
}
