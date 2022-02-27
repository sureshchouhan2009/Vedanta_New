using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
   
    public class ActionPlanDetailsModel
    {
        public int GembaWalkScheduleId { get; set; }
        public object GembaWalkSchedule { get; set; }
        public int AoGembaObservationId { get; set; }
        public object AoGembaObservation { get; set; }
        public string ActionPlan { get; set; }
        public object ResponsibilityUserId { get; set; }
        public DateTime TargetDate { get; set; }
        public object Remark { get; set; }
        public string Status { get; set; }
        public string ResponsibilityImage { get; set; }
        public string ResponsibilityImage1 { get; set; }
        public string ResponsibilityImage2 { get; set; }
        public object ImageResponsibilityFile { get; set; }
        public string Employee { get; set; }
        public string UserName { get; set; }
        public object Measure { get; set; }
        public string Observations { get; set; }
        public DateTime Date { get; set; }
        public string Department { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public object PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
    }
}
