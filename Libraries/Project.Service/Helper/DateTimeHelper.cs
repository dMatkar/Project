using System;
using System.Collections.Generic;

namespace Project.Service.Helper
{
    public class DateTimeHelper : IDateTimeHelper
    {
        #region Properties

        public TimeZoneInfo DefaultTimeZone 
        { 
            get
            {
               return FindTimeZoneById("India Standard Time");
            }
        }

        #endregion

        #region Methods

        public DateTime ConvertToUserTime(DateTime dateTime)
        {
            return ConvertToUserTime(dateTime: dateTime, dateTimeKind: dateTime.Kind);
        }

        public DateTime ConvertToUserTime(DateTime dateTime, DateTimeKind dateTimeKind)
        {
            DateTime dt = DateTime.SpecifyKind(dateTime, dateTimeKind);
            var currentTimeZone = DefaultTimeZone;
           return TimeZoneInfo.ConvertTime(dt, currentTimeZone);
        }

        public DateTime ConvertToUtcTime(DateTime dateTime)
        {
            return ConvertToUtcTime(dateTime, dateTime.Kind);
        }

        public DateTime ConvertToUtcTime(DateTime dateTime,DateTimeKind dateTimeKind)
        {
           DateTime dt = DateTime.SpecifyKind(dateTime, dateTimeKind);
            return TimeZoneInfo.ConvertTimeToUtc(dt);
        }

        public TimeZoneInfo FindTimeZoneById(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return null;
            return TimeZoneInfo.FindSystemTimeZoneById(Id);
        }

        public IReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        #endregion
    }
}
