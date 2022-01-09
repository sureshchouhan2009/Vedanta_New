using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class GembaChecklistParametersModel
    {
        public string Measure { get; set; }
        public string Checkpoint { get; set; }
        public string Score0 { get; set; }
        public string Score3 { get; set; }
        public string Score5 { get; set; }
        public int Score { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
    }
}
