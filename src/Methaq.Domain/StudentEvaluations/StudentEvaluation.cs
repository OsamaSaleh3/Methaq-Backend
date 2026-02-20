using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Students;
using System;

namespace Methaq.Domain.StudentEvaluations;

public class StudentEvaluation : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public decimal MemorizationScore { get; private set; }
    public decimal AttendanceScore { get; private set; }
    public decimal ParticipationScore { get; private set; }
    public decimal InteractionScore { get; private set; }

    protected StudentEvaluation() { }

    private StudentEvaluation(Guid studentId, decimal memorization, decimal attendance, decimal participation, decimal interaction)
    {
        StudentId = studentId;
        MemorizationScore = memorization;
        AttendanceScore = attendance;
        ParticipationScore = participation;
        InteractionScore = interaction;
    }

    public static ErrorOr<StudentEvaluation> Create(Guid studentId, decimal memorization, decimal attendance, decimal participation, decimal interaction)
    {
        if (studentId == Guid.Empty)
            return StudentEvaluationErrors.StudentIdRequired;

        if (memorization < 0 || memorization > 100 || attendance < 0 || attendance > 100 ||
            participation < 0 || participation > 100 || interaction < 0 || interaction > 100)
        {
            return StudentEvaluationErrors.InvalidScores;
        }

        return new StudentEvaluation(studentId, memorization, attendance, participation, interaction);
    }

    public ErrorOr<Success> UpdateScores(decimal memorization, decimal attendance, decimal participation, decimal interaction)
    {
        if (memorization < 0 || memorization > 100 || attendance < 0 || attendance > 100 ||
            participation < 0 || participation > 100 || interaction < 0 || interaction > 100)
        {
            return StudentEvaluationErrors.InvalidScores;
        }

        MemorizationScore = memorization;
        AttendanceScore = attendance;
        ParticipationScore = participation;
        InteractionScore = interaction;
        MarkAsUpdated();
        return Result.Success;
    }

    public decimal GetTotalScore() => (MemorizationScore + AttendanceScore + ParticipationScore + InteractionScore) / 4;
}