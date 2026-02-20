using ErrorOr;
using System;

namespace Methaq.Domain.StudentSurahRecords;

public static class StudentSurahRecordErrors
{
    public static readonly Error StudentIdRequired = Error.Validation(
        code: "SurahRecord.StudentId",
        description: "Student ID is required.");

    public static readonly Error SurahNameRequired = Error.Validation(
        code: "SurahRecord.SurahName",
        description: "Surah name is required.");

    public static readonly Error AlreadyCompleted = Error.Conflict(
        code: "SurahRecord.AlreadyCompleted",
        description: "This Surah is already marked as completed.");

    public static readonly Error CompletionDateRequired = Error.Validation(
       code: "SurahRecord.CompletionDateRequired",
       description: "Completion date is required when status is not Current.");

    public static readonly Error CurrentStatusCannotHaveCompletionDate = Error.Validation(
        code: "SurahRecord.InvalidCompletionDate",
        description: "Current surah cannot have a completion date.");

    public static readonly Error CompletionDateCannotBeInFuture = Error.Validation(
        code: "SurahRecord.FutureDate",
        description: "Completion date cannot be in the future.");


}
