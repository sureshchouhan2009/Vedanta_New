using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
   
    public class UpdateActionPlanModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string ResponsibilityImage { get; set; }
        public string ResponsibilityImage1 { get; set; }
        public string ResponsibilityImage2 { get; set; }
        public string Date { get; set; }
        public string TargetDate { get; set; }
        public string ActionPlan { get; set; }
        public string Department { get; set; }
        public string Employee { get; set; }
        public int AoGembaObservationId { get; set; }
        public int GembaWalkScheduleId { get; set; }
        public string UserName { get; set; }
        public string ResponsibilityUserId { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedOn { get; set; }
    }
}
