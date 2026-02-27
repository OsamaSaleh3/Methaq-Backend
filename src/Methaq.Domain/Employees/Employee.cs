using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using Methaq.Domain.Employees.enums;
using Methaq.Domain.QuranCenters;
using Methaq.Domain.Sections;

namespace Methaq.Domain.Employees;

public class Employee : BaseEntity
{
    public string UserId { get; private set; } = null!;
    public ApplicationUser User { get; private set; } = null!;

    public AcademicDegree Degree { get; private set; }
    public string? IslamicQualifications { get; private set; }
    public string Specialization { get; private set; } = null!;
    public string? CurrentJob { get; private set; }
    public DateTime HireDate { get; private set; }
    public EmploymentStatus EmploymentStatus { get; private set; }

    public EmployeeRole Role { get; private set; }
    public Guid? CenterId { get; private set; }      // المركز اللي هو فيه
    public QuranCenter? Center { get; private set; }

    private readonly List<Section> _supervisedSections = [];
    public IReadOnlyCollection<Section> SupervisedSections => _supervisedSections.AsReadOnly();

    protected Employee() { }

    private Employee(string userId, AcademicDegree degree, string specialization, string? islamicQualifications, string? currentJob, EmployeeRole role)
    {
        UserId = userId;
        Degree = degree;
        Specialization = specialization;
        IslamicQualifications = islamicQualifications;
        CurrentJob = currentJob;
        HireDate = DateTime.UtcNow;
        EmploymentStatus = EmploymentStatus.Active;
        Role = role;
    }

    public static ErrorOr<Employee> Create(string userId, AcademicDegree degree, string specialization, string? islamicQualifications, string? currentJob, EmployeeRole role)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return EmployeeErrors.UserIdRequired;

        return new Employee(userId, degree, specialization, islamicQualifications, currentJob, role);
    }

    public ErrorOr<Success> AssignToCenter(Guid centerId)
    {
        if (CenterId != null)
            return EmployeeErrors.AlreadyAssignedToCenter;

        CenterId = centerId;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> PromoteToManager(Guid centerId)
    {
        if (EmploymentStatus == EmploymentStatus.Resigned)
            return EmployeeErrors.CannotUpdateResigned;

        Role = EmployeeRole.CenterManager;
        CenterId = centerId;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> DemoteToSupervisor()
    {
        if (Role != EmployeeRole.CenterManager)
            return EmployeeErrors.NotAManager;

        Role = EmployeeRole.Supervisor;
        CenterId = null;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Resign()
    {
        if (EmploymentStatus == EmploymentStatus.Resigned)
            return EmployeeErrors.AlreadyResigned;

        EmploymentStatus = EmploymentStatus.Resigned;
        CenterId = null;
        MarkAsUpdated();
        return Result.Success;
    }

    public void RemoveFromCenter()
    {
        CenterId = null;
        MarkAsUpdated();
    }
    public ErrorOr<Success> UpdateQualifications(AcademicDegree? degree, string? specialization, string? islamicQualifications)
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
    public bool IsManager() => Role == EmployeeRole.CenterManager;
    public bool IsSupervisor() => Role == EmployeeRole.Supervisor;
}
