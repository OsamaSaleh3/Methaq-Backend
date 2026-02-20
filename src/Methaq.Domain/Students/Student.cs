using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using System;
using System.Collections.Generic;
using Methaq.Domain.Sections;
using Methaq.Domain.MemorizationTasks;
using Methaq.Domain.MemorizationTasks.enums;

namespace Methaq.Domain.Students;

public class Student : BaseEntity
{
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    public string ParentName { get; private set; } = null!;
    public string AcademicLevel { get; private set; } = null!;

    public Guid? SectionId { get; private set; }
    public Section? Section { get; private set; }

    private readonly List<MemorizationTask> _tasks = [];
    public IReadOnlyCollection<MemorizationTask> Tasks => _tasks.AsReadOnly();

    protected Student() { }

    private Student(Guid userId, string parentName, string academicLevel)
    {
        UserId = userId;
        ParentName = parentName;
        AcademicLevel = academicLevel;
    }

    public static ErrorOr<Student> Create(Guid userId, string parentName, string academicLevel)
    {
        if (userId == Guid.Empty)
            return StudentErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(parentName))
            return StudentErrors.ParentNameRequired;

        return new Student(userId, parentName, academicLevel);
    }

    public ErrorOr<Success> AssignToSection(Guid sectionId)
    {
        if (sectionId == Guid.Empty)
            return StudentErrors.InvalidSectionId;

        SectionId = sectionId;
        MarkAsUpdated();
        return Result.Success;
    }


    public ErrorOr<Success> AddTask(MemorizationTask task)
    {
        if (task == null)
            return StudentErrors.TaskCannotBeNull;

        _tasks.Add(task);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> RemoveTask(Guid taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null)
            return StudentErrors.TaskNotFound;

        _tasks.Remove(task);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> RemoveFromSection()
    {
        if (SectionId == null)
            return StudentErrors.NotAssignedToSection;

        SectionId = null;
        Section = null;
        MarkAsUpdated();
        return Result.Success;
    }

    public int GetCompletedTaskCount() => _tasks.Count(t => t.Status == MemorizationTasks.enums.TaskStatus.Completed);

    public decimal? GetAverageScore()
    {
        var completedTasks = _tasks.Where(t => t.AchievedMark.HasValue).ToList();
        return completedTasks.Any() ? completedTasks.Average(t => t.AchievedMark ?? 0) : null;
    }
}