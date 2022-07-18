using System;
using System.Collections.Generic;
using VinaCent.Blaze.Enums;

namespace VinaCent.Blaze.Utilities
{
    public static class DaySessionUtils
    {
        public static Dictionary<DaySessions, string> DaySessionWelcomeMessages = new()
        {
            { DaySessions.Morning, "DaySession:Welcome:GoodMorning%s" },
            { DaySessions.Noon, "DaySession:Welcome:GoodNoon%s" },
            { DaySessions.Afternoon, "DaySession:Welcome:GoodAfternoon%s" },
            { DaySessions.Evening, "DaySession:Welcome:GoodEvening%s" }
        };

        public static DaySessions CurrentSession
        {
            get
            {
                var currentTimeSpan = DateTime.Now.TimeOfDay;
                if (new TimeSpan(5, 0, 0) < currentTimeSpan && currentTimeSpan <= new TimeSpan(10, 0, 0))
                {
                    return DaySessions.Morning;
                }

                if (new TimeSpan(10, 0, 0) < currentTimeSpan && currentTimeSpan <= new TimeSpan(13, 0, 0))
                {
                    return DaySessions.Noon;
                }

                if (new TimeSpan(13, 0, 0) < currentTimeSpan && currentTimeSpan <= new TimeSpan(17, 59, 59))
                {
                    return DaySessions.Afternoon;
                }

                return DaySessions.Evening;
            }
        }

        public static string CurrentWelcomeMessage => DaySessionWelcomeMessages[CurrentSession];
    }
}
