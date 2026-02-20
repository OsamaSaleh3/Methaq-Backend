using ErrorOr;
using System;

namespace Methaq.Domain.StudentEvaluations;

public static class StudentEvaluationErrors
{
    public static readonly Error StudentIdRequired = Error.Validation(
        code: "Evaluation.StudentId",
        description: "Student ID is required.");

    public static readonly Error InvalidScores = Error.Validation(
        code: "Evaluation.Scores",
        description: "Scores must be between 0 and 100.");
}
