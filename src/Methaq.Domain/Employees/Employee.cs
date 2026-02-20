using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using Methaq.Domain.Employees;
using Methaq.Domain.Employees.enums;
using Methaq.Domain.Sections;
using System;
using System.Collections.Generic;
namespace Methaq.Domain.Employees;

public class Employee : BaseEntity
{
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    public AcademicDegree Degree { get; private set; }
    public string? IslamicQualifications { get; private set; } 
    public string Specialization { get; private set; } = null!;
    public string? CurrentJob { get; private set; } 
    public DateTime HireDate { get; private set; }
    public EmploymentStatus EmploymentStatus { get; private set; }

    private readonly List<Section> _supervisedSections = [];
    public IReadOnlyCollection<Section> SupervisedSections => _supervisedSections.AsReadOnly();

    protected Employee() { }

    private Employee(Guid userId, AcademicDegree degree, string specialization, string? islamicQualifications, string? currentJob)
    {
        UserId = userId;
        Degree = degree;
        Specialization = specialization;
        IslamicQualifications = islamicQualifications;
        CurrentJob = currentJob;
        HireDate = DateTime.UtcNow;
        EmploymentStatus = EmploymentStatus.Active;
    }

    public static ErrorOr<Employee> Create(Guid userId, AcademicDegree degree, string specialization, string? islamicQualifications,string? currentJob)
    {
        if (userId == Guid.Empty)
            return EmployeeErrors.UserIdRequired;

        return new Employee(userId, degree, specialization, islamicQualifications,currentJob);
    }

    public ErrorOr<Success> Resign()
    {
        if (EmploymentStatus == EmploymentStatus.Resigned)
            return EmployeeErrors.AlreadyResigned;

        EmploymentStatus = EmploymentStatus.Resigned;
        MarkAsUpdated();
        return Result.Success;
    }

public ErrorOr<Success> UpdateQualifications(
    AcademicDegree? degree, 
    string? specialization, 
    string? islamicQualifications)
{
    if (EmploymentStatus == EmploymentStatus.Resigned)
        return EmployeeErrors.CannotUpdateResigned;

    if (degree.HasValue)
        Degree = degree.Value;
    
    if (!string.IsNullOrWhiteSpace(specialization))
        Specialization = specialization;
    
    if (islamicQualifications != null)
        IslamicQualifications = islamicQualifications;

    MarkAsUpdated();
    return Result.Success;
}

public ErrorOr<Success> UpdateCurrentJob(string? newJob)
{
    if (EmploymentStatus == EmploymentStatus.Resigned)
        return EmployeeErrors.CannotUpdateResigned;

    CurrentJob = newJob;
    MarkAsUpdated();
    return Result.Success;
}

public ErrorOr<Success> Reactivate()
{
    if (EmploymentStatus != EmploymentStatus.Resigned)
        return EmployeeErrors.NotResigned;

    EmploymentStatus = EmploymentStatus.Active;
    MarkAsUpdated();
    return Result.Success;
}

public bool CanBeSupervisor() => EmploymentStatus == EmploymentStatus.Active;
}