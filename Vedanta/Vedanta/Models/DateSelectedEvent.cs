using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class DateSelectedEvent : EventArgs
    {
        public DateTime selectedDate { get; set; }

        public DateSelectedEvent(DateTime selectedDateargs)
        {
            selectedDate = selectedDateargs;
        }
    }
}
