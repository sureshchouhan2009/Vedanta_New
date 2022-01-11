using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
   
    public class PostScoreModel
    {
        public int Id { get; set; }
        public int GembaWalkScheduleId { get; set; }
        public int AoGembaCheckListMasterId { get; set; }
        public int Score { get; set; }
        public bool IsDeleted { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedOn { get; set; }
    }
}
