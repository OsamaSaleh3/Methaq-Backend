using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Sections.Commands.AddStudentToSection;

public class AddStudentToSectionCommandHandler : IRequestHandler<AddStudentToSectionCommand, ErrorOr<Success>>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IEnrollmentRequestRepository _enrollmentRepository;
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddStudentToSectionCommandHandler(ISectionRepository sectionRepository, IStudentRepository studentRepository, IEnrollmentRequestRepository enrollmentRepository, IGroupChatRepository groupChatRepository, IUnitOfWork unitOfWork)
    {
        _sectionRepository = sectionRepository;
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
        _groupChatRepository = groupChatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(AddStudentToSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(request.SectionId);
        if (section is null)
            return AddStudentToSectionErrors.SectionNotFound;

        var student = await _studentRepository.GetByIdWithUserAsync(request.StudentId,cancellationToken);
        if (student is null)
            return AddStudentToSectionErrors.StudentNotFound;

        var approvedRequest = await _enrollmentRepository.GetApprovedRequestAsync(request.StudentId, section.CenterId);
        if (approvedRequest is null)
            return AddStudentToSectionErrors.StudentNotEnrolledInCenter;

        var result = section.AddStudent(student);
        if (result.IsError)
            return result.Errors;

        var chat = await _groupChatRepository.GetBySectionIdAsync(request.SectionId);
        if (chat is not null)
        {
            var addMemberResult = chat.AddMember(student.User);
            if (addMemberResult.IsError)
                return addMemberResult.Errors;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
