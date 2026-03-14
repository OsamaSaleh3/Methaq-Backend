using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.AttendanceRecords;
using Methaq.Domain.CenterEnrollmentRequests;
using Methaq.Domain.Employees;
using Methaq.Domain.FinalReports;
using Methaq.Domain.GroupChats;
using Methaq.Domain.Lectures;
using Methaq.Domain.Notifications;
using Methaq.Domain.PushTokens;
using Methaq.Domain.QuranCenters;
using Methaq.Domain.RefreshTokens;
using Methaq.Domain.Sections;
using Methaq.Domain.SectionTasks;
using Methaq.Domain.Students;
using Methaq.Domain.SupervisorEnrollmentRequests;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Methaq.Infrastructure.Common.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<QuranCenter> QuranCenters => Set<QuranCenter>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<Lecture> Lectures => Set<Lecture>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<SectionTask> SectionTasks => Set<SectionTask>();
    public DbSet<StudentTaskEvaluation> StudentTaskEvaluations => Set<StudentTaskEvaluation>();
    public DbSet<CenterEnrollmentRequest> CenterEnrollmentRequests => Set<CenterEnrollmentRequest>();
    public DbSet<FinalReport> FinalReports => Set<FinalReport>();
    public DbSet<StudentFinalReport> StudentFinalReports => Set<StudentFinalReport>();
    public DbSet<GroupChat> GroupChats => Set<GroupChat>();
    public DbSet<GroupMessage> GroupMessages => Set<GroupMessage>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<UserChatLastRead> UserChatLastReads => Set<UserChatLastRead>();
    public DbSet<SupervisorEnrollmentRequest> SupervisorEnrollmentRequests => Set<SupervisorEnrollmentRequest>();
    public DbSet<PushToken> PushTokens => Set<PushToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}