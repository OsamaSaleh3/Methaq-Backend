namespace Methaq.Contracts.Lectures;

public record CreateLectureRequest(
    Guid SectionId,
    DateTime Date,
    string StartTime,
    string EndTime
);