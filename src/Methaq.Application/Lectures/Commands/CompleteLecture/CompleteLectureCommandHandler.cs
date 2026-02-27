using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Lectures.Commands.CompleteLecture;

public class CompleteLectureCommandHandler : IRequestHandler<CompleteLectureCommand, ErrorOr<Success>>
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteLectureCommandHandler(ILectureRepository lectureRepository, IUnitOfWork unitOfWork)
    {
        _lectureRepository = lectureRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(CompleteLectureCommand request, CancellationToken cancellationToken)
    {
        var lecture =await _lectureRepository.GetByIdAsync(request.LectureId);
        if (lecture is null)
            return CompleteLectureErrors.LectureNotFound;

        var result = lecture.Complete(request.Notes);
        if (result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success;
    }
}
