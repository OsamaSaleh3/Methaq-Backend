using ErrorOr;

namespace Methaq.Application.Students;

public static class StudentErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        code: "Student.UserId",
        description: "User ID cannot be empty.");

    public static readonly Error GuardianNameRequired = Error.Validation(
        code: "Student.GuardianName",
        description: "Guardian name is required.");

    public static readonly Error GuardianPhoneRequired = Error.Validation(
        code: "Student.GuardianPhone",
        description: "Guardian phone is required.");

    public static readonly Error NotAssignedToSection = Error.Conflict(
        code: "Student.NotAssigned",
        description: "Student is not assigned to any section.");

    public static readonly Error InvalidSectionId = Error.Validation(
        code: "Student.SectionId",
        description: "Section ID is invalid.");

    public static readonly Error AlreadyInSection = Error.Conflict(
        code: "Student.AlreadyInSection",
        description: "Student is already assigned to a section. Must leave current section first.");

    public static readonly Error InvalidCenterId = Error.Validation(
        code: "Student.CenterId",
        description: "Center ID is invalid.");

    public static readonly Error AlreadyInCenter = Error.Conflict(
        code: "Student.AlreadyInCenter",
        description: "Student is already assigned to a center.");

    public static readonly Error NotAssignedToCenter = Error.Conflict(
        code: "Student.NotAssignedToCenter",
        description: "Student is not assigned to any center.");

    public static readonly Error NotFound = Error.NotFound(
        code: "Student.NotFound",
        description: "Student not found.");

    public static readonly Error FinalReportNotFound = Error.NotFound(
        code: "Student.FinalReportNotFound",
        description: "Final report not found for this student.");
}