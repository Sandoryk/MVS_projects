using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowSpy.Models
{
    public class LinkViewModel
    {
        public int LinkId { get; set; }
        public string Type { get; set; }
        public int SourceTaskId { get; set; }
        public int TargetTaskId { get; set; }
        public Guid GUID { get; set; }
    }
}