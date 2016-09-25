using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceService.DBModels
{
    public class LinkDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkId { get; set; }
        [MaxLength(1)]
        public string Type { get; set; }
        public int SourceTaskId { get; set; }
        public int TargetTaskId { get; set; }
        public Guid GUID { get; set; }
    }
}
