using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Lectures;
using Methaq.Domain.Sections.enums;

namespace Methaq.Application.Lectures.Commands.CreateLecture;

public class CreateLectureCommandHandler : IRequestHandler<CreateLectureCommand, ErrorOr<Guid>>
{
    private readonly ILectureRepository _lectureRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLectureCommandHandler(ILectureRepository lectureRepository, ISectionRepository sectionRepository, IUnitOfWork unitOfWork)
    {
        _lectureRepository = lectureRepository;
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateLectureCommand request, CancellationToken cancellationToken)
    {
        var section=await _sectionRepository.GetByIdAsync(request.SectionId);
        if (section is null)
            return CreateLectureErrors.SectionNotFound;

        if(section.Status==SectionStatus.Closed)
            return CreateLectureErrors.SectionClosed;

        var lectureResult = Lecture.Create(
            request.SectionId,
            request.Date,
            request.StartTime,
            request.EndTime
            );
        if(lectureResult.IsError)
            return lectureResult.Errors;

        var lecture = lectureResult.Value;
        await _lectureRepository.AddAsync(lecture,cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return lecture.Id;
    }
}
