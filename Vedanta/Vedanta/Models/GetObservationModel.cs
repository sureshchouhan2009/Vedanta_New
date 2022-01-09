using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class GetObservationModel
    {
        public int GembaWalkScheduleId { get; set; }
        public object GembaWalkSchedule { get; set; }
        public int AoGembaCheckListMasterId { get; set; }
        public object AoGembaCheckListMaster { get; set; }
        public string Observations { get; set; }
        public string AoGembaObservationFile { get; set; }
        public string ObservationImage { get; set; }
        public string ObservationImage1 { get; set; }
        public string ObservationImage2 { get; set; }
        public string ImageObservationFile { get; set; }
        public string Measure { get; set; }
        public int ActionCount { get; set; }
        public string Employee { get; set; }
        public int Score { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
    }
}
