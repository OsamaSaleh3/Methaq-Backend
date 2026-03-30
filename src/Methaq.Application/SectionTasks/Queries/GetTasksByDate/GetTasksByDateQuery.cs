using ErrorOr;
using MediatR;
using Methaq.Application.SectionTasks.Queries.GetTasksByLecture;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.SectionTasks.Queries.GetTasksByDate
{
    public record GetTasksByDateQuery(Guid SectionId, DateOnly Date) : IRequest<ErrorOr<List<SectionTaskResponse>>>;

}
