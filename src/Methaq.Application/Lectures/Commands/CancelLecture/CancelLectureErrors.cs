using ErrorOr;

namespace Methaq.Application.Lectures.Commands.CancelLecture;

public static class CancelLectureErrors
{
    public static readonly Error LectureNotFound = Error.NotFound(
        code: "Lecture.NotFound",
        description: "Lecture not found.");
}