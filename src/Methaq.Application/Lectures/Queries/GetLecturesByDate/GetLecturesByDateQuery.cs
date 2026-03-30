using ErrorOr;
using MediatR;
using Methaq.Application.Lectures.Queries.GetLecturesBySection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Lectures.Queries.GetLecturesByDate
{
    public record GetLecturesByDateQuery(Guid SectionId, DateOnly Date) : IRequest<ErrorOr<List<LectureSummaryResponse>>>;

}
