using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.SectionTasks.Commands.EvaluateStudent;

public class EvaluateStudentCommandHandler : IRequestHandler<EvaluateStudentCommand, ErrorOr<Success>>
{
    private readonly ISectionTaskRepository _sectionTaskRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EvaluateStudentCommandHandler(ISectionTaskRepository sectionTaskRepository, ISectionRepository sectionRepository, IUnitOfWork unitOfWork)
    {
        _sectionTaskRepository = sectionTaskRepository;
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(EvaluateStudentCommand request, CancellationToken cancellationToken)
    {
        var task = await _sectionTaskRepository.GetByIdAsync(request.SectionTaskId);
        if (task is null)
            return EvaluateStudentErrors.TaskNotFound;

        var section = await _sectionRepository.GetByIdWithStudentsAsync(task.SectionId);
        var studentInSection = section!.Students.Any(s => s.Id == request.StudentId);
        if (!studentInSection)
            return EvaluateStudentErrors.StudentNotInSection;

        var result = task.EvaluateStudent(
            request.StudentId,
            request.AchievedMark,
            request.Notes
            );
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}
