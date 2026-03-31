using Methaq.Application.Common.Interfaces;
using Methaq.Application.SectionTasks.Queries.GetTasksHeatmap;
using Methaq.Domain.SectionTasks;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Repositories
{
    public class SectionTaskRepository : ISectionTaskRepository
    {
        private readonly ApplicationDbContext _context;
public SectionTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionTask task)
        {
            await _context.SectionTasks.AddAsync(task);
        }

        public async Task<SectionTask?> GetByIdAsync(Guid id)
        {
            return await _context.SectionTasks.FindAsync(id);
        }

        public async Task<List<SectionTask>> GetByLectureIdAsync(Guid lectureId)
        {
            return await _context.SectionTasks
                .Include(st=>st.AssignedBy)
                    .ThenInclude(e=>e.User)
                .Include(st=>st.Student)
                    .ThenInclude(s=>s!.User)
                .Where(s => s.LectureId == lectureId).ToListAsync();
        }

        public async Task<List<StudentTaskEvaluation>> GetEvaluationsByStudentIdAsync(Guid studentId)
        {
            return await _context.StudentTaskEvaluations
                .Include(s => s.SectionTask)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<SectionTask>> GetBySectionIdAndDateAsync(Guid sectionId, DateOnly date)
        {
            return await _context.SectionTasks
        .Include(t => t.AssignedBy).ThenInclude(e => e.User)
        .Include(t => t.Student).ThenInclude(s => s!.User)
        .Where(t => t.SectionId == sectionId && t.Lecture.Date == date)
        .ToListAsync();
        }

        public async Task<List<TaskHeatmapResponse>> GetTasksHeatmapAsync(Guid sectionId)
        {
             return await _context.SectionTasks
                .Include(t => t.Lecture)
                .Where(t => t.SectionId == sectionId)
                .GroupBy(t => t.Lecture.Date)
                .Select(g => new TaskHeatmapResponse(g.Key, g.Count()))
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
    }
}
