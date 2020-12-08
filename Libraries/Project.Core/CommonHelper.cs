using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Web.Hosting;

namespace Project.Core
{
    public partial class CommonHelper
    {
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }
        public static object To(object value, Type detinationType)
        {
            return To(value, detinationType, CultureInfo.InvariantCulture);
        }
        public static object To(object value, Type destinaionType, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                Type sourceType = value.GetType();

                 var destinationTypeConverter = TypeDescriptor.GetConverter(destinaionType);
                if (destinationTypeConverter != null && destinationTypeConverter.CanConvertFrom(sourceType))
                    return destinationTypeConverter.ConvertFrom(null, cultureInfo, value);

                var sourceTypeConverter = TypeDescriptor.GetConverter(sourceType);
                if (sourceTypeConverter != null && sourceTypeConverter.CanConvertTo(destinaionType))
                    return sourceTypeConverter.ConvertTo(null, cultureInfo, value, destinaionType);
            }
            return null;
        }

        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace("/", "\\");
            return Path.Combine(basePath, path);
        }
    }
}
