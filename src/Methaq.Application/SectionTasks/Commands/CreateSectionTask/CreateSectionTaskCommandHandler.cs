using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Lectures.enums;
using Methaq.Domain.Sections.enums;
using Methaq.Domain.SectionTasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.SectionTasks.Commands.CreateSectionTask;

public class CreateSectionTaskCommandHandler : IRequestHandler<CreateSectionTaskCommand, ErrorOr<Guid>>
{
    private readonly ISectionTaskRepository _sectionTaskRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSectionTaskCommandHandler(ISectionTaskRepository sectionTaskRepository, ILectureRepository lectureRepository, ISectionRepository sectionRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _sectionTaskRepository = sectionTaskRepository;
        _lectureRepository = lectureRepository;
        _sectionRepository = sectionRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateSectionTaskCommand request, CancellationToken cancellationToken)
    {
        var lecture = await _lectureRepository.GetByIdAsync(request.LectureId);
        if (lecture is null)
            return CreateSectionTaskErrors.LectureNotFound;

        if (lecture.Status == LectureStatus.Cancelled)
            return CreateSectionTaskErrors.LectureCancelled;

        var section = await _sectionRepository.GetByIdWithStudentsAsync(request.SectionId);
        if (section is null)
            return CreateSectionTaskErrors.SectionNotFound;

        if (section.Status == SectionStatus.Closed)
            return CreateSectionTaskErrors.SectionClosed;

        if (request.StudentId.HasValue)
        {
            var isInSection = section.Students.Any(s => s.Id == request.StudentId);
            if(!isInSection)
                return CreateSectionTaskErrors.StudentNotInSection;
        }

        var employee = await _employeeRepository.GetByIdAsync(request.AssignedById);
        if (employee is null)
            return CreateSectionTaskErrors.EmployeeNotFound;

        var taskResult = SectionTask.Create(
            request.Title,
            request.Description,
            request.SectionId,
            request.LectureId,
            request.AssignedById,
            request.FullMark
            );
        if (taskResult.IsError)
            return taskResult.Errors;

        var task = taskResult.Value;
        await _sectionTaskRepository.AddAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return task.Id;
    }
}
