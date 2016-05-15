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
        public DataBaseConnector(int timeOut)
        {
            db = new HostelDBContext(timeOut);
        }

        public List<T> GetAllRecords<T>() where T : class
        {
            List<T> list = db.SetOf<T>().ToList<T>();
            return list;
        }
        public List<IPersonInfo> GetPersonRecords(Expression<Func<PersonInfoDBModel, bool>> predicate)
        {
            List<IPersonInfo> list = db.PersonInfoList.Where(predicate).ToList<IPersonInfo>();
            return list;
        }
        public List<IPersonPayments> GetPersonPaymentRecords(Expression<Func<PersonPaymentsDBModel, bool>> predicate)
        {
            List<IPersonPayments> list = db.PersonPaymentsList.Where(predicate).ToList<IPersonPayments>();
            return list;
        }
        public List<ISettledList> GetSettledListRecords(Expression<Func<SettledListDBModel, bool>> predicate)
        {
            List<ISettledList> list = db.SettledList.Where(predicate).ToList<ISettledList>();
            return list;
        }
        public List<IRoom> GetRoomRecords(Expression<Func<RoomDBModel, bool>> predicate)
        {
            List<IRoom> list = db.RoomList.Where(predicate).ToList<IRoom>();
            return list;
        }
        public List<IRoomFurniture> GetFurnitureRoomRecords(Expression<Func<RoomFurnitureDBModel, bool>> predicate)
        {
            List<IRoomFurniture> list = db.RoomFurnitureList.Where(predicate).ToList<IRoomFurniture>();
            return list;
        }

        public int HandlePersonInfoTable(IPersonInfo inData, Expression<Func<PersonInfoDBModel, bool>> predicate, RecordActions state)
        {
            int result = 0;
            if (state == RecordActions.Updated && predicate!=null)
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
                result = 1;
            }
            else if (state == RecordActions.Inserted)
            {
                PersonInfoDBModel person = new PersonInfoDBModel();
                PropertyInfo[] propInfos = typeof(IPersonInfo).GetProperties();
                foreach (var curPropt in propInfos)
                {
                    curPropt.SetValue(person, curPropt.GetValue(inData));
                }
                db.PersonInfoList.Add(person);
                result = 1;
            }
            else if (state == RecordActions.Deleted)
            {
                List<PersonInfoDBModel> persInfList = db.PersonInfoList.Where(predicate).ToList();
                if (persInfList.Count > 0)
                {
                    foreach (var item in persInfList)
                    {
                        db.PersonInfoList.Remove(item);
                    }
                }
                result = 1;
            }
            return result;
            
        }
        public int HandlePersonPaymentsTable(IPersonPayments inData, Expression<Func<PersonPaymentsDBModel, bool>> predicate, RecordActions state)
        {
            int result = 0;
            if (state == RecordActions.Updated && predicate != null)
            {
                List<PersonPaymentsDBModel> persInfList = db.PersonPaymentsList.Where(predicate).ToList();
                if (persInfList.Count > 0)
                {
                    PropertyInfo[] propInfos = typeof(IPersonPayments).GetProperties();
                    foreach (var item in persInfList)
                    {
                        foreach (var curPropt in propInfos)
                        {
                            curPropt.SetValue(item, curPropt.GetValue(inData));
                        }
                    }
                }
                result = 1;
            }
            else if (state == RecordActions.Inserted)
            {
                PersonPaymentsDBModel payment = new PersonPaymentsDBModel();
                PropertyInfo[] propInfos = typeof(IPersonPayments).GetProperties();
                foreach (var curPropt in propInfos)
                {
                    curPropt.SetValue(payment, curPropt.GetValue(inData));
                }
                db.PersonPaymentsList.Add(payment);
                result = 1;
            }
            else if (state == RecordActions.Deleted)
            {
                List<PersonPaymentsDBModel> persInfList = db.PersonPaymentsList.Where(predicate).ToList();
                if (persInfList.Count > 0)
                {
                    foreach (var item in persInfList)
                    {
                        db.PersonPaymentsList.Remove(item);
                    }
                }
                result = 1;
            }
            return result;

        }
        public int HandleSettledListTable(ISettledList inData, Expression<Func<SettledListDBModel, bool>> predicate, RecordActions state)
        {
            int result = 0;
            if (state == RecordActions.Updated && predicate != null)
            {
                List<SettledListDBModel> settledfList = db.SettledList.Where(predicate).ToList();
                if (settledfList.Count > 0)
                {
                    PropertyInfo[] propInfos = typeof(ISettledList).GetProperties();
                    foreach (var item in settledfList)
                    {
                        foreach (var curPropt in propInfos)
                        {
                            curPropt.SetValue(item, curPropt.GetValue(inData));
                        }
                    }
                }
                result = 1;
            }
            else if (state == RecordActions.Inserted)
            {
                SettledListDBModel settledOne = new SettledListDBModel();
                PropertyInfo[] propInfos = typeof(ISettledList).GetProperties();
                foreach (var curPropt in propInfos)
                {
                    curPropt.SetValue(settledOne, curPropt.GetValue(inData));
                }
                db.SettledList.Add(settledOne);
                result = 1;
            }
            else if (state == RecordActions.Deleted)
            {
                List<SettledListDBModel> persInfList = db.SettledList.Where(predicate).ToList();
                if (persInfList.Count > 0)
                {
                    foreach (var item in persInfList)
                    {
                        db.SettledList.Remove(item);
                    }
                }
                result = 1;
            }
            return result;

        }
        public int HandleRoomsTable(IRoom inData, Expression<Func<RoomDBModel, bool>> predicate, RecordActions state)
        {
            int result = 0;
            if (state == RecordActions.Inserted)
            {
                RoomDBModel room = new RoomDBModel();
                PropertyInfo[] propInfos = typeof(IRoom).GetProperties();
                foreach (var curPropt in propInfos)
                {
                    curPropt.SetValue(room, curPropt.GetValue(inData));
                }
                db.RoomList.Add(room);
                result = 1;
            }
            return result;

        }
        public int HandleRoomFurnituresTable(IRoomFurniture inData, Expression<Func<RoomFurnitureDBModel, bool>> predicate, RecordActions state)
        {
            int result = 0;
            if (state == RecordActions.Inserted)
            {
                RoomFurnitureDBModel furnUnit = new RoomFurnitureDBModel();
                PropertyInfo[] propInfos = typeof(IRoomFurniture).GetProperties();
                foreach (var curPropt in propInfos)
                {
                    curPropt.SetValue(furnUnit, curPropt.GetValue(inData));
                }
                db.RoomFurnitureList.Add(furnUnit);
                result = 1;
            }
            return result;

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
