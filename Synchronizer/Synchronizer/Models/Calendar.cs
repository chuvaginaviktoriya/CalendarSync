﻿using System.Collections.Generic;

namespace Synchronizer
{
    public enum CalendarType { Google, Outlook };

    public class Calendar
    {
        public List<Appointment> Appointments { get; set; }
        public CalendarType Type { get; set; }
    }
}
