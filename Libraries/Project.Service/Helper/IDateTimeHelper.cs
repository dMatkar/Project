using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Helper
{
    public interface IDateTimeHelper
    {
        TimeZoneInfo FindTimeZoneById(string Id);
        IReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones();
        DateTime ConvertToUserTime(DateTime dateTime);
        DateTime ConvertToUserTime(DateTime dateTime, DateTimeKind dateTimeKind);
        DateTime ConvertToUtcTime(DateTime dateTime);
        DateTime ConvertToUtcTime(DateTime dateTime, DateTimeKind dateTimeKind);
    }
}
