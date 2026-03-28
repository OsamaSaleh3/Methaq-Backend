namespace Methaq.Contracts.Lectures;

public record CreateLectureRequest(
    Guid SectionId,
    DateOnly Date,
    string StartTime,
    string EndTime
);