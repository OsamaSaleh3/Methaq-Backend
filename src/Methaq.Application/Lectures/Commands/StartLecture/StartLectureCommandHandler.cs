using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Lectures.Commands.StartLecture;

public class StartLectureCommandHandler : IRequestHandler<StartLectureCommand, ErrorOr<Success>>
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StartLectureCommandHandler(ILectureRepository lectureRepository, IUnitOfWork unitOfWork)
    {
        _lectureRepository = lectureRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(StartLectureCommand request, CancellationToken cancellationToken)
    {
        var lecture = await _lectureRepository.GetByIdAsync(request.LectureId);
        if (lecture is null)
            return StartLectureErrors.LectureNotFound;

        var result=lecture.Start();
        if(result.IsError)
            return result.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;

    }
}
