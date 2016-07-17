using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixHandler
{
    public class NoSuchMemberException:Exception
    {
        string message;
        public NoSuchMemberException()
        {
            message = "Required matrix member does not exist";
        }
        public NoSuchMemberException(string mes)
        {
            message = mes;
        }
        public override string Message { get { return message; } }
    }
}
