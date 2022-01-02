using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
    public class SBUFilterModel
    {
       public bool IsSelected { get; set; }
       public String SBUName { get; set; }
    }

    public class DepartmentModel
    {
        public bool IsSelected { get; set; }
        public String DepartmentName { get; set; }
    }
    public class StatusModel
    {
        public bool IsSelected { get; set; }
        public String StatusName { get; set; }
    }
}
