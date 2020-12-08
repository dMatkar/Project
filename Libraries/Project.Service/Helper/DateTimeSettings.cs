using Project.Core.Configuration;
using System;

namespace Project.Service.Helper
{
    public class DateTimeSettings : ISettings
    {
        public string DefaultTimeZone { get; set; }
    }
}
