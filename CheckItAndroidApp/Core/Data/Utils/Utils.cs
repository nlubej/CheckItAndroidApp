using System;
using System.Globalization;

namespace CheckItAndroidApp.Core.Data.Utils
{
    public class Utils
    {
        public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public static DateTime? ToDateTimeNull(string dateString)
        {
            DateTime date;
            if (DateTime.TryParseExact(dateString, DateFormat, null, DateTimeStyles.None, out date))
            {
                return date;
            }

            return null;
        }

        public static DateTime ToDateTime(string dateString)
        {
            DateTime date;
            if (DateTime.TryParseExact(dateString, DateFormat, null, DateTimeStyles.None, out date))
            {
                return date;
            }

            throw new InvalidCastException("Error converting string to DateTime");
        }

        public static Enums.FrequencyType ToFrequencyType(int typeId)
        {
           switch (typeId)
            {
                case (int) Enums.FrequencyType.Custom:
                    return Enums.FrequencyType.Custom;

                case (int) Enums.FrequencyType.Predefined:
                    return Enums.FrequencyType.Predefined;
            }

            return Enums.FrequencyType.Custom;
        }
    }
}