using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class SBUFilterModel
    {
        public int ID { get; set; }
        public bool IsSelected { get; set; }
       public String SBUName { get; set; }
    }

    public class DepartmentModel
    {
        public int SBUID { get; set; }
        public bool IsSelected { get; set; }
        public String DepartmentName { get; set; }
    }
    public class StatusModel
    {
        public bool IsSelected { get; set; }
        public String StatusName { get; set; }
    }
}
