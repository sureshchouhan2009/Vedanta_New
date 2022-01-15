using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class GetObservationImagesModel
    {
        public int AoGembaObservationId { get; set; }
        public object AoGembaObservation { get; set; }
        public string FileSource { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string FileSource1 { get; set; }
        public string FileType1 { get; set; }
        public string FileName1 { get; set; }
        public string Description1 { get; set; }
        public string FileSource2 { get; set; }
        public string FileType2 { get; set; }
        public string FileName2 { get; set; }
        public string Description2 { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public object PerformedBy { get; set; }
        public DateTime PerformedOn { get; set; }
    }
}
