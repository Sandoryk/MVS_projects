using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowManager.ModelsWFM
{
    public class EmployeeWFM
    { 
        public EmployeeWFM()
        {
            Terminated = false;
        }
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public string HolderCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? HiredDate { get; set; }
        public string Position { get; set; }
        public bool Terminated { get; set; }
        public string IdentityId { get; set; }
    }
    
}
