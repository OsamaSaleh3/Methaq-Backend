using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Sections.Queries.GetSectionStudents;

public class GetSectionStudentsQueryHandler : IRequestHandler<GetSectionStudentsQuery, ErrorOr<List<SectionStudentResponse>>>
{
    private readonly ISectionRepository _sectionRepository;

    public GetSectionStudentsQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<ErrorOr<List<SectionStudentResponse>>> Handle(GetSectionStudentsQuery query, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdWithStudentsAsync(query.SectionId);
        if (section is null)
            return Error.NotFound("Section.NotFound", "Section not found.");

        var response = section.Students.Select(s => new SectionStudentResponse(
            StudentId: s.Id,
            FullName: s.User.FullName,
            Email: s.User.Email!,
            PhoneNumber: s.User.PhoneNumber!,
            AcademicLevel: s.AcademicLevel
        )).ToList();

        return response;
    }
}