using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    
    public class GembaScheduleModel
    {
        public DateTime Date { get; set; }
        public int AoSBUMasterId { get; set; }
        public int AoDepartmentMasterId { get; set; }
        public int AoCategoryMasterId { get; set; }
        public string EmployeeMappingId { get; set; }
        public string Status { get; set; }
        public object UserStatus { get; set; }
        public DateTime GembaWalkActualDate { get; set; }
        public string SBU { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string Employee { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public string Percentage { get; set; }
        public string ActionPlanComplianceFraction { get; set; }
        public object ActionPlanCompliance { get; set; }
        public object AoGembaObservations { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public object PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
    }
}
