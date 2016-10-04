using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowSpy.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Progress { get; set; }
        public int SortOrder { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public string Holder { get; set; }
        public Guid GUID { get; set; }
        public string ProjectName { get; set; }
    }
}