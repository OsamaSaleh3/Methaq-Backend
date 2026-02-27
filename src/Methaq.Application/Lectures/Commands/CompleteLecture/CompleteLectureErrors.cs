using ErrorOr;

namespace Methaq.Application.Lectures.Commands.CompleteLecture;

public static class CompleteLectureErrors
{
    public static readonly Error LectureNotFound = Error.NotFound(
        code: "Lecture.NotFound",
        description: "Lecture not found.");
}