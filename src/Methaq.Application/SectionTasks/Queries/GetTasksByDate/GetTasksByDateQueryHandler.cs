using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.SectionTasks.Queries.GetTasksByLecture;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.SectionTasks.Queries.GetTasksByDate
{
    public class GetTasksByDateQueryHandler : IRequestHandler<GetTasksByDateQuery, ErrorOr<List<SectionTaskResponse>>>
    {
        private readonly ISectionTaskRepository _sectionTaskRepository;

        public GetTasksByDateQueryHandler(ISectionTaskRepository sectionTaskRepository)
        {
            _sectionTaskRepository = sectionTaskRepository;
        }

        public async Task<ErrorOr<List<SectionTaskResponse>>> Handle(GetTasksByDateQuery query, CancellationToken cancellationToken)
        {
            var tasks = await _sectionTaskRepository.GetBySectionIdAndDateAsync(query.SectionId, query.Date);

            return tasks.Select(t => new SectionTaskResponse(
                t.Id, t.Title, t.Description, t.SectionId, t.LectureId,
                t.AssignedById, t.AssignedBy.User.FullName, t.FullMark,
                t.Types, t.Status, t.StudentId, t.Student?.User.FullName,
                t.Range, t.CreatedAt
            )).ToList();
        }
    }
}
