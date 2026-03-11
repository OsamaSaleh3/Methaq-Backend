using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.UseCases.Students.Commands.RemoveStudentFromCenter;

public class RemoveStudentFromCenterCommandHandler : IRequestHandler<RemoveStudentFromCenterCommand, ErrorOr<Success>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveStudentFromCenterCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(RemoveStudentFromCenterCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null)
            return RemoveStudentFromCenterErrors.StudentNotFound;

        var result = student.RemoveFromCenter();
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}