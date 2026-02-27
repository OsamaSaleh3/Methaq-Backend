using ErrorOr;
using MediatR;
using Methaq.Domain.Sections.enums;

namespace Methaq.Application.Sections.Commands.CreateSection;

public record CreateSectionCommand(
    string Name,
    AcademicLevel AcademicLevel,
    Guid CenterId,
    Guid SupervisorId,
    List<DayOfWeek> ScheduleDays,
    TimeOnly StartTime,
    TimeOnly EndTime
) : IRequest<ErrorOr<Guid>>;