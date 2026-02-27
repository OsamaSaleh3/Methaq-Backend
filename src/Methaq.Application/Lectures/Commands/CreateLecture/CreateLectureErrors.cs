using ErrorOr;

namespace Methaq.Application.Lectures.Commands.CreateLecture;

public static class CreateLectureErrors
{
    public static readonly Error SectionNotFound = Error.NotFound(
        code: "Lecture.SectionNotFound",
        description: "Section not found.");

    public static readonly Error SectionClosed = Error.Conflict(
        code: "Lecture.SectionClosed",
        description: "Cannot create a lecture for a closed section.");
}