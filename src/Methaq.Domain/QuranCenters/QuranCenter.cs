using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Employees;
using Methaq.Domain.Sections;
using Methaq.Domain.CenterEnrollmentRequests;
using Methaq.Domain.QuranCenters.enums;

namespace Methaq.Domain.QuranCenters;

public class QuranCenter : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Location { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }
    public CenterStatus Status { get; private set; }
    public Guid ManagerId { get; private set; }
    public Employee Manager { get; private set; } = null!;

    private readonly List<Section> _sections = [];
    public IReadOnlyCollection<Section> Sections => _sections.AsReadOnly();

    private readonly List<Employee> _supervisors = [];
    public IReadOnlyCollection<Employee> Supervisors => _supervisors.AsReadOnly();

    private readonly List<CenterEnrollmentRequest> _enrollmentRequests = [];
    public IReadOnlyCollection<CenterEnrollmentRequest> EnrollmentRequests => _enrollmentRequests.AsReadOnly();

    protected QuranCenter() { }

    private QuranCenter(string name, string description, string location, string? phoneNumber, Guid managerId)
    {
        Name = name;
        Description = description;
        Location = location;
        PhoneNumber = phoneNumber;
        ManagerId = managerId;
        Status = CenterStatus.Active;
    }

    public static ErrorOr<QuranCenter> Create(string name, string description, string location, string? phoneNumber, Guid managerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return QuranCenterErrors.NameRequired;

        if (string.IsNullOrWhiteSpace(location))
            return QuranCenterErrors.LocationRequired;

        if (managerId == Guid.Empty)
            return QuranCenterErrors.ManagerIdRequired;

        return new QuranCenter(name, description, location, phoneNumber, managerId);
    }

    public ErrorOr<Success> AddSupervisor(Employee supervisor)
    {
        if (supervisor == null)
            return QuranCenterErrors.SupervisorNull;

        if (_supervisors.Any(s => s.Id == supervisor.Id))
            return QuranCenterErrors.SupervisorAlreadyAssigned;

        if (!supervisor.CanBeSupervisor())
            return QuranCenterErrors.SupervisorNotActive;

        _supervisors.Add(supervisor);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> RemoveSupervisor(Guid supervisorId)
    {
        var supervisor = _supervisors.FirstOrDefault(s => s.Id == supervisorId);
        if (supervisor == null)
            return QuranCenterErrors.SupervisorNotFound;

        _supervisors.Remove(supervisor);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> AddSection(Section section)
    {
        if (section == null)
            return QuranCenterErrors.SectionNull;

        _sections.Add(section);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> UpdateInfo(string? name, string? description, string? location, string? phoneNumber)
    {
        if (Status == CenterStatus.Closed)
            return QuranCenterErrors.CenterClosed;

        if (!string.IsNullOrWhiteSpace(name))
            Name = name;

        if (description != null)
            Description = description;

        if (!string.IsNullOrWhiteSpace(location))
            Location = location;

        if (phoneNumber != null)
            PhoneNumber = phoneNumber;

        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> TransferManagement(Guid newManagerId)
    {
        if (newManagerId == Guid.Empty)
            return QuranCenterErrors.ManagerIdRequired;

        if (newManagerId == ManagerId)
            return QuranCenterErrors.SameManager;

        if (!_supervisors.Any(s => s.Id == newManagerId))
            return QuranCenterErrors.NewManagerNotSupervisor;

        ManagerId = newManagerId;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Close()
    {
        if (Status == CenterStatus.Closed)
            return QuranCenterErrors.AlreadyClosed;

        Status = CenterStatus.Closed;
        MarkAsUpdated();
        return Result.Success;
    }

    public bool IsSupervisor(Guid employeeId) =>
        _supervisors.Any(s => s.Id == employeeId);
}
