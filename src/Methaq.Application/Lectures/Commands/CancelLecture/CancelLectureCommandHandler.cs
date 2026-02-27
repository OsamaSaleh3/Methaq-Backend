using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Lectures.Commands.CancelLecture;

public class CancelLectureCommandHandler : IRequestHandler<CancelLectureCommand, ErrorOr<Success>>
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelLectureCommandHandler(ILectureRepository lectureRepository, IUnitOfWork unitOfWork)
    {
        _lectureRepository = lectureRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(CancelLectureCommand request, CancellationToken cancellationToken)
    {
        var lecture =await _lectureRepository.GetByIdAsync(request.LectureId);
        if (lecture is null)
            return CancelLectureErrors.LectureNotFound;

        var cancelResult = lecture.Cancel();
        if (cancelResult.IsError)
            return cancelResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
