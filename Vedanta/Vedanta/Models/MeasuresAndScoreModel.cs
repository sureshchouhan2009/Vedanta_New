using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class MeasuresAndScoreModel
    {
        public int GembaWalkScheduleId { get; set; }
        public object GembaWalkSchedule { get; set; }
        public int AoGembaCheckListMasterId { get; set; }
        public object AoGembaCheckListMaster { get; set; }
        public string Observations { get; set; }
        public object AoGembaObservationFile { get; set; }
        public object ObservationImage { get; set; }
        public object ObservationImage1 { get; set; }
        public object ObservationImage2 { get; set; }
        public object ImageObservationFile { get; set; }
        public string Measure { get; set; }
        public int ActionCount { get; set; }
        public string Employee { get; set; }
        public int Score { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public object PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
        public bool IsDetailsViewEnabled { get; set; }

    }
}
