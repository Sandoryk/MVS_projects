using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class DataBaseInteract
    {
        public List<PersonInfo> GetHabitants()
        {
            List<PersonInfo> list;
            using (HostelDBContext db = new HostelDBContext())
            {
                // создаем два объекта User
                //PersonInfo user1 = new PersonInfo { FirstName = "Tom", LastName = "Fisko", DateBirth = DateTime.Now, RoomNumber = "1-01", Sex = "Male" };
                //PersonInfo user2 = new PersonInfo { FirstName = "Sam", LastName = "Howoer", DateBirth = DateTime.Now, RoomNumber = "1-02", Sex = "Male" };
                // добавляем их в бд 
                //db.PersonInfoList.Add(user1);
                //db.PersonInfoList.Add(user2);
                //db.SaveChanges();
                list = db.PersonInfoList.ToList();
            }
            return list;
        }
    }
}
