using ErrorOr;

namespace Methaq.Application.Lectures.Commands.StartLecture;

public static class StartLectureErrors
{
    public static readonly Error LectureNotFound = Error.NotFound(
        code: "Lecture.NotFound",
        description: "Lecture not found.");
}