using ErrorOr;
using MediatR;
using Methaq.Domain.SectionTasks.enums;
using Methaq.Domain.SectionTasks.ValueObject;

namespace Methaq.Application.SectionTasks.Commands.CreateSectionTask;

public record CreateSectionTaskCommand(
    string Title,
    string? Description,
    Guid SectionId,
    Guid LectureId,
    Guid AssignedById,
    decimal FullMark,
    TaskTypes Types,
    Guid? StudentId,
    QuranRange? Range
) : IRequest<ErrorOr<Guid>>;