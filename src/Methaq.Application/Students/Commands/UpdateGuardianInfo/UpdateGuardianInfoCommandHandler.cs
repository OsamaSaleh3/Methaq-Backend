using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Students;

namespace Methaq.Application.UseCases.Students.Commands.UpdateGuardianInfo;

public class UpdateGuardianInfoCommandHandler : IRequestHandler<UpdateGuardianInfoCommand, ErrorOr<Success>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGuardianInfoCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateGuardianInfoCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (student is null)
            return UpdateGuardianInfoErrors.StudentNotFound;

        var result = student.UpdateGuardianInfo(request.GuardianName, request.GuardianPhone, request.GuardianEmail);
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}