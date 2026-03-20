using Spectre.Console;

namespace Advocu.Core;

/// <summary>
/// Extension methods for adding Spectre.Console status indicators to tasks.
/// </summary>
internal static class StatusExtensions
{
    /// <summary>
    /// Runs a task synchronously while displaying a status spinner in the console.
    /// </summary>
    /// <typeparam name="T">The return type of the task.</typeparam>
    /// <param name="task">The task to run.</param>
    /// <param name="message">The status message to display.</param>
    /// <returns>The result of the task.</returns>
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

    /// <summary>
    /// Runs a task asynchronously while displaying a status spinner in the console.
    /// </summary>
    /// <typeparam name="T">The return type of the task.</typeparam>
    /// <param name="task">The task to run.</param>
    /// <param name="message">The status message to display.</param>
    /// <returns>The result of the asynchronous task.</returns>
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
