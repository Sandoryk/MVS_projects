using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class DataBaseConnector: IDisposable
    {
        HostelDBContext db;
        public DataBaseConnector()
        {
            db = new HostelDBContext();
        }
       /* public List<PersonInfo> GetHabitants()
        {
            List<PersonInfo> list;
            // создаем два объекта User
            //PersonInfo user1 = new PersonInfo { FirstName = "Tom", LastName = "Fisko", DateBirth = DateTime.Now, RoomNumber = "1-01", Sex = "Male" };
            //PersonInfo user2 = new PersonInfo { FirstName = "Sam", LastName = "Howoer", DateBirth = DateTime.Now, RoomNumber = "1-02", Sex = "Male" };
            // добавляем их в бд 
            //db.PersonInfoList.Add(user1);
            //db.PersonInfoList.Add(user2);
            //db.SaveChanges();
            list = db.PersonInfoList.ToList();
            return list;
        }*/
        public ObservableCollection<T> GetAllRecords<T>() where T : class
        {
            List<T> list = db.SetOf<T>().ToList<T>();
            return new ObservableCollection<T>(list);
        }
        public void HandlePersonInfoTable(IPersonInfo inData, Expression<Func<PersonInfoDBModel, bool>> predicate)
        {
            List<PersonInfoDBModel> persInfList = db.PersonInfoList.Where(predicate).ToList();
            if (persInfList.Count > 0)
            {
                PropertyInfo[] propInfos = typeof(IPersonInfo).GetProperties();
                foreach (var item in persInfList)
                {
                    foreach (var curPropt in propInfos)
                    {
                        curPropt.SetValue(item, curPropt.GetValue(inData));
                    }
                }
            }
        }
        public int SaveChanges()
        {
            return db.SaveChanges(); 
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
