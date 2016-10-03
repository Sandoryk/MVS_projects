using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceService
{
    public class EmployeeDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        [Index("NameIndex", IsUnique = true)]
        public Guid GUID { get; set; }
        public string HolderCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public string Position { get; set; }
        public bool Terminated { get; set; }
        public string IdentityId { get; set; }
    }
}
