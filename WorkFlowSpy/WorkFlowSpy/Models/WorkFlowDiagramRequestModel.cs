using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkFlowSpy.Tools;

namespace WorkFlowSpy.Models
{
    public class WorkFlowDiagramRequestModel
    {
        public DiagramMode Mode { get; set; }
        public DiagramAction Action { get; set; }
        public TaskViewModel UpdatedTask { get; set; }
        public LinkViewModel UpdatedLink { get; set; }
        public long SourceId { get; set; }
    }
}