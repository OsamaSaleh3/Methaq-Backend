using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Lectures.Queries.GetLectureById;
using Methaq.Application.Lectures.Queries.GetLecturesBySection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Lectures.Queries.GetLecturesByDate
{
    public class GetLecturesByDateQueryHandler : IRequestHandler<GetLecturesByDateQuery, ErrorOr<List<LectureSummaryResponse>>>
    {
        private readonly ILectureRepository _lectureRepository;

        public GetLecturesByDateQueryHandler(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public async Task<ErrorOr<List<LectureSummaryResponse>>> Handle(GetLecturesByDateQuery query, CancellationToken cancellationToken)
        {
            var lectures = await _lectureRepository.GetBySectionIdAndDateAsync(query.SectionId, query.Date);

            return lectures.Select(l => new LectureSummaryResponse(
                l.Id, l.Date, l.StartTime, l.EndTime,
                (int)l.Status, l.AttendanceRecords.Count, l.SectionTasks.Count
            )).ToList();
        }
    }
}
