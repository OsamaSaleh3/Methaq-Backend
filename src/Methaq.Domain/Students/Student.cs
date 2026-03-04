using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using Methaq.Domain.QuranCenters;
using Methaq.Domain.Sections;
using Methaq.Domain.SectionTasks;

namespace Methaq.Domain.Students;

public class Student : BaseEntity
{
    public string UserId { get; private set; } = null!;
    public ApplicationUser User { get; private set; } = null!;
    public string GuardianName { get; private set; } = null!;
    public string GuardianPhone { get; private set; } = null!;
    public string? GuardianEmail { get; private set; }
    public string AcademicLevel { get; private set; } = null!;
    public Guid? CenterId { get; private set; }
    public QuranCenter? Center { get; private set; }
    public Guid? SectionId { get; private set; }
    public Section? Section { get; private set; }

    protected Student() { }

    private Student(string userId, string guardianName, string guardianPhone, string? guardianEmail, string academicLevel)
    {
        UserId = userId;
        GuardianName = guardianName;
        GuardianPhone = guardianPhone;
        GuardianEmail = guardianEmail;
        AcademicLevel = academicLevel;
    }

    public static ErrorOr<Student> Create(string userId, string guardianName, string guardianPhone, string? guardianEmail, string academicLevel)
    {
        if (string.IsNullOrEmpty(userId))
            return StudentErrors.UserIdRequired;

        if (string.IsNullOrWhiteSpace(guardianName))
            return StudentErrors.GuardianNameRequired;

        if (string.IsNullOrWhiteSpace(guardianPhone))
            return StudentErrors.GuardianPhoneRequired;


        return new Student(userId, guardianName, guardianPhone, guardianEmail, academicLevel);
    }

    public ErrorOr<Success> AssignToCenter(Guid centerId)
    {
        if (centerId == Guid.Empty)
            return StudentErrors.InvalidCenterId;

        if (CenterId != null)
            return StudentErrors.AlreadyInCenter;

        CenterId = centerId;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> RemoveFromCenter()
    {
        if (CenterId == null)
            return StudentErrors.NotAssignedToCenter;

        CenterId = null;
        Center = null;
        SectionId = null;
        Section = null;
        MarkAsUpdated();
        return Result.Success;
    }
    public ErrorOr<Success> AssignToSection(Guid sectionId)
    {
        if (sectionId == Guid.Empty)
            return StudentErrors.InvalidSectionId;

        if(this.SectionId != null)
            return StudentErrors.AlreadyInSection;

        SectionId = sectionId;
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

    public ErrorOr<Success> UpdateGuardianInfo(string? guardianName, string? guardianPhone, string? guardianEmail)
    {
        if (!string.IsNullOrWhiteSpace(guardianName))
            GuardianName = guardianName;

        if (!string.IsNullOrWhiteSpace(guardianPhone))
            GuardianPhone = guardianPhone;

        if (guardianEmail != null)
            GuardianEmail = guardianEmail;

        MarkAsUpdated();
        return Result.Success;
    }

    
}
