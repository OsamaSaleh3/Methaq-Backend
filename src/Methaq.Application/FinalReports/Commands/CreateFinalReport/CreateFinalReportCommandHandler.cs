using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.FinalReports;
using Methaq.Domain.Sections.enums;

namespace Methaq.Application.FinalReports.Commands.CreateFinalReport;

public class CreateFinalReportCommandHandler : IRequestHandler<CreateFinalReportCommand, ErrorOr<Guid>>
{
    private readonly IFinalReportRepository _finalReportRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFinalReportCommandHandler(
        IFinalReportRepository finalReportRepository,
        ISectionRepository sectionRepository,
        IUnitOfWork unitOfWork)
    {
        _finalReportRepository = finalReportRepository;
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateFinalReportCommand command, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(command.SectionId);
        if (section is null)
            return CreateFinalReportErrors.SectionNotFound;

        if (section.Status == SectionStatus.Closed)
            return CreateFinalReportErrors.SectionClosed;

        var existingReport = await _finalReportRepository.GetBySectionIdAsync(command.SectionId);
        if (existingReport is not null)
            return CreateFinalReportErrors.ReportAlreadyExists;

        var reportResult = FinalReport.Create(command.SectionId, command.GeneralNotes);
        if (reportResult.IsError)
            return reportResult.Errors;

        var report = reportResult.Value;
        await _finalReportRepository.AddAsync(report);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return reportResult.Value.Id;
    }
}