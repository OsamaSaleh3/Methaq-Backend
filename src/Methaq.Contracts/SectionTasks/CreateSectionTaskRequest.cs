namespace Methaq.Contracts.SectionTasks;

public record CreateSectionTaskRequest(
    string Title,
    string? Description,
    Guid SectionId,
    Guid LectureId,
    Guid AssignedById,
    decimal FullMark,
    string Types,
    Guid? StudentId,
    QuranRangeRequest? Range
);

public record QuranRangeRequest(
    string Volume,
    string SurahName,
    int StartPage,
    int EndPage,
    int StartAyah,
    int EndAyah
);