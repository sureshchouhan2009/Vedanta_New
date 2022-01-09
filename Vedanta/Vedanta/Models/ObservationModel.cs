using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
  
    public class PostObservationModel
    {
        public int Id { get; set; }
        public int GembaWalkScheduleId { get; set; }
        public int AoGembaCheckListMasterId { get; set; }
        public string Observations { get; set; }
        public string ObservationImage { get; set; }
        public string ObservationImage1 { get; set; }
        public string ObservationImage2 { get; set; }
        public string Measure { get; set; }
        public string Employee { get; set; }
        public int AoGembaObservationId { get; set; }
        public string Date { get; set; }
        public int AoSBUMasterId { get; set; }
        public int AoDepartmentMasterId { get; set; }
        public int AoCategoryMasterId { get; set; }
        public string EmployeeMappingId { get; set; }
        public object Leader { get; set; }
        public object Status { get; set; }
        public string SBU { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public string Percentage { get; set; }
        public bool IsDeleted { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedOn { get; set; }
    }
}
