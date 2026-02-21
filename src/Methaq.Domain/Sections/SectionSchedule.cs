using ErrorOr;

namespace Methaq.Domain.Sections;

public record SectionSchedule(
    List<DayOfWeek> Days,
    TimeOnly StartTime,
    TimeOnly EndTime
)
{
    public ErrorOr<bool> IsValid() {

        if(Days == null || Days.Count == 0)
            return SectionErrors.ScheduleDaysRequired;
        if(EndTime<= StartTime)
            return SectionErrors.ScheduleInvalidTime;

        return true;
        
    }

   

       
}
