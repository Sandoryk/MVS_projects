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
        public HostelDBContext db;
        public DataBaseConnector()
        {
            db = new HostelDBContext();
        }
        public List<IPersonInfo> GetHabitants() //For populating database
        {
            List<IPersonInfo> list;
            // создаем два объекта User
            PersonInfoDBModel user1 = new PersonInfoDBModel { FirstName = "Tom", LastName = "Fisko", DateBirth = DateTime.Now, RoomNumber = "1-1", Sex = "Male" };
            PersonInfoDBModel user2 = new PersonInfoDBModel { FirstName = "Sam", LastName = "Howoer", DateBirth = DateTime.Now, RoomNumber = "1-2", Sex = "Male" };
            // добавляем их в бд 
            db.PersonInfoList.Add(user1);
            db.PersonInfoList.Add(user2);
            db.SaveChanges();
            list = db.PersonInfoList.ToList<IPersonInfo>();
            return list;
        }
        public List<T> GetAllRecords<T>() where T : class
        {
            List<T> list = db.SetOf<T>().ToList<T>();
            return list;
        }
        public List<IPersonPayments> GetPersonPaymentsRecords(Expression<Func<PersonPaymentsDBModel, bool>> predicate)
        {
            List<IPersonPayments> list = db.PersonPaymentsList.Where(predicate).ToList<IPersonPayments>();
            return list;
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
