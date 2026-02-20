using ErrorOr;
using System;

namespace Methaq.Domain.MemorizationTasks;

public static class MemorizationTaskErrors
{
    public static readonly Error TitleRequired = Error.Validation(
        code: "Task.Title",
        description: "Task title is required.");

    public static readonly Error MarkNotSet = Error.Validation(
        code: "Task.Mark",
        description: "You must set the mark before completing the task.");

    public static readonly Error RangeRequired = Error.Validation(
        code: "Task.Range",
        description: "Quran range is required.");

    public static readonly Error CannotReEvaluatePendingTask = Error.Validation(
        code: "Task.PendingTask",
        description: "The task status is pending.");

    public static readonly Error CannotModifyEvaluatedTask = Error.Conflict(
        code: "MemorizationTask.Evaluated",
        description: "Cannot modify evaluated task.");

    public static Error InvalidMark(decimal fullMark) => Error.Validation(
        code: "Task.Mark",
        description: $"Mark must be between 0 and {fullMark}.");
}
