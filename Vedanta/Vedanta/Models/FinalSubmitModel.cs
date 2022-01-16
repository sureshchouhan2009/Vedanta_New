using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class FinalSubmitModel
    {
        public int Id { get; set; }
        public int GembaWalkScheduleId { get; set; }
        public int AoGembaCheckListMasterId { get; set; }
        public object Observations { get; set; }
        public object ObservationImage { get; set; }
        public object Measure { get; set; }
        public object Employee { get; set; }
        public int AoGembaObservationId { get; set; }
        public object Date { get; set; }
        public int AoSBUMasterId { get; set; }
        public int AoDepartmentMasterId { get; set; }
        public int AoCategoryMasterId { get; set; }
        public object EmployeeMappingId { get; set; }
        public object Leader { get; set; }
        public object Status { get; set; }
        public object SBU { get; set; }
        public object Department { get; set; }
        public object Category { get; set; }
        public object UserName { get; set; }
        public int Score { get; set; }
        public object Percentage { get; set; }
        public bool IsDeleted { get; set; }
        public object PerformedBy { get; set; }
        public object PerformedOn { get; set; }
    }
}
