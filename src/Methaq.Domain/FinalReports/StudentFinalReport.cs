using Methaq.Domain.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Domain.FinalReports
{
    public class StudentFinalReport
    {
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public Guid FinalReportId { get; private set; }

        public decimal MemorizationScore { get; private set; }
        public decimal AttendanceScore { get; private set; }
        public decimal ParticipationScore { get; private set; }
        public decimal BehaviorScore { get; private set; }

        public string? SupervisorNotes { get; private set; }

        public decimal TotalScore =>
            (MemorizationScore + AttendanceScore + ParticipationScore + BehaviorScore) / 4;

        protected StudentFinalReport() { }

        public StudentFinalReport(Guid studentId, Guid finalReportId, decimal memorization, decimal attendance, decimal participation, decimal behavior, string? notes)
        {
            StudentId = studentId;
            FinalReportId = finalReportId;
            MemorizationScore = memorization;
            AttendanceScore = attendance;
            ParticipationScore = participation;
            BehaviorScore = behavior;
            SupervisorNotes = notes;
        }
    }
}
