using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Domain.MemorizationTasks.ValueObject
{
    public record QuranRange(
        string Volume,
        string SurahName,
        int StartPage,
        int EndPage,
        int StartAyah,
        int EndAyah
    );
}
