using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowManager.ModelsWFM
{
    public class LinkWFM
    {
        public int LinkId { get; set; }
        public string Type { get; set; }
        public int SourceTaskId { get; set; }
        public int TargetTaskId { get; set; }
    }
}
