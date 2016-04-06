using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic_4_Task_1
{
    class ClientInfo
    {
        public ClientInfo(string fn, string ln, DateTime bd, string phone, string email, string passID, string cardID, DateTime CardD)
        {
            FirstName = fn;
            LastName = ln;
            BirthDay = bd;
            PhoneNumber = phone;
            EMail = email;
            PassportID = passID;
            CardID = cardID;
            CardDate = CardD;

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public string PassportID { get; set; }
        public string CardID { get; set; }
        public DateTime CardDate { get; set; }
        public string[] GetProperties()
        {
            string[] props = new string[] { FirstName, LastName, BirthDay.Date.ToString("D"), PhoneNumber, EMail, PassportID, String.Format(new FormatCardID(), "{0:Cd}", CardID), CardDate.Date.ToString("D") };
            return props;
        }
    }
}
