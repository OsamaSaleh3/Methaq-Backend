using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.MemorizationTasks.ValueObject;
using Methaq.Domain.Employees;
using Methaq.Domain.Students;
using TaskStatus = Methaq.Domain.MemorizationTasks.enums.TaskStatus;
using System;

namespace Methaq.Domain.MemorizationTasks;

public class MemorizationTask : BaseEntity
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public QuranRange Range { get; private set; } = null!;

    public DateTime TaskDate { get; private set; }
    public decimal FullMark { get; private set; }
    public decimal? AchievedMark { get; private set; }
    public string? Notes { get; private set; }
    public TaskStatus Status { get; private set; }

    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public Guid AssignedById { get; private set; }
    public Employee AssignedBy { get; private set; } = null!;

    protected MemorizationTask() { }

    private MemorizationTask(string title, string description, QuranRange range, DateTime taskDate, Guid studentId, Guid assignedById)
    {
        Title = title;
        Description = description;
        Range = range;
        TaskDate = taskDate;
        StudentId = studentId;
        AssignedById = assignedById;
        FullMark = 100;
        Status = TaskStatus.Pending;
    }

    public static ErrorOr<MemorizationTask> Create(string title, string description, QuranRange range, DateTime taskDate, Guid studentId, Guid assignedById)
    {
        if (string.IsNullOrWhiteSpace(title))
            return MemorizationTaskErrors.TitleRequired;

        if (range == null)
            return MemorizationTaskErrors.RangeRequired;

        return new MemorizationTask(title, description, range, taskDate, studentId, assignedById);
    }

    public ErrorOr<Success> Evaluate(decimal mark, string? notes)
    {
        if (mark < 0 || mark > FullMark)
            return MemorizationTaskErrors.InvalidMark(FullMark);

        AchievedMark = mark;
        Notes = notes;
        Status = TaskStatus.Completed;
        MarkAsUpdated();

        return Result.Success;
    }

    public ErrorOr<Success> ReEvaluate(decimal newMark, string? newNotes)
    {
        if (Status == TaskStatus.Pending)
            return MemorizationTaskErrors.CannotReEvaluatePendingTask;

        if (newMark < 0 || newMark > FullMark)
            return MemorizationTaskErrors.InvalidMark(FullMark);

        AchievedMark = newMark;
        Notes = newNotes;
        MarkAsUpdated();
        return Result.Success;
    }


    public ErrorOr<Success> Update(string? newTitle, string? newDescription, QuranRange? newRange)
    {
        if (Status == TaskStatus.Completed)
            return MemorizationTaskErrors.CannotModifyEvaluatedTask;

        if (!string.IsNullOrWhiteSpace(newTitle))
            Title = newTitle;

        if (newDescription != null)
            Description = newDescription;

        if (newRange != null)
            Range = newRange;

        MarkAsUpdated();
        return Result.Success;
    }

    

    public ErrorOr<Success> CompleteTask()
    {
        if (AchievedMark == null)
            return MemorizationTaskErrors.MarkNotSet;

        Status = TaskStatus.Completed;
        MarkAsUpdated();
        return Result.Success;
    }


}